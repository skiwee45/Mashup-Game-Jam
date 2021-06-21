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
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name == "UpTrap")
            {
                SoundController.Instance.PlaySound(SoundController.Sound.Spike);
            } else {
                SoundController.Instance.PlaySound(SoundController.Sound.Death);
            }

            Respawn();
        }
        else if (layer == 7)
        {
            SoundController.Instance.PlaySound(SoundController.Sound.Win);
            LevelManager.Instance.Next();
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < -15)
        {
            SoundController.Instance.PlaySound(SoundController.Sound.Death);
            Respawn();
        }
    }

    void Respawn()
    {
        LevelManager.Instance.Restart();
    }
}
