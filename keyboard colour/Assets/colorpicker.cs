
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorpicker : MonoBehaviour {

    const float INTERPOLATE_STEP = 0.08f;
    GameObject test;
    System.Random rand = new System.Random();

    float goal_r;
    float goal_g;
    float goal_b;

    float curr_r;
    float curr_g;
    float curr_b;

    // Use this for init
    void Start () {

        test = gameObject;
        goal_r = 0.0f;
        goal_g = 1.0f;
        goal_b = 0.0f;

        curr_r = 0.0f;
        curr_g = 1.0f;
        curr_b = 0.0f;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            goal_r = 1.0f;
            goal_g = 0.0f;
            goal_b = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            goal_r = 0.0f;
            goal_g = 1.0f;
            goal_b = 0.5f;
        }

        interpolate_color(ref curr_r, ref curr_g, ref curr_b, ref goal_r, ref goal_g, ref goal_b);

        Color whateverColor = new Color(curr_r, curr_g, curr_b, 1);
        MeshRenderer gameObjectRenderer = test.GetComponent<MeshRenderer>();
        Material newMaterial = new Material(Shader.Find("Standard"));

        newMaterial.color = whateverColor;
        gameObjectRenderer.material = newMaterial;
    }


    static void interpolate_color(ref float curr_r, ref float curr_g, ref float curr_b,
                                    ref float goal_r, ref float goal_g, ref float goal_b)
    {

        float diff_r = goal_r - curr_r;
        float diff_g = goal_g - curr_g;
        float diff_b = goal_b - curr_b;

        if (Math.Abs(diff_r) < INTERPOLATE_STEP)
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

        if (Math.Abs(diff_g) < INTERPOLATE_STEP)
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

        if (Math.Abs(diff_b) < INTERPOLATE_STEP)
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



