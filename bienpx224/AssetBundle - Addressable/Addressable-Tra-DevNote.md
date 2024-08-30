																									
1. Tạo Group																									
	! Đặt asset bên ngoài folder resourses																								
	- App ko cần phải có folder của asset để load asset																								
	- group dùng để quản lý asset cùng label (Tiện để load theo label)																								
	- Chọn Built In Data và uncheck Include Resources Folder và Include Build Setting Scenes																								
																									
2. Setting																									
	* Window>Asset Management>Addressables>Groups	For Dev Test	- Tắt user aset bundle cache test load thành công																						
																									
		For Release	- Nhớ bật user asset bundle cache																						
																									
	* Window>Asset Management>Addressables>Setting	Catalog	- Sửa Player Version Override thành version của profile thay vì version của bản build																						
																									
3. Window>Asset Management>Addressables>Profile																									
	- Profile gồm những nguồn dùng kết hợp để test và deploy (Default gồm built-in và editor hosted tương ứng vs việc load từ local và remote)																								
	- Tạo nhiều profile để thay đổi qua lại trên nhiều môi trường khác nhau: test trên nhiều remote server,...																								
	* Tạo Profile																								
		- Mở window Addressables > Profiles -> chọn Create -> Chọn Profile																							
		- Local: chọn Buit-in																							
		- Remote: Chọn custom (vì dùng server tự host) 																							
	!! Tạo Variables để dễ thay đổi path, trong đó có Version hỗ trợ server host nhiều version cùng lúc khi có thay đổi để không bị lỗi 				VD: [Host]/[Path]/[Version]/[SubPath]																				
																									
4. Script AddressableManager																									
	* Chứa methods để load và lưu ảnh, trả về ảnh theo tên																								
	* Load Catalog	- Check bản local và remote có khác nhau không																							
	* Load từng bundle	- Mỗi bundle chứa asset của từng chapter																							
																									
5. Test	- Chọn PlayModeScript (Window Groups): Use Existing Build																								
	- Local: dùng 127.0.0.1																								
																									
6. Load Process	Login -->	- Nếu current chapter < 4	LoadFromResource(current chapter)		--> Vào Home Scene --> 	Load All Chapter (in background): từ cả resouces và bundle																			
		- Nếu current chapter >= 4	LoadFromBundle(current chapter)																						
																									
Lỗi	fix																								
blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource																									
																									
																									
