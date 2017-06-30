using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnPlatform : MonoBehaviour
{
    public RuntimePlatform[] platforms;
    public bool ShowInEditor;
    public bool ShowInDesktop;
    public bool ShowInIOS;
    public bool ShowInAndroid;

	void Awake() {
	    switch (Application.platform)
	    {
	        case RuntimePlatform.OSXEditor:
	            if(!ShowInEditor) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.OSXPlayer:
	            if(!ShowInDesktop) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.WindowsPlayer:
	            if(!ShowInDesktop) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.WindowsEditor:
	            if(!ShowInEditor) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.IPhonePlayer:
	            if(!ShowInIOS) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.Android:
	            if(!ShowInAndroid) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.LinuxPlayer:
	            if(!ShowInDesktop) gameObject.SetActive(false);
	            break;
	        case RuntimePlatform.LinuxEditor:
	            if(!ShowInEditor) gameObject.SetActive(false);
	            break;
	    }
	}
}
