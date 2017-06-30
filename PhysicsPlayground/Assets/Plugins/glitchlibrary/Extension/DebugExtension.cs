using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using System.Diagnostics;
using System.Reflection;
#endif

public class DebugExtension : MonoBehaviour
{
    public enum BlipType
    {
        None,
        Warning,
        Error
    }

    public static void BlipList(params object[] list)
    {
        var str = String.Join(" \t", list.Select(item => item.ToString()).ToArray());

        DoBlip(null, Color.cyan, str, BlipType.None);
    }

    public static void Blip(string text)
    {
        DoBlip(null, Color.green, text, BlipType.None);
    }

    public static void Blip(Object context = null, string text = "",Color? color = null)
    {
        if (color == null) color = Color.green;
        DoBlip(context, (Color)color, text, BlipType.None);
    }

    public static void BlipError(Object context = null, string text = "",Color? color = null)
    {
        if (color == null) color = Color.red;
        DoBlip(context, (Color)color, text, BlipType.Error);
    }

    private static void DoBlip(Object context, Color color, string text, BlipType type)
    {

#if UNITY_EDITOR
        var st = new StackTrace();
        var sf = st.GetFrame(2);

        var currentMethodName = sf.GetMethod();
        var methodname = currentMethodName.Name;
        var file = currentMethodName.DeclaringType.ToString();

        var ust = StackTraceUtility.ExtractStackTrace();
        var frames = ust.Split('\n');
#else
        string file = "";
        string methodname ="";
        string[] frames = new string[]{"", "", ""};
#endif
        switch (type)
        {
            case BlipType.None:
                UnityEngine.Debug.Log(ColorPad("[blip]", color) + " " + file + ":" + methodname + "\n" + text + "\n" + "[blip origin] " + frames[2] + "\n\n", context);
                break;
            case BlipType.Warning:
                UnityEngine.Debug.LogWarning(ColorPad("[blip]", color) + " " + file + ":" + methodname + "\n" + text + "\n" + "[blip origin] " + frames[2] + "\n\n", context);
                break;
            case BlipType.Error:
                UnityEngine.Debug.LogError(ColorPad("[blip]", color) + " " + file + ":" + methodname + "\n" + text + "\n" + "[blip origin] " + frames[2] + "\n\n", context);
                break;
            default:
                break;
        }
    }

    public static string ColorPad(string value, Color color)
    {
        return "<color=#" + ColorExtension.colorToHex(color) + ">" + value + "</color>";
    }
}
