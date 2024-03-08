using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ContractConfig
{
    public string contractFactoryAddress;
    public string currencyAddress;
    public string ethCurrencyAddress;
    public string usdtCurrencyAddress;
    public string codeHashAddress;
    public string abiFormatFactoryContract;
    public string abiFormatCurrecyContract;
    public string abiFormatCodeHashContract;
    public string abiFormatGameContract;
    public long maxBudgetAllowance = 10000;
}
