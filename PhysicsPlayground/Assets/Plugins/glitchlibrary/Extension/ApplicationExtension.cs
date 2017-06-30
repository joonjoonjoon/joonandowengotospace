using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public static class ApplicationExtension
{
    public const string defaultLanguageCode = "en";

#if UNITY_IOS
    [DllImport("__Internal")]
    public extern static string _GlitchGetLocale();
#endif

    // C#
    // returns "en" / "de" / ...
    public static string GetDeviceLanguageCode()
    {
        var result = "";
        try
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            var locale = new AndroidJavaClass("java.util.Locale");
            var localeInst = locale.CallStatic<AndroidJavaObject>("getDefault");
            var name = localeInst.Call<string>("toString");
            result = name.ToString();
#elif UNITY_IOS && !UNITY_EDITOR
        result = _GlitchGetLocale();
#endif
            if (result == "" || result.Length < 2)
                result = GetLanguageCode(Application.systemLanguage);
        }
        catch (System.Exception e)
        {
            Debug.Log("[GlitchLocale] Couldn't find locale on Android... \n" + e);
        }

        return result;
    }

    public static string GetLanguageCode(SystemLanguage systemLanguage)
	{
		switch(systemLanguage)
		{
			case SystemLanguage.Afrikaans:
				return "af";
			case SystemLanguage.Arabic:
				return "ar";
			case SystemLanguage.Basque:
				return "eu";
			case SystemLanguage.Belarusian:
				return "be";
			case SystemLanguage.Bulgarian:
				return "bg";
			case SystemLanguage.Catalan:
				return "ca";
			case SystemLanguage.Chinese:
				return "zh";
			case SystemLanguage.Czech:
				return "cs";
			case SystemLanguage.Danish:
				return "da";
			case SystemLanguage.Dutch:
				return "nl";
			case SystemLanguage.English:
				return "en";
			case SystemLanguage.Estonian:
				return "et";
			case SystemLanguage.Faroese:
				return "fo";
			case SystemLanguage.Finnish:
				return "fi";
			case SystemLanguage.French:
				return "fr";
			case SystemLanguage.German:
				return "de";
			case SystemLanguage.Greek:
				return "el";
			case SystemLanguage.Hebrew:
				return "he";
			case SystemLanguage.Hungarian:
				return "hu";
			case SystemLanguage.Icelandic:
				return "is";
			case SystemLanguage.Indonesian:
				return "id";
			case SystemLanguage.Italian:
				return "it";
			case SystemLanguage.Japanese:
				return "ja";
			case SystemLanguage.Korean:
				return "ko";
			case SystemLanguage.Latvian:
				return "lv";
			case SystemLanguage.Lithuanian:
				return "lt";
			case SystemLanguage.Norwegian:
				return "no";
			case SystemLanguage.Polish:
				return "pl";
			case SystemLanguage.Portuguese:
				return "pt";
			case SystemLanguage.Romanian:
				return "ro";
			case SystemLanguage.Russian:
				return "ru";
			case SystemLanguage.SerboCroatian:
				return "sr";
			case SystemLanguage.Slovak:
				return "sk";
			case SystemLanguage.Slovenian:
				return "sl";
			case SystemLanguage.Spanish:
				return "es";
			case SystemLanguage.Swedish:
				return "sv";
			case SystemLanguage.Thai:
				return "th";
			case SystemLanguage.Turkish:
				return "tr";
			case SystemLanguage.Ukrainian:
				return "uk";
			case SystemLanguage.Unknown:
				return defaultLanguageCode;
			case SystemLanguage.Vietnamese:
				return "vi";
#if UNITY_5
			case SystemLanguage.ChineseSimplified:
				return "zh-CHS";
			case SystemLanguage.ChineseTraditional:
				return "zh-CHT";
#endif
			default:
				return defaultLanguageCode;
		}
	}
}

