using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteveAI : MonoBehaviour
{
    /// <summary>
    /// figure out if we can use NavAgent for a 2d game. would work great
    /// </summary>
    [Header("Design")]
    public float KickForce = 90000;// insane strong kick bro
    public float RandomMoverForce = 15000;
    [Header("Debug")]
    public int frameswalked = 0;
    private Rigidbody2D rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rg.velocity.magnitude < 1f)
        {
            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().Play();
            }
            Debug.Log("Walking " + frameswalked);
            frameswalked++;
            float x = UnityEngine.Random.Range(-1f, 1f);
            float y = UnityEngine.Random.Range(-1f, 1f);
            float z = 0;
            Vector3 dir = new Vector3(x, y, z).normalized;
            rg.AddForce(dir * RandomMoverForce * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("I AM THE KING, " + collision.gameObject.name+"!!!!");
            Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * KickForce * Time.deltaTime);
        }
        else
        {
            if (collision.gameObject.tag == "rat")
            {
                Debug.Log("I AM THE KING, " + collision.gameObject.name + "!!!!");
                Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * KickForce/5 * Time.deltaTime);
            }
            Debug.Log("I, a human being, just accidentally bumped into . . . " + collision.gameObject.name);
        }

    }

}
