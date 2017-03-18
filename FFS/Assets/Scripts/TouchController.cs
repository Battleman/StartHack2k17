using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to demonstrate the use of touch controllers.
/// More information: https://developer3.oculus.com/documentation/game-engines/latest/concepts/unity-ovrinput/
/// 
/// Author: Thomas Rouvinez
/// Creation date: 27.02.2017
/// Last modified: 27.02.2017
/// </summary>
public class TouchController : MonoBehaviour {

    [Header("Controller type")]
    public OVRInput.Controller handType;
    public enum ControllerHand { Left, Right };

    // Update is called once per frame
    void Update() {

        // Update position and rotation of the controller
        gameObject.transform.localPosition = OVRInput.GetLocalControllerPosition(handType);
        gameObject.transform.localRotation = OVRInput.GetLocalControllerRotation(handType);

        OVRInput.Update();

        // Detect events
        #region Start button

        if (OVRInput.Get(OVRInput.Button.Start))
        {
            Debug.Log("Start button pressed");
        }

        #endregion

        #region Button A and X

        // Returns true if the primary button (typically “A”) was pressed this frame
        if (OVRInput.GetDown(OVRInput.Button.One, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button X Down");
            }
            else
            {
                Debug.Log("Button A Down");
            }
        }

        // Returns true if the primary button (typically “A”) is currently pressed
        if (OVRInput.Get(OVRInput.Button.One, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button X Pressed");
            }
            else
            {
                Debug.Log("Button A Pressed");
            }
        }

        // Returns true if the primary button (typically “A”) is released
        if (OVRInput.GetUp(OVRInput.Button.One, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button X up");
            }
            else
            {
                Debug.Log("Button A up");
            }
        }

        // Returns true if the primary button is touched
        if (OVRInput.Get(OVRInput.Touch.One, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button X touched");
            }
            else
            {
                Debug.Log("Button A touched");
            }
        }

        #endregion

        #region Button B and Y

        // Returns true if the secondary button (typically “B”) was pressed this frame
        if (OVRInput.GetDown(OVRInput.Button.Two, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button Y down");
            }
            else
            {
                Debug.Log("Button B down");
            }
        }

        // Returns true if the secondary button (typically “B”) is currently pressed
        if (OVRInput.Get(OVRInput.Button.Two, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button Y pressed");
            }
            else
            {
                Debug.Log("Button B pressed");
            }
        }

        // Returns true if the secondary button (typically “B”) is released
        if (OVRInput.GetUp(OVRInput.Button.Two, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button Y up");
            }
            else
            {
                Debug.Log("Button B up");
            }
        }

        // Returns true if the primary button is touched
        if (OVRInput.Get(OVRInput.Touch.Two, handType))
        {
            if (handType == OVRInput.Controller.LTouch)
            {
                Debug.Log("Button Y touched");
            }
            else
            {
                Debug.Log("Button B touched");
            }
        }

        #endregion

        #region Triggers

        // Returns a float of the left index finger trigger’s current state.  
        // (range of 0.0f to 1.0f)
        if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0f)
        {
            Debug.Log("Left index trigger: " + OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));
        }

        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0f)
        {
            Debug.Log("Left hand trigger: " + OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger));
        }

        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0f)
        {
            Debug.Log("Right index trigger: " + OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
        }

        if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0f)
        {
            Debug.Log("Right hand trigger: " + OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger));
        }

        // Trigger touch
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger))
        {
            Debug.Log("Left index trigger touched");
        }

        if (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger))
        {
            Debug.Log("Right index trigger touched");
        }

        #endregion

        #region Thumbsticks

        // Returns true if the primary thumbstick has been moved upwards more than halfway.  
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            Debug.Log("Left thumbstick down");
        }

        // Returns true if the primary thumbstick is currently pressed (clicked as a button)
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick)){
            Vector2 coordinates = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Debug.Log("Left thumbstick pressed: x: " + coordinates.x
                            + " / y: " + coordinates.y);
        }

        // Returns true if the primary thumbstick has been moved upwards more than halfway.  
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)){
            Debug.Log("Right thumbstick up");
        }

        // Returns true if the secondary thumbstick has been moved upwards more than halfway.  
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            Debug.Log("Right thumbstick down");
        }

        // Returns true if the secondary thumbstick is currently pressed (clicked as a button)
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            Vector2 coordinates = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

            Debug.Log("Right thumbstick pressed: x: " + coordinates.x
                            + " / y: " + coordinates.y);
        }

        // Returns true if the secondary thumbstick has been moved upwards more than halfway.  
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            Debug.Log("Right thumbstick up");
        }

        // Thumbsticks touch
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            Debug.Log("Left thumbstick touched");
        }

        if (OVRInput.Get(OVRInput.Touch.SecondaryThumbstick))
        {
            Debug.Log("Right thumbstick touched");
        }

        #endregion

        #region Thumbrest

        // Thumb rest touch
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbRest))
        {
            Debug.Log("Left thumb rest touched");
        }

        if (OVRInput.Get(OVRInput.Touch.SecondaryThumbRest))
        {
            Debug.Log("Right thumb rest touched");
        }

        #endregion
    }
}