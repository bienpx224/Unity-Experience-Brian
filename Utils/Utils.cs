using UnityEngine;
using System;

public static class Utils
{
    public static string FirstLetterToUpper(string str)
    {
        try
        {
            if (str == null)
                return str;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
        catch (Exception e)
        {
            return str;
        }
    }

    // Returns the system architecture
     public static string GetArchitecture()
     {
         using (var system = new AndroidJavaClass("java.lang.System"))
         {
             return system.CallStatic<string>("getProperty", "os.arch");
         }
     }

     public static void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }
}
