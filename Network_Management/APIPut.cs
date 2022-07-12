using Newtonsoft.Json;
using UnityEngine;
public class APIPut
{
	

	public static APIRequest APITest(string obstacleId, string type)
	{
		APIRequest request = new APIRequest();
		request.url = string.Format("{0}test/test1", GameConstants.HOST);
		var data = new
		{
			id = int.Parse(obstacleId),
			type = type
		};
		request.body = JsonConvert.SerializeObject(data);
		return request;
	}
}

