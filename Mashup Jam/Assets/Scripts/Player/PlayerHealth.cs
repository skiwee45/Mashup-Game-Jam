using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Hit");
        int layer = collision.gameObject.layer;
        // Debug.Log(layer);
        //dangerous layer
        if (layer == 6)
        {
            PlayerManager.Instance.Respawn();
        }
        else if (layer == 7)
        {
            // Debug.Log("Touched goal");
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
