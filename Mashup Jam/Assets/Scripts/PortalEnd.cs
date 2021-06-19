using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnd : MonoBehaviour
{
	public PortalStart portalStart;
	
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(portalStart.playerTag))
		{
			portalStart.EndTeleport();
		}
	}
}
