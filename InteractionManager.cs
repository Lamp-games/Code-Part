using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    /// <summary>
    /// contains all the interactions
    /// answers call for interaction (good or bad)
    /// </summary>
    public static InteractionManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("SingletonError me:" + gameObject.name + " and that thing " + instance.gameObject.name + " it scares me");
        }
        Setup();
    }

    [ContextMenu("Setup Interactions")]
    public void Setup()
    {
        Debug.Log("Setting up interactions");
        bool goodSetup = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;

            if (go.tag != "Interact")
            {
                go.tag = "Interact";
            }
            if (go.GetComponent<Interaction>() == null)
            {
                goodSetup = false;
                go.AddComponent<Interaction>();
                Debug.LogError(go.name + " needs Interaction designed!");
            }


        }
        if (goodSetup)
        {
            Debug.Log("Count : " + transform.childCount + "\t Interaction Setup complete!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject FindInRangeToPlayerWhoCalled(Vector3 callerPosition,float Range)
    {
        GameObject returnable = null;
        float closest=Range+1f;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform go = transform.GetChild(i);
            if (Vector3.Distance(go.position, callerPosition) < closest)
            {
                closest = Vector3.Distance(go.position, callerPosition);
                returnable = go.gameObject;
            }

        }


       


        return returnable;
    }

}
