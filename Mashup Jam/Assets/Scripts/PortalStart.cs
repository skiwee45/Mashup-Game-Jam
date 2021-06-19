using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PortalStart : MonoBehaviour
{
	//config
	[SerializeField]
	[Tag]
	private string playerTag;
	
	[SerializeField]
	private GameObject portalEnd;
	
	//runtime
	private GameObject originalPlayer = null; //set to null if not in the middle of teleport
	private GameObject playerCopy;

	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(playerTag))
		{
			StartTeleport(other.gameObject);
		}
	}
	
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(playerTag))
		{
			EndTeleport();
		}
	}
	
	// This function is called when the MonoBehaviour will be destroyed.
	protected void OnDestroy()
	{
		if (originalPlayer != null)
		{
			EndTeleport();
		}
		Destroy(portalEnd);
	}
	
	private void StartTeleport(GameObject player)
	{
		originalPlayer = player;
		//position away from center of start portal
		var deltaPos = new Vector3(transform.position.x - originalPlayer.transform.position.x, 0, 0);
			
		//spawn copy on the other side with same velocity
		playerCopy = Instantiate(originalPlayer, portalEnd.transform.position + deltaPos, Quaternion.identity);
		playerCopy.GetComponent<Rigidbody2D>().velocity = originalPlayer.GetComponent<Rigidbody2D>().velocity;
	}
	
	private void EndTeleport()
	{
		//delete this player
		var name = originalPlayer.name;
		Destroy(originalPlayer.gameObject);
			
		//change name of copy
		playerCopy.name = name;
	}
}
