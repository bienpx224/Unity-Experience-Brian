using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;
using Newtonsoft.Json.Linq;

public partial class NetworkManager : Singleton<NetworkManager>
{
    public static double UTCTimeRate;
    private string _accessToken = "";
    private long _expireTimeAccessToken = 0;
    private string _refreshToken = "";
    private string _password = "";
    private string _userId;
    private int _signStatus;
    public string walletAccount;

    
    public string AccessToken
    {
        get { return _accessToken; }
        set { _accessToken = value; }
    }
    public long ExpireTimeAccessToken
    {
        get { return _expireTimeAccessToken; }
        set { _expireTimeAccessToken = value; }
    }

    public string RefreshToken
    {
        get { return _refreshToken; }
        set { _refreshToken = value; }
    }

    public int SigninStatus
    {
        get { return _signStatus; }
        set { _signStatus = value; }
    }
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }

    private void Start()
    {
        Application.runInBackground = true;
        // StartCoroutine(IEGetWorldTime());
    }

    private IEnumerator IEGetWorldTime()
    {
        yield return CreateWebRequest("http://worldtimeapi.org/api/timezone/Europe/London", (string time) =>
        {
            JSONObject data = new JSONObject(time);
            UTCTimeRate = (TimeHelper.GetStartDate().AddSeconds(data["unixtime"].n) - DateTime.UtcNow).TotalSeconds;
            

        });
    }
}



