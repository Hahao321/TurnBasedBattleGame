using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static string Username;
    public static int Money;

    public static bool LoggedIn { get { return Username != null; } }

    public static void LogOut()
    {
        Username = null;
    }
    
    public static void UpdateAllInfo()
    {

    }

}
