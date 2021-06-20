using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Door : MonoBehaviour
{
	[Tag]
	[SerializeField]
	private string playerTag;
	
	[SerializeField]
	private GameObject key;
	
	// Sent when an incoming collider makes contact with this object's collider (2D physics only).
	protected void OnCollisionEnter2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.CompareTag(playerTag))
		{
			if (KeyDoorManager.Instance.GetKeyStatus(key.name))
			{
				OpenDoor();
			}
		}
	}
	
	private void OpenDoor()
	{
		GetComponent<BoxCollider2D>().enabled = false;
		var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		var newColor = new Color(1, 1, 1, 125f/255f);
		foreach (var spriteRenderer in spriteRenderers)
		{
			spriteRenderer.color = newColor;
		}
	}
}
