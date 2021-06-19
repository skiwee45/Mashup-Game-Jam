using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    void Start()
    {

    }
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter2D(Collision2D collision)
    {
        //dangerous layer
        if (collision.gameObject.layer == 6)
        {
            PlayerManager.Instance.Respawn();
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < -15)
        {
            PlayerManager.Instance.Respawn();
        }
    }
}
