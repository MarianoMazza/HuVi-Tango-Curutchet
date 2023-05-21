using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupColliderScript : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision, this.GetComponent<Collider>());
        }
    }
}
