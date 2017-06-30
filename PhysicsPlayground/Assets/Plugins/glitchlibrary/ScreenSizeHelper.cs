using UnityEngine;
using System.Collections;
using System;

public class ScreenSizeHelper : MonoSingleton<ScreenSizeHelper>
{
    private static Action<Vector2> _resizeEvent;

    public static Action<Vector2> ResizeEvent
    {
        get
        {
            if (instance == null)
            {
                // should be initialized now, because it was touched
            }
            return _resizeEvent;
        }

        set
        {
            if (instance == null)
            {
                // should be initialized now, because it was touched
            }
            _resizeEvent = value;
            DoCheckAndUpdateDimensions();
        }

    }

    private static Vector2? dimensions;

    void Awake()
	{
		instance = this;
	}

    void Start()
    {
        DoCheckAndUpdateDimensions();
    }

    void Update()
    {
        DoCheckAndUpdateDimensions();
    }

    static void DoCheckAndUpdateDimensions()
    {
        var currentDimensions =new Vector2(Screen.width, Screen.height);

        if (dimensions == null)
        {
            dimensions = currentDimensions;
            if (_resizeEvent != null && dimensions != null)
                _resizeEvent.Invoke(dimensions.Value);
        }
        else
        {
            if (dimensions.Value != currentDimensions)
            {
                dimensions = currentDimensions;
                if (_resizeEvent != null && dimensions != null)
                {
                    _resizeEvent.Invoke(dimensions.Value);
                }

            }
        }
    }

    public static Vector2 GetWorldSpaceDimensionsOfScreen(Camera cam)
    {
        if (cam == null) return new Vector2(Screen.width, Screen.height); // can be used by world and non-world space canvases.

        var worldScreenWidth = Mathf.Abs(cam.ScreenToWorldPoint(Vector3.zero.withZ(cam.transform.position.z)).x -
            cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, cam.transform.position.z)).x);
        var worldScreenHeight = Mathf.Abs(cam.ScreenToWorldPoint(Vector3.zero.withZ(cam.transform.position.z)).y -
            cam.ScreenToWorldPoint(new Vector3(0, Screen.height, cam.transform.position.z)).y);

        if (worldScreenWidth == 0)
        {
            Debug.Log("Warning, I recorded a screen width of 0. This should never happen. Is the List camera set to perspective?");
        }
        else if (!cam.orthographic)
        {
            Debug.Log("Warning, List camera set to perspective. This might have unexpected consequences.");
        }

        return new Vector2(worldScreenWidth, worldScreenHeight);
    }
}
