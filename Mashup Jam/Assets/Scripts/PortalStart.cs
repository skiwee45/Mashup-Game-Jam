using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PortalStart : MonoBehaviour
{
	//config
	[Tag]
	public string playerTag;
	private GameObject portalEnd;
	public GameObject PortalEnd
	{
		get => portalEnd;
		set {
			portalEnd = value;
			portalEnd.GetComponent<PortalEnd>().portalStart = this;
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
	
	// This function is called when the MonoBehaviour will be destroyed.
	protected void OnDestroy()
	{
		if (originalPlayer != null)
		{
			EndTeleport();
		}
		Destroy(portalEnd);
	}
	
	private void StartTeleport()
	{
		if (portalEnd == null)
		{
			return;
		}
		//position away from center of start portal
		var deltaPos = new Vector3(originalPlayer.transform.position.x - (transform.position.x - (width / 2f)), 0, 0);
		var portalEndEdgePosition = portalEnd.transform.position + Vector3.right	 * (width / 2f);
		Debug.Log(width);
		
		//spawn copy on the other side with same velocity
		playerCopy = Instantiate(originalPlayer, portalEndEdgePosition + deltaPos, Quaternion.identity);
		playerCopy.GetComponent<Rigidbody2D>().velocity = originalPlayer.GetComponent<Rigidbody2D>().velocity;
	}
	
	/// <summary>
	/// Called by PortalEnd when the player exits
	/// </summary>
	public void EndTeleport()
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
