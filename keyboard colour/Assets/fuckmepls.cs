﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuckmepls : MonoBehaviour
{

    //   ---- MODEL COLOURS ----
    const float INTERPOLATE_STEP = 0.08f;
    GameObject test;
    System.Random rand = new System.Random();

    float goal_r;
    float goal_g;
    float goal_b;

    float curr_r;
    float curr_g;
    float curr_b;

    //   ---- KEYBOARD COLOURS -----
    int red, blue, green;
    public string effectLabel;
    enum colors { R, G, B };
    colors curr_color = colors.R;

    // Use this for initialization
    void Start()
    {

        test = gameObject;
        goal_r = 0.0f;
        goal_g = 0.0f;
        goal_b = 0.0f;

        curr_r = 0.0f;
        curr_g = 0.0f;
        curr_b = 0.0f;

        blue = 0;
        red = 0;
        green = 0;
        LogitechGSDK.LogiLedInit();
        LogitechGSDK.LogiLedSaveCurrentLighting();
        effectLabel = "Press F to test flashing effect, P to test pulsing effect \n " +
            "Press mouse1 to set all lighting to random color, mouse 2 to set G910 to random bitmap \n" +
            "Press E to start per-key effects (F1-F12) show on supported devices \n" +
            "Press S to stop the effects \n";

        
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 250, 500, 200), effectLabel);
    }

    // Update is called once per frame
    void Update()
    {

        LogitechGSDK.LogiLedSetLighting(red, green, blue);

        if (Input.GetKey(KeyCode.R))
        {
            curr_color = colors.R;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            curr_color = colors.G;
        }
        else if (Input.GetKey(KeyCode.B))
        {
            curr_color = colors.B;
        }
        else if (Input.GetKey(KeyCode.KeypadPlus))
        {
            switch (curr_color)
            {
                case colors.R:
                    goal_r = Mathf.Min((float)(goal_r + 0.02f), 1.0f);
                    red = (int)Mathf.Min((float)(red + 2), 100.0f);
                    break;
                case colors.G:
                    goal_g = Mathf.Min((float)(goal_g + 0.02f), 1.0f);
                    green = (int)Mathf.Min((float)(green + 2), 100.0f);
                    break;
                case colors.B:
                    goal_b = Mathf.Min((float)(goal_b + 0.02f), 1.0f);
                    blue = (int)Mathf.Min((float)(blue + 2), 100.0f);
                    break;
            }
        }

        else if (Input.GetKey(KeyCode.KeypadMinus))
        {
            switch (curr_color)
            {
                case colors.R:
                    goal_r = Mathf.Max((float)(goal_r - 0.02f), 0.0f);
                    red = (int)Mathf.Max((float)(red - 2), 0.0f);
                    break;
                case colors.G:
                    goal_g = Mathf.Max((float)(goal_g - 0.02f), 0.0f);
                    green = (int)Mathf.Max((float)(green - 2), 0.0f);
                    break;
                case colors.B:
                    goal_b = Mathf.Max((float)(goal_b - 0.02f), 0.0f);
                    blue = (int)Mathf.Max((float)(blue - 2), 0.0f);
                    break;
            }
        }

        interpolate_color(ref curr_r, ref curr_g, ref curr_b, ref goal_r, ref goal_g, ref goal_b);

        Color whateverColor = new Color(curr_r, curr_g, curr_b, 1);
        MeshRenderer gameObjectRenderer = test.GetComponent<MeshRenderer>();
        Material newMaterial = new Material(Shader.Find("Standard"));

        newMaterial.color = whateverColor;
        gameObjectRenderer.material = newMaterial;
    }

    void OnDestroy()
    {
        //Before quitting, we need to restore the user's backlighting settings
        LogitechGSDK.LogiLedRestoreLighting();
        LogitechGSDK.LogiLedShutdown();
    }

    static void interpolate_color(ref float curr_r, ref float curr_g, ref float curr_b,
                                   ref float goal_r, ref float goal_g, ref float goal_b)
    {

        float diff_r = goal_r - curr_r;
        float diff_g = goal_g - curr_g;
        float diff_b = goal_b - curr_b;

        if (Mathf.Abs(diff_r) < INTERPOLATE_STEP)
        {
            curr_r = goal_r;
        }
        else if (diff_r > 0.0f)
        {
            curr_r += INTERPOLATE_STEP;
        }
        else
        {
            curr_r -= INTERPOLATE_STEP;
        }

        if (Mathf.Abs(diff_g) < INTERPOLATE_STEP)
        {
            curr_g = goal_g;
        }
        else if (diff_g > 0.0f)
        {
            curr_g += INTERPOLATE_STEP;
        }
        else
        {
            curr_g -= INTERPOLATE_STEP;
        }

        if (Mathf.Abs(diff_b) < INTERPOLATE_STEP)
        {
            curr_b = goal_b;
        }
        else if (diff_b > 0.0f)
        {
            curr_b += INTERPOLATE_STEP;
        }
        else
        {
            curr_b -= INTERPOLATE_STEP;
        }
    }
}