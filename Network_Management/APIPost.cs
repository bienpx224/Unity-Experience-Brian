using Newtonsoft.Json;
public class APIPost
{
	public static APIRequest APITest(int gold = 1, int food = 1)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}test/test1", GameConstants.HOST);

		var data = new
		{
			gold = gold.ToString(),
			food = food.ToString()
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}

}

public class APIRequest
{
	public string url;
	public string body;
}
