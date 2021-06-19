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
	public GameObject portalEnd;
	private float width;
	
	//runtime
	private GameObject originalPlayer = null; //set to null if not in the middle of teleport
	private GameObject playerCopy;

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		width = GetComponent<BoxCollider2D>().size.x;
	}

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
		if (portalEnd == null)
		{
			return;
		}
		originalPlayer = player;			
		//spawn copy on the other side with same velocity
		playerCopy = Instantiate(originalPlayer, portalEnd.transform.position, Quaternion.identity);
		playerCopy.GetComponent<Rigidbody2D>().velocity = originalPlayer.GetComponent<Rigidbody2D>().velocity;
	}
	
	private void EndTeleport()
	{
		if (originalPlayer == null)
		{
			return;
		}
		//delete this player
		var name = originalPlayer.name;
		Destroy(originalPlayer);
		originalPlayer = null;
			
		//change name of copy
		playerCopy.name = name;
	}
}
