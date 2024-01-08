# Unity-Experience-Brian
Base experience about Unity : 

## JSON : 
- Đọc dữ liệu từ 1 file json vào 1 biến trong game : Ta cần tạo struct khai báo các kiểu của JSON đó rồi convert.
FoodConfig[] food = JsonConvert.DeserializeObject<FoodConfig[]>(Resources.Load<TextAsset>(path).text);
## JSON OBJECT FILE ĐÃ DỰNG SẴN : 

- VD việc update thay đổi 1 trường trong array JSONObject : 
```c#
	public void UpdateIslandData(JSONObject islandData)
	{
		var temp = _islandDatas.list.Find(x => x["islandId"].ToString() == islandData["islandId"].ToString());
		int indexData = _islandDatas.list.IndexOf(temp);
		_islandDatas.list[indexData]["matrix"] = islandData["matrix"];
	}
```

- VD về việc get array trong json : 
```c#
List<JSONObject> a =  GridManager.Instance.GetCurrentIslandIndex()["metaData"]["expandableSlots"].list ;

List<JSONObject> trainer = UserData.Instance.GetAllTrainers().Where(x => x["buniHouseId"].ToString() == data["id"].ToString()).ToList();

List<JSONObject> filter = _listData.Where(x =>
					{
						int level = (int)x["level"].n;
						return level > 0 && level <= 10;
					}).ToList();

UserData.Instance.GetAllBunniHouse().Where(x => x["id"].n == _data["id"].n).FirstOrDefault()      

```

- VD về việc Remove index trong JSONObject: 
```c# 
public void RemoveBuniHouse(string buniHouseId)
	{
		var index = _buniHouseDatas.list.IndexOf(_buniHouseDatas.list.Find(x => x["id"].ToString() == buniHouseId));
		if (index >= 0)
		{
			_buniHouseDatas.list.RemoveAt(index);
		}
	}
```




