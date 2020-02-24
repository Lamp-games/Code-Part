using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Design")]

    public float OpenAfter=42f;
    private bool waitingtoDie = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AudioSource>().isPlaying == false && waitingtoDie)
        {
            Destroy(gameObject);
        }

        if (OpenAfter < 0)
        {
            waitingtoDie = true;
            OpenAfter = 0f;
            transform.Translate(-15 * Time.deltaTime, 0, 0);
            GetComponent<AudioSource>().Play();
        }
        else
        {
            OpenAfter -= Time.deltaTime;
        }


    }
}
