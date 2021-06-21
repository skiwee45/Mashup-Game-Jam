using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PortalStart : MonoBehaviour
{
	//config
	[Tag]
	public string playerTag;
	[Tag]
	public string teleportingPlayerTag;
	public string playerName= "Player";
	[Layer]
	public int teleportingPlayerLayer;
	private GameObject portalEnd;
	public GameObject PortalEnd
	{
		get => portalEnd;
		set {
			portalEnd = value;
			//check if already has player inside it
			if (originalPlayer == null)
			{
				return;
			}
			if (GetComponent<BoxCollider2D>().IsTouching(originalPlayer.GetComponent<Collider2D>()))
			{
				StartTeleport();
			}
		}
	}
	private float width;
	
	//runtime
	private GameObject originalPlayer = null; //set to null if not in the middle of teleport
	private GameObject playerCopy;
	private GameObject teleportingPlayer = null;

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		width = transform.localScale.x;
	}

	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(playerTag))
		{
			originalPlayer = other.gameObject;
			StartTeleport();
		}
	}
	
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(teleportingPlayerTag))
		{
			teleportingPlayer = other.gameObject;
			EndTeleport();
		}
	}
	
	// This function is called when the MonoBehaviour will be destroyed.
	protected void OnDestroy()
	{
		EndTeleport();
		CleanupPortal();
		Destroy(portalEnd);
	}
	
	private void StartTeleport()
	{
		if (portalEnd == null)
		{
			return;
		}
		//position player
		var deltaPos = new Vector3(originalPlayer.transform.position.x - (transform.position.x - (width / 2f)), 0, 0);
		var portalEndEdgePosition = portalEnd.transform.position + Vector3.right	 * (width / 2f);
		
		//spawn copy on the other side with same velocity
		var playerRB = originalPlayer.GetComponent<Rigidbody2D>();
		playerCopy = Instantiate(originalPlayer, portalEndEdgePosition + deltaPos, Quaternion.identity);
		playerCopy.GetComponent<Rigidbody2D>().velocity = playerRB.velocity;
		
		//make original player have no vertical velocity and pass through objects
		playerRB.gravityScale = 0;
		playerRB.velocity *= Vector2.right;
		originalPlayer.tag = teleportingPlayerTag;
		originalPlayer.layer = teleportingPlayerLayer;

        SoundController.PlaySound(SoundController.Sound.Portal);
    }
	
	/// <summary>
	/// Called by PortalEnd when the player exits
	/// </summary>
	public void EndTeleport()
	{
		//delete this player=
		Destroy(teleportingPlayer);
			
		//change name of copy
		if (playerCopy != null)
		{
			playerCopy.name = playerName;
		}
	}
	
	public void CleanupPortal()
	{
		var players = GameObject.FindGameObjectsWithTag(teleportingPlayerTag);
		foreach (var player in players)
		{
			Destroy(player);
		}
	}
}
