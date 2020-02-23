using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool CatInteract;
    public bool MouseInteract;

    public enum ItemState {okay , mess}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()

    {
        Debug.Log(gameObject.name + "was used");


    }


}
