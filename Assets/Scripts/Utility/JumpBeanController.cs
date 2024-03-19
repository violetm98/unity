using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBeanController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded;
    private float nextTime;

    public float minForce = 5f;
    public float maxForce = 10f;
    public float minTime = 1f;
    public float maxTime = 3f;

    public float speed = 2f;

     

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        nextTime = Time.time;
    }

    // 
    void FixedUpdate()
    {
        if(isGrounded & Time.time>=nextTime)
        {
           
            {
                ApplyRandomForce();
                nextTime = Time.time + Random.Range(minTime, maxTime);
            }
            
        }
        
    }

    void ApplyRandomForce()
        
    {
        Vector3 force = Random.insideUnitSphere * Random.Range(minForce, maxForce);

        Vector3 torque = new Vector3(1,1,1) * Random.Range(minForce, maxForce);

        rb.AddForce(force * speed, ForceMode.Impulse);
        rb.AddTorque(torque * speed, ForceMode.Impulse);
      
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
