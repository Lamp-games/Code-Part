using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    /// <summary>
    /// handles the UI point at an interaction
    /// </summary>
    [Header("setup")]
    public Vector3 TargetPosition;
    public GameObject theEntirePointerThing;
    public Transform t_circle;
    public Transform t_square;
    public Transform t_complete;

    [Header("animation")]
    public AnimationCurve interact_circle;
    public AnimationCurve interact_square;
    public AnimationCurve interact_complete;
    public AnimationCurve fail_circle;
    public AnimationCurve fail_square;
    public AnimationCurve fail_complete;



    /// <summary>
    ///   offScreen > movingIntoView > closingCompletion
    ///   closingCompletion >- complete -> offScreen
    ///   closingCompletion > cooling 
    ///   cooling > offScreen
    ///   cooling > closingCompletion
    ///   offScreen > movingIntoView > failing > offScreen
    /// </summary>
    public enum InteractionState { closingCompletion, cooling, offScreen, movingIntoView, failing}
    public InteractionState myState;



    [Header("progress")]
    public float progress = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = new Vector3(0,200,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(theEntirePointerThing.transform.position, TargetPosition) > 1)
        {
            Vector3 nowPosition = theEntirePointerThing.transform.position;
            nowPosition = Vector3.Lerp(nowPosition, TargetPosition, 0.05f);
            theEntirePointerThing.transform.position = nowPosition;
        }
        HandleAnimation();

    }

    private void HandleAnimation()
    {
        switch (myState)
        {
            case InteractionState.closingCompletion:
                progress += Time.deltaTime;
                if (progress > 1)
                {
                    myState = InteractionState.offScreen;
                    progress = 1;
                }

                break;
            case InteractionState.cooling:
                progress -= Time.deltaTime;
                if (progress < 0)
                {
                    myState = InteractionState.offScreen;
                    progress = 0;
                }

                break;
            case InteractionState.offScreen:
                if (progress > 0)
                {
                    progress -= Time.deltaTime;
                }
                break;
            case InteractionState.movingIntoView:
                if (progress > 0)
                {
                    theEntirePointerThing.transform.Rotate(0, 0, 15.69f * Time.deltaTime);
                    progress -= Time.deltaTime;
                }
                break;
            case InteractionState.failing:
                progress += Time.deltaTime;
                if (progress > 1)
                {
                    theEntirePointerThing.transform.Rotate(0, 0, 69.420f * Time.deltaTime);
                    progress = 0;
                }

                break;
            default:
                break;
        }

        float val = interact_circle.Evaluate(progress);
        t_circle.transform.localScale = new Vector3(val, val, val);


        float val2 = interact_square.Evaluate(progress);
        t_square.transform.localScale = new Vector3(val2, val2, val2);

        float val3 = interact_complete.Evaluate(progress);
        t_complete.transform.localScale = new Vector3(val3, val3, val3);

        theEntirePointerThing.transform.Rotate(0, 0, -150 * Time.deltaTime);

    }

    public void SetPosition(Vector3 newPos)
    {
        if (newPos != TargetPosition)
        {
            Debug.Log("My new position "+ newPos.ToString());
            TargetPosition = newPos;
        }
    }

    public void SetState(InteractionState newState)
    {
        if (newState != myState)
        {
            Debug.Log("My new state "+newState);
            myState = newState;
        }
    }

}
