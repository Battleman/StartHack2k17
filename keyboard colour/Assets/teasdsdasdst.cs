using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboard_led_changer : MonoBehaviour
{

    int red, blue, green;
    public string effectLabel;
    enum colors { R, G, B };
    colors curr_color = colors.R;
    // Use this for initialization
    void Start()
    {

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

        LogitechGSDK.LogiLedSetLighting(red, blue, green);

        if (Input.GetKeyDown(KeyCode.R))
        {
            curr_color = colors.R;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            curr_color = colors.G;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            curr_color = colors.B;
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            switch (curr_color)
            {
                case colors.R:
                    red = (int)Mathf.Min((float)(red + 5), 100.0f);
                    break;
                case colors.G:
                    green = (int)Mathf.Min((float)(green + 5), 100.0f);
                    break;
                case colors.B:
                    blue = (int)Mathf.Min((float)(blue + 5), 100.0f);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            switch (curr_color)
            {
                case colors.R:
                    red = (int)Mathf.Max((float)(red - 5), 0.0f);
                    break;
                case colors.G:
                    green = (int)Mathf.Max((float)(green - 5), 0.0f);
                    break;
                case colors.B:
                    blue = (int)Mathf.Max((float)(blue - 5), 0.0f);
                    break;
            }
        }
    }

    void OnDestroy()
    {
        //Before quitting, we need to restore the user's backlighting settings
        LogitechGSDK.LogiLedRestoreLighting();
        LogitechGSDK.LogiLedShutdown();
    }
}