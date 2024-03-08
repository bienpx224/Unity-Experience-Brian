# Thirdweb Unity SDK 

## Integrate Thirdweb to Unity : 
*** (OFFICIAL DOCUMENT THIRDWEB UNITY SDK)["https://portal.thirdweb.com/unity] 
- Import Thirdweb > Spawn ThirdwebManager to your scene (or find and drag)
- In ThirdwebManager : 
    + Setup your client id : take it from thirdweb dashboard.
- Find prefab "Prefab_ConnectWallet" and put in on Canvas where contain Connect UI. Adjust Connect Wallet button position and UI for suitable.
- Find "walletProvider_EmbeddedWallet", "WalletProvider_Metamask", "WalletProvider_WalletConnect" and setting Canvas sort order to higher (maybe 10 is fine).

- Add listener WalletConnected function inside Prefab_ConnectWallet in Editor : When you know user connected or not, you can change login state of user. 


## Tương tác với Smart Contract :
### Import address vào dashboard thirdweb :  
- Trong Thirdweb Dashboard : Khi đã có Smart Contract verified thì ta cần address của contract đó.
RỒi vào phần Contract, Import contract, nhập address, chọn mạng và Import. 
- Có thể sẽ cần chút thời gian để bên Thirdweb get data lấy được các functions, event trong contract mình đã import. 
- Khi đã import xong. Trong Unity, ta sử dụng hàm sdk.GetContract({address}) để lấy ra contract đó rồi tương tác Read, Write, Event với contract theo như document của thirdweb. 

### Sử dụng Address và ABI trực tiếp trong Unity : 
- Tạo biến lưu trữ Address và ABI string của Contract. 
- Tạo biến contract để lưu contract đó lại : 
```
    Contract newContract = sdk.GetContract(contractAddress , abiFormat);
```

### Thực hiện call Read/Write func của SC : 
- Trong Thirdweb document cũng có ví dụ về việc tương tác với SC. 
- Có thể sử dụng `SmartContractManager.cs` trong folder này để thực hiện việc tương tác với SC 


