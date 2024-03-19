using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCube : MonoBehaviour
{
    private Animator anim;

    void OnTriggerEnter(Collider c)
    {
        anim = GetComponent<Animator>();
        if (c.attachedRigidbody)
        {
            CubeCollector cc = c.attachedRigidbody.gameObject.GetComponent<CubeCollector>();
            if (cc != null)
            {
               
                anim.SetBool("startRotate", true);
                cc.ReceiveCube();
            }
        }



    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody)
        {
            CubeCollector cc = c.attachedRigidbody.gameObject.GetComponent<CubeCollector>();
            if (cc != null)
            {
                
                anim.SetBool("startRotate", false);
                
            }
        }



    }

}



