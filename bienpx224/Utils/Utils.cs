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
    public void LookAtTarget()
    {
        // For game 2D : This game Object look at the target
        Vector3 diff = this.targetPosition - transform.parent.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.parent.position = Quaternion.Euler(0f, 0f, rot_z + 90);
    }
}
