using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class APIGet
{
    public static string APIGetTest()
    {
        string url = string.Format("{0}test", GameConstants.HOST);
        return url;
    }
}
