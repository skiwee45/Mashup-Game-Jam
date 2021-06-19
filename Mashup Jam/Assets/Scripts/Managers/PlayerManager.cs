using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Player").transform.position;
        Debug.Log(spawn);
    }

    // Update is called once per frame
    public void Respawn()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = spawn;
    }
}
