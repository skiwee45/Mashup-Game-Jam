using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Vector3 spawn;
    GameObject player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        spawn = player.transform.position;
        Debug.Log(spawn);
    }
    public void Respawn()
    {
        // player.transform.position = spawn;
    }
}