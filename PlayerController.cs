using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// handles player input and game fell
    /// </summary>

    [Header("Setup")]
    public GameObject myCamera;
    [Header("Input")]
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;
    public KeyCode Interact;
    [Header("Design")]
    public Vector3 someUIoffset;
    public float InteractionRange = 40f;
    public float moveForceStart=100;
    public float moveForceMaintain=10;

    public float maxVelocity = 10f;
    [Header("Debug")]
    public GameObject ImInteractingWith;
    public float OpenVelocity;


    //---------private 
    private Vector3 CamLerp = Vector3.zero;
    private Rigidbody2D rg;
    private Vector3 offset = new Vector3(0, 0, -10f);
    private InteractUI myCursor;
    private float firstBoostCooldown=0f;

    // Start is called before the first frame update
    void Start()
    {
        myCursor = GetComponent<InteractUI>();
        rg = GetComponent<Rigidbody2D>();
        if (rg == null)
        {
            rg = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    [ContextMenu("Setup Test")]
    public void testStart()
    {
        Debug.Log(gameObject.name + "Start");
        Start();
    }

    [ContextMenu("Camera Test")]
    public void testCameraControl()
    {
        Debug.Log(gameObject.name + " starting and booting camera : " + myCamera.name);
        Start();
        CameraControl();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveInput();
        CameraControl();

        //some debug things
        OpenVelocity = rg.velocity.magnitude;



        //time to interact. 
        if (Input.GetKeyDown(Interact))
        {
            PlayerInteractStart();
        }
        if (Input.GetKey(Interact))
        {
            PlayerInteractMaintain();
        }
        if (Input.GetKeyUp(Interact))
        {
            PlayerInteractEnd();
        }

    }

    private void PlayerInteractEnd()
    {
        myCursor.SetPosition(myCursor.TargetPosition + new Vector3(0,200,0));

        ImInteractingWith = null;
        // ui fucks off and cools
    }

    private void PlayerInteractMaintain()
    {
        InteractUI.InteractionState aState;
        if (ImInteractingWith == null)
        {
            aState = InteractUI.InteractionState.failing;
        }
        else
        {
            aState = InteractUI.InteractionState.closingCompletion;
        }
        myCursor.SetState(aState);
        
        //if bad interaction, then change state and cfuck off
    }

    private void PlayerInteractStart()
    {
        //set UI pos, then a state, gotta catch some nulls tho. 
        ImInteractingWith = InteractionManager.instance.FindInRangeToPlayerWhoCalled(transform.position, InteractionRange);

        InteractUI.InteractionState aState = InteractUI.InteractionState.movingIntoView;
        myCursor.SetState(aState);


        if (ImInteractingWith == null)
        {
            myCursor.SetPosition(transform.position + someUIoffset);
        }
        else
        {
            myCursor.SetPosition(ImInteractingWith.transform.position);
        }

    }

    private void CameraControl()
    {
        Vector3 camVelocity = new Vector3(rg.velocity.x, rg.velocity.y, 0);

        if (camVelocity.magnitude > maxVelocity/2)
        {
            camVelocity = camVelocity.normalized * (maxVelocity/2);
        }
        CamLerp = Vector3.Lerp(CamLerp, camVelocity, 0.025f);
        myCamera.transform.position = transform.position + offset + CamLerp;
    }

    private void MoveInput()
    {

        bool anyKey = Input.GetKey(MoveUp)|| Input.GetKey(MoveDown)|| Input.GetKey(MoveLeft)|| Input.GetKey(MoveRight);
        bool reallyanyKey = Input.GetKeyDown(MoveUp)|| Input.GetKeyDown(MoveDown)|| Input.GetKeyDown(MoveLeft)|| Input.GetKeyDown(MoveRight);

        if (anyKey || reallyanyKey)
        {

            if (rg.velocity.magnitude > maxVelocity)
            {
                return;
            }

            Vector2 moveDir = Vector2.zero;
            if (Input.GetKey(MoveUp))
            {
                moveDir += Vector2.up;
            }
            if (Input.GetKey(MoveDown))
            {
                moveDir += Vector2.down;
            }
            if (Input.GetKey(MoveLeft))
            {
                moveDir += Vector2.left;
            }
            if (Input.GetKey(MoveRight))
            {
                moveDir += Vector2.right;
            }

            moveDir.Normalize();


            if (rg.velocity.magnitude < 0.5f && firstBoostCooldown <= 0f)
            {
                rg.AddForce(moveDir * moveForceStart * Time.deltaTime);
                firstBoostCooldown = 1f;
            }
            else
            {
                rg.AddForce(moveDir * moveForceMaintain * Time.deltaTime);
            }

        }
        else
        {

            if (firstBoostCooldown > 0)
            {
                firstBoostCooldown -= Time.deltaTime;
            }
        }

    }
}
