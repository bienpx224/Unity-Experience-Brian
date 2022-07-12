using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.Serialization;
using System;
public partial class NetworkManager
{

    const int CODE_TOKEN_INVALID = 403;
    const string MESSAGE_TOKEN_INVALID = "Access token is not valid";

    private bool isWaitingRefreshToken = false;
    public bool IsWaitingRefreshToken
    {
        get { return isWaitingRefreshToken; }
        set { isWaitingRefreshToken = value; }
    }

    public IEnumerator CheckRefreshTokenDoing()
    {
        Debug.Log("CheckRefreshTokenDoing");
        while (IsWaitingRefreshToken == true)
        {
            Debug.Log("Dang doi ket qua refresh token");
            yield return null;
        }
        yield return 0;
    }
    public bool CheckExpireAccessToken()
    {
        DateTime _expireAt = CountDownBuilding.GetStartDate().AddSeconds(NetworkManager.Instance.ExpireTimeAccessToken);
        long totalSecond = (long)Math.Round((_expireAt - DateTime.UtcNow).TotalSeconds);
        if (totalSecond <= 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator RefreshTokenCallAPI(Action onComplete, Action onFail)
    {
        if (NetworkManager.Instance.IsLoginViaWallet)
        {
            onComplete();

        }
        else
        {
            IsWaitingRefreshToken = true;
            Debug.LogWarning("Start Refresh Token");
            String refreshToken = PlayerPrefs.GetString(GameConstants.REFRESH_TOKEN_REF, "");

            yield return NetworkManager.Instance.StartCoroutine(NetworkManager.Instance.CreateWebPostRequest(APIPost.APIRefreshAccessToken(refreshToken),
                (string response) =>
                {
                    JSONObject data = new JSONObject(response);
                    if (data && data["accessToken"] != null && data["refreshToken"] != null)
                    {

                        NetworkManager.Instance.AccessToken = data["accessToken"].str;
                        NetworkManager.Instance.RefreshToken = data["refreshToken"].str;
                        NetworkManager.Instance.ExpireTimeAccessToken = (long)data["accessTokenExpireAt"].n;

                        PlayerPrefs.SetString(GameConstants.ACCESS_TOKEN_REF, NetworkManager.Instance.AccessToken);
                        PlayerPrefs.SetString(GameConstants.REFRESH_TOKEN_REF, NetworkManager.Instance.RefreshToken);
                        PlayerPrefs.SetString(GameConstants.EXPIRE_TOKEN_REF, NetworkManager.Instance.ExpireTimeAccessToken.ToString());
                        IsWaitingRefreshToken = false;
                        Debug.LogWarning("Done Refresh Token Complete ");
                        onComplete();
                    }
                    else
                    {
                        IsWaitingRefreshToken = false;
                        Debug.LogWarning("Done Refresh Token: " + IsWaitingRefreshToken);
                        onFail();
                        Debug.Log("ko co access token va refresh token : Logout");

                        NetworkManager.Instance.Logout();
                    }
                },
                (object objectFail) =>
                {
                    IsWaitingRefreshToken = false;
                    Debug.LogWarning("Done Refresh Token: " + IsWaitingRefreshToken);
                    onFail();
                    Debug.Log("Call Refresh Token API failed");
                    Debug.Log(objectFail.ToString());

                    NetworkManager.Instance.Logout();
                }
            ));
        }
    }

    public IEnumerator CreateWebDeleteRequest(APIRequest requestAPi, System.Action<string> onComplete, System.Action<object> onFail, bool isShowToast = true)
    {
        if (CheckExpireAccessToken() && IsWaitingRefreshToken == false)
        {
            yield return StartCoroutine(
            RefreshTokenCallAPI(
                () => { },
                () => { }
            )
            );
        }
        if (IsWaitingRefreshToken == true)
        {
            yield return new WaitUntil(() => IsWaitingRefreshToken == false);
        }

        UnityWebRequest request = UnityWebRequest.Delete(requestAPi.url);
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(requestAPi.body);
        UploadHandler data = new UploadHandlerRaw(payload);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.uploadHandler = data;
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        Debug.LogError("DELETE:" + requestAPi.url);
        Debug.LogError(requestAPi.body);
        PopupLog._LogText += "= Post API : " + requestAPi.url + "\n";
        PopupLog._LogText += "\t body : " + requestAPi.body + "\n";
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        yield return request.SendWebRequest();

        if (!string.IsNullOrEmpty(request.error))
        {
            if (request.downloadHandler != null && request.downloadHandler.text != null)
            {
                /* Check if the request was expired -> need refresh access token */
                JSONObject requestFailure = new JSONObject(request.downloadHandler.text.ToString());
                if (requestFailure && (int)requestFailure["statusCode"].n == CODE_TOKEN_INVALID && requestFailure["message"].list[0].str == MESSAGE_TOKEN_INVALID)
                {
                    if (IsWaitingRefreshToken == false)
                    {
                        yield return StartCoroutine(
                        RefreshTokenCallAPI(
                            () =>
                            {
                                NetworkManager.Instance.StartCoroutine(NetworkManager.Instance.CreateWebDeleteRequest(requestAPi, onComplete, onFail));
                            },
                            () => { }
                        )
                        );
                    }
                }
                else
                {
                    if (isShowToast)
                        Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                    if (onFail != null)
                    {
                        onFail.Invoke(request.error);
                    }
                }
            }
            else
            {
                Toast.Show("No message available");
                if (onFail != null)
                {
                    onFail.Invoke(null);
                }
            }
        }
        else
        {
            //Debug.LogError(request.downloadHandler.text);
            string result = request.downloadHandler == null ? null : request.downloadHandler.text;
            // PopupLog._LogText += "\t Response : " + result + "\n";
            PopupLog._LogText += "\t Success \n";
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }

    public IEnumerator CreateWebPostRequest(APIRequest requestAPi, System.Action<string> onComplete, System.Action<object> onFail, bool isShowToast = true)
    {


        if (requestAPi.url != GameConstants.API_REFRESH_ACCESSTOKEN && requestAPi.url != GameConstants.API_LOGIN_USERPASS && requestAPi.url != GameConstants.API_LOGIN_WALLET)
        {
            if (CheckExpireAccessToken() && IsWaitingRefreshToken == false)
            {
                Debug.Log("===== Oh no! Access Token has expired. Let get new one ");
                yield return StartCoroutine(
                RefreshTokenCallAPI(
                    () => { Debug.Log("DONE SUCCESS Call APi RefreshToken"); },
                    () => { Debug.Log("DONE FAIL Call APi RefreshToken"); }
                )
                );
            }
            if (IsWaitingRefreshToken == true)
            {
                yield return new WaitUntil(() => IsWaitingRefreshToken == false);
            }
        }

        UnityWebRequest request = UnityWebRequest.Post(requestAPi.url, UnityWebRequest.kHttpVerbPOST);
        byte[] payload = System.Text.Encoding.UTF8.GetBytes(requestAPi.body);
        UploadHandler data = new UploadHandlerRaw(payload);
        request.uploadHandler = data;
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        Debug.LogError("POST:" + requestAPi.url);
        Debug.LogError(requestAPi.body);
        PopupLog._LogText += "= Post API : " + requestAPi.url + "\n";
        PopupLog._LogText += "\t body : " + requestAPi.body + "\n";
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        yield return request.SendWebRequest();

        string resultError = "{'statusCode': 400,'message': ['Bad gateway cannot access'],'error': 'Bad gateway cannot access'}";
        if (!string.IsNullOrEmpty(request.error))
        {
            if (request != null && request.downloadHandler != null && request.downloadHandler.text != null)
            {
                resultError = request.downloadHandler.text;
            }
            Debug.LogError("error " + resultError);
            PopupLog._LogText += "\t Error : " + resultError + "\n";
            PopupLog._LogText += "\t Msg : " + new JSONObject(resultError)["message"].list[0].str + "\n";

            /* Check if the request was expired -> need refresh access token */
            JSONObject requestFailure = new JSONObject(resultError);
            if (requestFailure && (int)requestFailure["statusCode"].n == CODE_TOKEN_INVALID && requestFailure["message"].list[0].str == MESSAGE_TOKEN_INVALID)
            {
                if (requestAPi.url != GameConstants.API_REFRESH_ACCESSTOKEN && requestAPi.url != GameConstants.API_LOGIN_USERPASS && requestAPi.url != GameConstants.API_LOGIN_WALLET)
                {
                    if (IsWaitingRefreshToken == false)
                    {
                        yield return StartCoroutine(
                        RefreshTokenCallAPI(
                            () =>
                            {
                                NetworkManager.Instance.StartCoroutine(NetworkManager.Instance.CreateWebPostRequest(requestAPi, onComplete, onFail));
                            },
                            () => { }
                        )
                        );
                    }
                }
            }
            else
            {
                if (isShowToast)
                    Toast.Show(new JSONObject(resultError)["message"].list[0].str);
                if (onFail != null)
                {
                    onFail.Invoke(resultError);
                }
            }

        }
        else
        {
            Debug.LogError(string.Format("Response: {0}", request.downloadHandler.text));

            string result = request.downloadHandler.text;
            // PopupLog._LogText += "\t Response : " + result + "\n";
            PopupLog._LogText += "\t Success \n";
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }

    public IEnumerator CreateWebRequest(string url, System.Action<string> onComplete)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
		request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");

        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {


        }
        else
        {
            string result = request.downloadHandler.text;
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }

    public IEnumerator CreateWebGetRequest(string requestAPi, System.Action<string> onComplete, System.Action<object> onFail, bool isShowToast = true)
    {
        if (requestAPi != APIGet.APIGetAllConfig())
        {
            if (CheckExpireAccessToken() && IsWaitingRefreshToken == false)
            {
                yield return StartCoroutine(
                RefreshTokenCallAPI(
                    () => { },
                    () => { }
                )
                );
            }
            if (IsWaitingRefreshToken == true)
            {
                yield return new WaitUntil(() => IsWaitingRefreshToken == false);
            }
        }
        UnityWebRequest request = UnityWebRequest.Get(requestAPi);
		request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        Debug.LogError("GET:" + requestAPi);
        if (!requestAPi.Contains("/harvest"))
        {
            PopupLog._LogText += "= Get API : " + requestAPi + "\n";
        }
        if (_accessToken != null && requestAPi != APIGet.APIGetAllConfig())
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError("error " + request.downloadHandler.text);
            if (!requestAPi.Contains("/harvest"))
            {
                PopupLog._LogText += "\t Error : " + request.downloadHandler.text + "\n";
                PopupLog._LogText += "\t Msg : " + new JSONObject(request.downloadHandler.text)["message"].list[0].str + "\n";
            }

            /* Check if the request was expired -> need refresh access token */
            JSONObject requestFailure = new JSONObject(request.downloadHandler.text.ToString());
            if (requestFailure && (int)requestFailure["statusCode"].n == CODE_TOKEN_INVALID && requestFailure["message"].list[0].str == MESSAGE_TOKEN_INVALID)
            {
                if (IsWaitingRefreshToken == false)
                {
                    yield return StartCoroutine(
                    RefreshTokenCallAPI(
                        () =>
                        {
                            NetworkManager.Instance.StartCoroutine(NetworkManager.Instance.CreateWebGetRequest(requestAPi, onComplete, onFail));
                        },
                        () => { }
                    )
                    );
                }
            }
            else
            {
                try
                {
                    if (isShowToast)
                    {
                        Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                    }

                }
                catch (Exception e)
                {

                }
                if (onFail != null)
                {
                    onFail.Invoke(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                }
            }


        }
        else
        {
            string result = request.downloadHandler.text;
            Debug.LogError(request.downloadHandler.text);
            // PopupLog._LogText += "\t Response : " + result + "\n";
            if (!requestAPi.Contains("/harvest"))
            {
                PopupLog._LogText += "\t Success \n";
            }
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }


    public IEnumerator CreateWebPutRequest(APIRequest requestAPi, System.Action<string> onComplete, System.Action<object> onFail, bool isPatch = false, bool isShowToast = true)
    {

        if (CheckExpireAccessToken() && IsWaitingRefreshToken == false)
        {
            yield return StartCoroutine(
            RefreshTokenCallAPI(
                () => { },
                () => { }
            )
            );
        }
        if (IsWaitingRefreshToken == true)
        {
            yield return new WaitUntil(() => IsWaitingRefreshToken == false);
        }

        byte[] payload = System.Text.Encoding.UTF8.GetBytes(requestAPi.body);
        UnityWebRequest request = UnityWebRequest.Put(requestAPi.url, payload);
		request.downloadHandler = new DownloadHandlerBuffer();
        if (isPatch)
            request.method = "PATCH";
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Access-Control-Expose-Headers", "ETag");
        if (_accessToken != null)
        {
            request.SetRequestHeader("Authorization", "Bearer " + _accessToken);
        }
        Debug.LogError(requestAPi.url);
        Debug.LogError(requestAPi.body);
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.LogError("error " + request.downloadHandler.text);
            

            /* Check if the request was expired -> need refresh access token */
            JSONObject requestFailure = new JSONObject(request.downloadHandler.text.ToString());
            if (requestFailure && (int)requestFailure["statusCode"].n == CODE_TOKEN_INVALID && requestFailure["message"].list[0].str == MESSAGE_TOKEN_INVALID)
            {
                if (IsWaitingRefreshToken == false)
                {
                    yield return StartCoroutine(
                    RefreshTokenCallAPI(
                        () =>
                        {
                            NetworkManager.Instance.StartCoroutine(NetworkManager.Instance.CreateWebPutRequest(requestAPi, onComplete, onFail));
                        },
                        () => { }
                    )
                    );
                }
            }
            else
            {
                try
                {
                    if (isShowToast)
                        Toast.Show(new JSONObject(request.downloadHandler.text)["message"].list[0].str);
                }
                catch (Exception e)
                {

                }
                if (onFail != null)
                {
                    onFail.Invoke(request.error);
                }
            }
        }
        else
        {
            Debug.LogError(request.downloadHandler.text);

            string result = request.downloadHandler.text;
            if (onComplete != null)
            {
                onComplete.Invoke(result);
            }
        }
    }
}
