using Newtonsoft.Json;
public class APIDelete
{

	public static APIRequest APITest(string testId, int[] arrId)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}test/test1", GameConstants.HOST, testId);
		var data = new
		{
			trainerIds = arrId
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
	
}

