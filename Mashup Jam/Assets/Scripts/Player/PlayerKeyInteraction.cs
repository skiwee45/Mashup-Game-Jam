using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerKeyInteraction : MonoBehaviour
{
	[Tag]
	[SerializeField]
	private string keyTag;
	
    // Sent when an incoming collider makes contact with this object's collider (2D physics only).
	protected void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		Debug.Log(collisionInfo.gameObject.name);
		if (collisionInfo.gameObject.CompareTag(keyTag))
		{
			KeyDoorManager.Instance.AddKey(collisionInfo.gameObject.name);
			collisionInfo.gameObject.SetActive(false);
		}
	}
}
