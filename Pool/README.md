# Unity-Experience-Brian
Base experience about Unity : 

# Lean Pool : 
- Package manager : Search Leanpool và import tất cả về. có thể bỏ example cho đỡ nặng.
- Sau đó với những prefabs nào mình Instantiate nhiều thì sử dụng Pool.
    + Ở đầu file import thêm : using Lean.Pool;
    + Spawn a object :  Lean.Pool.LeanPool.Spawn(bombPrefabs, position, Quaternion.identity);
    + Destroy a object :  Lean.Pool.LeanPool.Despawn(bombPrefabs, position, Quaternion.identity);

- Khi này trong Scene sẽ tạo ra từng Pool để chứa cho những prefabs Spawn trước đó. Nếu bị thiếu thì sẽ là spawn ra cái mới, nếu có rồi thì lấy từ trong pool ra rồi active nó lên. 

## Lưu ý : 
- Do việc Pool nó sẽ lấy cái cũ ra rồi active lên. Vì vậy các data ở trong component có thể bị khác (vẫn lưu là data bị thay đổi ở lần cuối cùng trước khi được Despawn). Vậy nên ta cần lưu ý setup lại data về default hoặc theo đúng ý bản thân. Nên có hàm Show() hoặc setup ở trong hàm OnEnable().