using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAudioListener : MonoBehaviour
{
    [Header("Setup")]

    public Transform TheCat;
    public Transform TheMouse;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = (TheCat.position + TheMouse.position) / 2;
    }


    
}
