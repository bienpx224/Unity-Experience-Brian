using Nethereum.ABI;
using Nethereum.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Nethereum.Util;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Crypto.Digests;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SmartContractManager : Singleton<SmartContractManager>
{
    public BigInteger chainIdActive = 168587773; 
    public ContractConfig contractConfig;
    private ThirdwebSDK sdk;
    private Contract contract;
    private string initCodeHash;

    Dictionary<string, Contract> listContractIntance = new(); 

    #region EventContract
    UnityEvent<ContractEvent<object>> eventContractListener { get; set; }
    Dictionary<string, UnityEvent<ContractEvent<object>>> listEventCustomContract = new();
    #endregion

    void Start()
    {
        InitializeContract();
    }

    void InitializeContract()
    {
        if(contractConfig == null)
        {
            Debug.LogError("Contract Config null");
            return;
        }

        sdk = ThirdwebManager.Instance.SDK;
        contract = sdk.GetContract(contractConfig.contractFactoryAddress, contractConfig.abiFormatFactoryContract);
        eventContractListener = new UnityEvent<ContractEvent<object>>();

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            contract.events.ListenToAll((ContractEvent<object> anyEvent) =>
            {
                Debug.Log("Event occurred: " + anyEvent.data);
                eventContractListener.Invoke(anyEvent);
            });
        }

    }

    #region Read methods

    public async void CallReadMethod<T>(string contractAddress, string methodName, object[] args = null, Action<T> callback = null)
    {
        Contract newContract = listContractIntance.GetValueOrDefault(contractAddress);
        if (newContract == default(Contract))
        {
            newContract = sdk.GetContract(contractAddress);
            listContractIntance[contractAddress] = newContract; 
        }
        var response = default(T);
        if (args == null || args.Length == 0)
        {
            response = await newContract.Read<T>(methodName);
        }
        else
        {
            response = await newContract.Read<T>(methodName, args);
        }
        callback?.Invoke(response);
    }

    public async void CallReadMethod<T>(string methodName, Action<T> callback = null)
    {
        var response = default(T);
        response = await contract.Read<T>(methodName);
        callback?.Invoke(response);
    }

    public async void CallReadMethod<T>(string methodName, object[] args, Action<T> callback = null)
    {
        var response = default(T);
        if (args == null || args.Length == 0)
        {
            CallReadMethod<T>(methodName, callback);
            return;
        }
        else
        {
            response = await contract.Read<T>(methodName, args);
        }
        callback?.Invoke(response); 
    }

    public async Task<T> CallReadMethodAsync<T>(string contractAddress, string abiFormat, string methodName, object[] args = null)
    {
        Contract newContract = listContractIntance.GetValueOrDefault(contractAddress);
        if (newContract == default(Contract))
        {
            newContract = sdk.GetContract(contractAddress , abiFormat);
            listContractIntance[contractAddress] = newContract;
        }
        var response = default(T);
        try
        {
            if (args == null || args.Length == 0)
            {
                response = await newContract.Read<T>(methodName);
            }
            else
            {
                response = await newContract.Read<T>(methodName, args);
            }
        }
        catch (Exception ex)
        {

        }
        return response;
    }

    public async Task<T> CallReadMethodAsync<T>(string methodName)
    {
        var response = default(T);
        response = await contract.Read<T>(methodName);
        return response;
    }

    public async Task<T> CallReadMethodAsync<T>(string methodName, object[] args = null)
    {
        var response = default(T);
        if (args == null || args.Length == 0)
        {
            response = await CallReadMethodAsync<T>(methodName);
        }
        else
        {
            response = await contract.Read<T>(methodName, args);
        }
        return response;
    }
    #endregion

    #region Write methods

    public async void CallWriteMethod(string contractAddress, string abiFormat, string methodName, object[] args = null, TransactionRequest transactionRequest = default, Action<object> callback = null)
    {
        TransactionResult response = default;
        try
        {
            var walletAddress = await sdk.wallet.GetAddress();
            if (walletAddress == null)
            {
                PopupSuggestConnectWallet.Show();
                return;
            }
            Contract newContract = GetCustomContract(contractAddress, abiFormat);
            if (args == null || args.Length == 0)
            {
                response = await newContract.Write(methodName, transactionRequest);
            }
            else if (!transactionRequest.Equals(default(TransactionRequest)))
            {
                response = await newContract.Write(methodName, args, transactionRequest);
            }
            else
            {
                response = await newContract.Write(methodName, args);
            }
        }
        catch (Exception ex) { }
            
        var responseJson = await response.ToJToken();
        callback?.Invoke(responseJson); 
    }

    public async void CallWriteMethod(string methodName, TransactionRequest transactionRequest = default, Action<object> callback = null)
    {
        TransactionResult response = default;
        try
        {
            var walletAddress = await sdk.wallet.GetAddress();
            if (walletAddress == null)
            {
                PopupSuggestConnectWallet.Show();
                return;
            }
            

            response = await contract.Write(methodName, transactionRequest);
        }
        catch (Exception ex)
        {

        }
    
        var responseJson = await response.ToJToken();
        callback?.Invoke(responseJson);
    }


    public async void CallWriteMethod(string methodName, object[] args, TransactionRequest transactionRequest = default, Action<object> callback = null)
    {
        var walletAddress = await sdk.wallet.GetAddress();
        if (walletAddress == null)
        {
            PopupSuggestConnectWallet.Show();
            return;
        }
        TransactionResult response = default;
        if (args == null || args.Length == 0)
        {
            CallWriteMethod(methodName, transactionRequest, callback);
            return;
        }
        else if (!transactionRequest.Equals(default(TransactionRequest)))
        {
            response = await contract.Write(methodName, args, transactionRequest);
        }
        else
        {
            response = await contract.Write(methodName, args);
        }
        
        var responseJson = await response.ToJToken();
        callback?.Invoke(responseJson); 
    }

    public async Task<object> CallWriteMethodAsync(string methodName, object[] args = null, TransactionRequest transactionRequest = default)
    {
        TransactionResult response = default;
        try
        {
            var walletAddress = await sdk.wallet.GetAddress();
            if (walletAddress == null)
            {
                PopupSuggestConnectWallet.Show();
                return null;
            }
            
            if (args == null || args.Length == 0)
            {
                response = await contract.Write(methodName, transactionRequest);
            }
            else if (!transactionRequest.Equals(default(TransactionRequest)))
            {
                response = await contract.Write(methodName, args, transactionRequest);
            }
            else
            {
                response = await contract.Write(methodName, args);
            }
            
            
        }
        catch (Exception ex)
        {

        }
        var responseJson = await response.ToJToken();
        return responseJson;
    }
    #endregion

    #region Event's methods
    public void AddEventFactoryContractListener(UnityAction<ContractEvent<object>> action)
    {
        Debug.Log("Add Event Contract here");
        eventContractListener.AddListener(action);

        contract.events.ListenToAll((ContractEvent<object> anyEvent) =>
        {
            //eventContractListener.Invoke(anyEvent);
            //action.Invoke(anyEvent);
        });
    }

    public void RemoveAllEventFactoryContractListeners()
    {
        eventContractListener.RemoveAllListeners();
    }

    public async Task<List<ContractEvent<object>>> GetAllEventsContract(EventQueryOptions option)
    {
        var response = await contract.events.GetAll(option);
        return response;
    }

    public async void AddEventToCustomContractListeners(string contractAddress, string abiFormat, UnityAction<ContractEvent<object>> action, bool forceAdd = false)
    {
        if(listEventCustomContract.ContainsKey(contractAddress) && !forceAdd)
        {
            return;
        }    
        Debug.Log("Add Event to custom contract listeners: ");
        var customContract = GetCustomContract(contractAddress, abiFormat);
        var eventListener = GetEventListenerCustomContract(contractAddress);
        eventListener.AddListener(action);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //await customContract.events.RemoveAllListeners();
            customContract.events.ListenToAll((ContractEvent<object> anyEvent) =>
            {
                Debug.Log("Custom event listener: " + anyEvent.data);
                eventListener.Invoke(anyEvent);
            });
        }
       
    }
        

    #endregion

    #region Generator Room
    public async Task<string> GenerateRoomAddress(string currency, BigInteger timestamp)
    {
        var abiEncoder = new ABIEncode();
        var val1 = new ABIValue(ABIType.CreateABIType("address"), currency);
        var val2 = new ABIValue(ABIType.CreateABIType("uint256"), timestamp);
        var dataEncoded = abiEncoder.GetSha3ABIEncodedPacked(new ABIValue[] { val1, val2 });

        var initCodeHash = await GetInitCodeHash();

        var dataKeccack = dataEncoded;

        var res = ContractUtils.CalculateCreate2AddressUsingByteCodeHash(contractConfig.contractFactoryAddress, dataKeccack.ToHex(), initCodeHash);

        return res;
    }

    public async Task<string> GetLastestRoomAddress()
    {
        var factory = contractConfig.contractFactoryAddress;
        var currency = contractConfig.currencyAddress;
        var initCodeHash = await GetInitCodeHash();
        var result = "";


        var timezoneLocal = TimeZoneInfo.Local;
        var currentTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + (long)timezoneLocal.BaseUtcOffset.TotalSeconds;
        var interval = GameDataManager.Instance.GameConfig.intervalEachMatch;
        var division = currentTimeStamp / interval;
        result = await GenerateRoomAddress(currency, division * interval);

        return result;
    }

    #endregion

    #region Alowance contract
    public void AllowanceTo(string spender, BigInteger amount, Action<object> callback = null)
    {
        CallWriteMethod(contractConfig.currencyAddress,contractConfig.abiFormatCurrecyContract,  SmartContractMethodsName.approve.ToString(), new object[] {spender, amount.ToString()},callback: callback);
    }

    public async Task<BigInteger> GetAllowanceAmount(string owner, string spender)
    {
        var response = await CallReadMethodAsync<BigInteger>(contractConfig.currencyAddress, contractConfig.abiFormatCurrecyContract, SmartContractMethodsName.allowance.ToString(), new object[] {owner, spender});
        return response;
    }

    #endregion

    #region Init code hash
    public async Task<string> GetInitCodeHash()
    {
        if(!string.IsNullOrEmpty(initCodeHash))
        {
            return initCodeHash;
        }
        initCodeHash = await CallReadMethodAsync<string>(contractConfig.codeHashAddress, contractConfig.abiFormatCodeHashContract, SmartContractMethodsName.getInitHash.ToString());
        Debug.Log($"Init code hash: {initCodeHash}");
#if TEMP_INITCODE_HASH
        initCodeHash = "0x9a9e94198748d9d7ffe6bb4a5dbe306acd22e07c85e2b1e678888e4737467e5c";
#endif
        return initCodeHash; 
    }
    #endregion

    #region Utils
    public Contract GetCustomContract(string contractAddress, string abiFormat)
    {
        Contract newContract = listContractIntance.GetValueOrDefault(contractAddress);
        if (newContract == default(Contract))
        {
            newContract = sdk.GetContract(contractAddress, abiFormat);
            listContractIntance[contractAddress] = newContract;
        }
        return newContract;
    }

    public UnityEvent<ContractEvent<object>> GetEventListenerCustomContract(string contractAddress)
    {
        var eventListener = listEventCustomContract.GetValueOrDefault(contractAddress);
        if (eventListener == default)
        {
            eventListener = new();
            listEventCustomContract[contractAddress] = eventListener;
        }
        return eventListener;

    }

    #endregion
}
