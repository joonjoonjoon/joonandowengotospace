using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Linq;

[InitializeOnLoad]
public class RootFolderCleanupGuard
{
    private static string[] exceptions = new string[] { "GPGSIds.cs" };

    static RootFolderCleanupGuard()
    {
        System.IO.DirectoryInfo datapath = new DirectoryInfo(Application.dataPath);
        var scripts = datapath.GetFiles("*.cs");

        int count = 0;
        for (int i = 0; i < scripts.Length; i++)
        {
            if(!exceptions.Contains(scripts[i].Name))
            {
                count++;
            }
        }

        if(count > 0)
        {
            Debug.Log("<color=red>SCRIPTS IN ROOT:</color> you have " + count + " .cs files in your root. Don't be that guy.");
        }
    }
}