using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerManager : Singleton<PlayerManager>
{
    [Tag]
    public string playerTag;
    private Vector3 spawn;
    // Start is called before the first frame update
    public void SetSpawnPoint()
    {
        Debug.Log(playerTag);
        Debug.Log(GameObject.FindGameObjectWithTag(playerTag));
        spawn = GameObject.FindGameObjectWithTag(playerTag).transform.position;
    }

    // Update is called once per frame
    public void Respawn()
    {
        GameObject.FindGameObjectWithTag(playerTag).transform.position = spawn;
    }
}
