# Đôi điều về Addressable System :

## Tài liệu : 
- Lợi ích của Addressable : https://thegamedev.guru/unity-addressables/benefits-for-your-game/?fbclid=IwAR33v0w2ZyQ38xRYhA48YKi8TUcWdJ3Fp_gqerEHoe7DF9hm1vGpPyB5KuM
- 
## Lợi ích : 
- Reduce Your Game’s Memory Pressure, efficient memory management.
- Addressables gives you full control over how, when and where to store and load your game assets is incredibly useful for implementing and selling Downloadable Content.
- Reduced Build Size, Reduced Loading Times


## Step by Step :
- Link : https://unity3d.college/2019/10/07/unity3d-addressables-for-beginners-next-level-of-assetbundles/ 
- Youtube : https://youtu.be/uNpBS0LPhaU?t=425 
- (Check file ParticleSpawner.cs)
- Kiểm tra xem AssetReference có tồn tại hay không : ``` assetReference.RuntimeKeyIsValid() == false ``` Nếu ko tồn tại thì thôi return. 
- Kiểm tra xem _asyncOperationHandles có đang chứa tiến trình loading asset đó ko. Nếu đã có tiến trình và isDone thì Spawn nó ra từ LoadedReference. Còn nếu ko thì cho nó vào queue để lưu lại.
- Nếu chưa có trong Queue, chưa làm gì tới việc load Asset đó thì cần thực hiện function LoadAndSpawn(): 
``` c# 
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;
    ... 
    private void LoadAndSpawn(AssetReference assetReference){
        var op = Addressable.LoadAssetAsync<GameObject>(assetReference);
        _asyncOperationHandles[assetReferece] = op;
        op.Completed += (operation) =>{
            SpawnParticleFromLoadedReference(assetReference, GetRandomPosition());
            if(_queuedSpawnRequests.ContainsKey(assetReferece)){
                while(_queuedSpawnRequests[assetReferece]?.Any() == true){
                    var position = _queuedSpawnRequests[assetReferece].Dequeue();
                    SpawnParticleFromLoadedReference(assetReference, position);
                }
            }
        };
    }
```

- Lưu ý khi Instantiate addressable object, thì cũng cần Release addressable system. Ở trong VD này ta thêm Script NotifyOnDestroy, thêm trong nó delegate để lắng nghe sự kiện khi mà GameObject đó bị destroy thì cũng sẽ trigger hàm Remove() để ReleaseInstance addressable system đó. 
