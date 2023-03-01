using Newtonsoft.Json;
public class APIPost
{
	public static APIRequest APIRefreshAccessToken(string refreshToken)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}test/test1", GameConstants.HOST);

		var data = new
		{
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

}
