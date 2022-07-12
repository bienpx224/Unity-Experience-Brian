# Hướng dẫn các bước Sử dụng tích hợp Asset Bundle trong Unity : 

* Mục đích : Lưu trữ các asset, resource, prefabs trên server. Trong game khi nào dùng tới thì mới check và tải về, lưu trữ trong game để dùng
* Công dụng : Giảm tải dung lượng application, giảm thời gian load game. Chỉ tải asset, res về khi nào cần dùng tới. 

* Step : 
- Triển khai làm như bt, đóng gói thành các prefabs. 
- Tạo 1 asset bundle mới: ví dụ là bundleA 
- Các prefabs nào muốn nhét vào asset bundleA thì chọn bundleA ở mục asset bundle bên dưới cùng của Inspector của prefabs đó. Lưu ý tên của prefabs (hay gọi là name_asset_bundle) sẽ được dùng để LoadAsset sau này, để load chính xác prefabs mình cần sử dụng.
- Import module AssetBundleBrowser.unitypackage ở trong project github này. 
- Build asset bundle : Vào Window > AssetBundleBrowser > Show popup window : 
    + Ở đây ta sẽ có các tab quản lý asset bundles.
    + Tab 1 : Danh sách các asset bundle có trong project cùng với nội dung bên trong asset bundle đó. 
    + Tab 2 : Build : Thực thi việc build asset bundle thành các file (Chọn build target phù hợp với platform mong muốn)
    Sau khi build xong thì file asset bundle sẽ được lưu ở trong Output Path. 
- Upload file asset bundle vừa build được lên server, lấy url về lưu lại sau dùng để download về. 

* Check and Download asset bundle in the game: 
- Khi vào game nếu cần tới asset, ta sẽ check đường dẫn tới asset đó xem có tồn tại hay ko, nếu có rồi thì lôi ra sử dụng. Còn nếu chưa có thì ta thực hiện việc download asset bundle đó về, rồi lưu lại ở path đó. sau đó lôi ra sử dụng. Cơ bản là thế, giờ thì đây là example : 

- Function Load Island Prefab (It's already uploaded to server, Now I need to get it ) :
```c# 

// Function Load Prefab 
 public void LoadIslandPrefab(System.Action<GameObject> onCompleteLoad)
	{
		byte[] assetBytes;
        assetBytes = FileHelper.LoadFile(PathConstants.ISLAND_LOCAL_PATH); // get asset bundle from this path

        if (assetBytes != null)
        {
            var baneIslandAsset = AssetBundle.LoadFromMemory(assetBytes);
            onCompleteLoad.Invoke(baneIslandAsset.LoadAsset<GameObject>("<name_asset_bundle>"));
        }
        else
        {
            NetworkManager.Instance.StartCoroutine(IEDownloadAssetBundle(PathConstants.URL_DOWNLOAD_ASSET_BUNDLE, (byte[] downloadData) =>
            {
                var baneIslandAsset = AssetBundle.LoadFromMemory(downloadData);
                onCompleteLoad.Invoke(baneIslandAsset.LoadAsset<GameObject>("<name_asset_bundle>"));
            },
            PathConstants.ISLAND_LOCAL_PATH   // download and save it into this path
            ));
        }
    }
// Function IEDownloadAssetBundle 
private IEnumerator IEDownloadAssetBundle(string url, System.Action<byte[]> onCompleteLoad, string savePath = null)
	{
		Indicator.Show(); // Show loading indicator.

		using (UnityWebRequest request = UnityWebRequest.Get(url))
		{
			//UnityWebRequestAsyncOperation operation = request.SendWebRequest();
			yield return request.SendWebRequest();
			//request.downloadHandler = new DownloadHandlerBuffer();
			//request.SendWebRequest();
			//while (!operation.isDone)
			//{
			//	yield return new WaitForEndOfFrame();
			//	Debug.LogError(request.downloadProgress * 100f);
			//}
			if (savePath != null)
			{
                // Save download file into path specified.
				FileHelper.SaveFile(request.downloadHandler.data, savePath); 
			}
            // callback 
			onCompleteLoad.Invoke(request.downloadHandler.data);
		}
		Indicator.Hide();
	}

```

- Ok so that's basically done. In the next time it'll be loading from existing files.
Thanks for watching.

