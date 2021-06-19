using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter2D(Collision2D collision)
    {
        int layer = collision.gameObject.layer;
        //dangerous layer
        if (layer == 6)
        {
            PlayerManager.Instance.Respawn();
        }
        else if (layer == 7)
        {
            LevelManager.Instance.Next();
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
