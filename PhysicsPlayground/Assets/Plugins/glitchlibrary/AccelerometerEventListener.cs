﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ScriptOrder(-1000)]
public class AccelerometerEventListener : MonoSingleton<AccelerometerEventListener>
{
    public static Action ShakeEvent;
    public static Vector3 Gravity;
    public static bool isShaking;

    void Awake()
    {
        instance = this;
        Gravity = Vector3.zero;
    }

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;

    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;

        //Input.gyro.enabled = true;
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            if (ShakeEvent != null) ShakeEvent.Invoke();
            isShaking = true;
        }
        else
        {
            isShaking = false;
        }

        Gravity = acceleration;
    }
}
