# NetCode : 
- Mặc định là Server Authoriative. Vậy nên muốn nhận biết position hoặc direction của clients thì cần dùng NetworkVar để truyền lên func ServerRPC, sau đó Server sẽ broardcast tới các Clients biết và update thông tin. 
- Nếu muốn client tự quản lý vị trí đó thì tạo 1 script tên là OwnerClientTransform extends từ NetworkTransform (using Unity.NetCode.Components) : Trong đó override lại func OnIsServerAuthoritative cho return false. 
- Sau đó trong GameObject NetworkObject thì ko sử dụng NetworkTransform nữa mà sử dụng OwnerNetworkTransform. Vậy là sync được position, rotation, scale..
- Với animator thì cũng tương tự như Transform.