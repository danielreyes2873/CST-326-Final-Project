using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_Temp : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public Vector3 forwardDistance;
    public float radius;
    private Animator anim;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
        // Movement();
        //OtherButtons();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(gameObject.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-gameObject.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-gameObject.transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(gameObject.transform.right * speed * Time.deltaTime, ForceMode.Impulse);
        }

    }



    //You can ignore this if you do not want to see the blue sphere
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + forwardDistance, radius);
    }
}
