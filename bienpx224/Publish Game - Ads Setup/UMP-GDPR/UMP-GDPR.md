


## Setup GDPR && Setup UMP Consent :
- [Link youtube hướng dẫn](https://www.youtube.com/watch?v=pJPN2QWFSfg)
- Vào Admob > Privacy & mesaging : Chọn Create GDPR > Tạo 1 message GDPR cho App .

- Gắn script UMPManager.cs vào Scene đầu tiên của dự án. 
Khi đó sẽ check nếu ở EU thì show Popup Consent.

- Thực hiện Init Ads khi Consent đã được chấp thuận. 

- Trong PopupSettings, thêm button Privacy Settings để user có thể xem lại lựa chọn Consent : 
```c#
        if (_privacySettingsBtn != null)
        {
            _privacySettingsBtn.onClick.AddListener(UMPManager.Instance.ShowPrivacyOptionsForm);
            _privacySettingsBtn.interactable = UMPManager.Instance.privacyStatusRequired;
        }   
```
