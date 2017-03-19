using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorUpdater : MonoBehaviour {

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
    enum colors { R, G, B };
    colors curr_color = colors.R;


    



    // Use this for initialization
    void Start () {

        test = gameObject;
        goal_r = 0.0f;
        goal_g = 0.0f;
        goal_b = 0.0f;

        curr_r = 1.0f;
        curr_g = 0.0f;
        curr_b = 0.0f;

        blue = 0;
        red = 0;
        green = 0;

        ChangeMaterial(test, new Color(curr_r, curr_g, curr_b));



    }

    


    // Update is called once per frame
    void Update () {
        LogiInit.interpolate_color(ref curr_r, ref curr_g, ref curr_b);

        ChangeMaterial(test, new Color(curr_r, curr_g, curr_b));

    }

    static void ChangeMaterial(GameObject go, Color c)
    {
        if (go.GetComponent<Renderer>())
        {
            Material m = new Material(new Shader());
            m.color = c;
            go.GetComponent<Renderer>().material = m;
        }
        for (int i = 0; i < go.transform.childCount; i++)
        {
            ChangeMaterial(go.transform.GetChild(i).gameObject, c);
        }
    }
}
