using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogiInit : MonoBehaviour
{
    const float INTERPOLATE_STEP = 0.08f;

    int red;
    int green;
    int blue;

    private static float goal_r;
    private static float goal_g;
    private static float goal_b;

    float curr_r;
    float curr_g;
    float curr_b;

    enum colors { R, G, B };
    colors curr_color = colors.R;

    public static float getColorR()
    {
        return goal_r;
    }
    public static float getColorG()
    {
        return goal_g;
    }
    public static float getColorB()
    {
        return goal_b;
    }


    // Use this for initialization
    void Start()
    {
        goal_r = 0.0f;
        goal_g = 0.0f;
        goal_b = 0.0f;

        curr_r = 0.0f;
        curr_g = 0.0f;
        curr_b = 0.0f;
        LogitechGSDK.LogiLedInit();
        LogitechGSDK.LogiLedSaveCurrentLighting();
    }

    // Update is called once per frame
    void FixedUpdate()
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
    }
    public static void interpolate_color(ref float curr_r, ref float curr_g, ref float curr_b)
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
