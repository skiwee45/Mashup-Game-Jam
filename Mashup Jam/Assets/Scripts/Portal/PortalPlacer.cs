using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Tilemaps;

/// <summary>
/// Takes mouse input and creates portals
/// </summary>
public class PortalPlacer : MonoBehaviour
{
	//config
	[SerializeField]
	private GameObject startPortalPrefab;
	[SerializeField]
	private GameObject startPortalPlaceholderPrefab;
	[SerializeField]
	private GameObject endPortalPrefab;
	[SerializeField]
	private GameObject endPortalPlaceholderPrefab;
	[SerializeField]
	[ReorderableList]
	private Tilemap[] allTileMaps;
	
	//runtime
	private bool fullPortalPlaced = true;
	private Camera mainCamera;
	private GameObject startPortal;
	private GameObject startPortalPlaceholder;
	private GameObject endPortal;
	private GameObject endPortalPlaceholder;
	private GameObject currentPlaceholder;
	private bool canPlacePortal = true;
	
	//debug
	private Vector2[] posChecks;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		startPortalPlaceholder = Instantiate(startPortalPlaceholderPrefab);
		startPortalPlaceholder.SetActive(true);
		endPortalPlaceholder = Instantiate(endPortalPlaceholderPrefab);
		endPortalPlaceholder.SetActive(false);
	}
	
    // Start is called before the first frame update
    void Start()
    {
	    mainCamera = Camera.main;
	    InputManager.Instance.mousePosition.AddListener(SetMousePosition);
	    InputManager.Instance.placePortal.AddListener(SetPlacePortal);
    }

	//Input getters
	public void SetMousePosition(Vector2 pos)
	{
		var worldPos = mainCamera.ScreenToWorldPoint(pos);
		PositionPortal(worldPos);
	}
	
	public void SetPlacePortal()
	{
		PlacePortal();
	}
	
	/// <summary>
	/// Positions placeholders to the right place
	/// </summary>
	/// <param name="pos"></param>
	private void PositionPortal(Vector2 pos)
	{
		if (fullPortalPlaced)
		{
			currentPlaceholder = startPortalPlaceholder;
		} else
		{
			currentPlaceholder = endPortalPlaceholder;
			if (CheckRectangleOverlap(
					startPortal.transform.position, startPortal.transform.localScale, 
					pos, startPortal.transform.localScale) || 
			    CheckLineOverlap(pos.y, startPortal.transform.localScale.y, 
				startPortal.transform.position.y, startPortal.transform.localScale.y) 
			    && pos.x > startPortal.transform.position.x)
			{
				Vector2 relativePos = startPortal.transform.InverseTransformPoint(pos);
				var leftPos = new Vector2(-startPortal.transform.localScale.x, relativePos.y);
				pos = startPortal.transform.TransformPoint(leftPos);

				//pos = GetClosestPosition(pos, startPortal.transform, startPortal.transform.localScale);
			}			
			pos = pos.Clamp(Vector2.negativeInfinity, new Vector2(startPortal.transform.position.x, Mathf.Infinity));
		}
		
		var xOffset = fullPortalPlaced ? -0.5f : 0.5f;
		posChecks = new Vector2[]{new Vector2(pos.x + xOffset, pos.y), new Vector2(pos.x + xOffset, pos.y + 0.5f), new Vector2(pos.x + xOffset, pos.y - 0.5f)};
		currentPlaceholder.transform.position = pos;
		foreach (var map in allTileMaps)
		{
			if(CheckPointOverlapTilemap(posChecks[0], map) || 
				CheckPointOverlapTilemap(posChecks[1], map) || 
				CheckPointOverlapTilemap(posChecks[2], map))
			{
				TurnPlaceholderRed();
				canPlacePortal = false;
				return;
			}

		}
		TurnPlaceholderNormal();
		canPlacePortal = true;
	}
	
	private void PlacePortal()
	{
		if (!canPlacePortal)
		{
			return;
		}
		
		Vector2 pos = currentPlaceholder.transform.position;
		if (fullPortalPlaced) //start portal
		{
			//destroy last portal
			Destroy(startPortal);
			startPortal = null;
			Destroy(endPortal);
			endPortal = null;
			
			//create new portal
			startPortalPlaceholder.SetActive(false);
			startPortal = Instantiate(startPortalPrefab, pos, Quaternion.identity);

			//update runtime variables
			fullPortalPlaced = false;
			endPortalPlaceholder.SetActive(true);
		} else //end portal
		{
			//create new portal
			endPortalPlaceholder.SetActive(false);
			endPortal = Instantiate(endPortalPrefab, pos, Quaternion.identity);

			//add endportal to startportal
			startPortal.GetComponent<PortalStart>().PortalEnd = endPortal;
			
			//update runtime variables
			fullPortalPlaced = true;
			startPortalPlaceholder.SetActive(true);
		}
		PositionPortal(pos);
	}
	
	private static Vector2 GetClosestPosition(Vector2 value, Transform obstruction, Vector2 buffer)
	{
		Vector2 relativePos = obstruction.InverseTransformPoint(value);
		
		//find closest position
		var upPos = new Vector2(relativePos.x, buffer.y);
		var downPos = new Vector2(relativePos.x, -buffer.y);
		var rightPos = new Vector2(buffer.x, relativePos.y);
		var leftPos = new Vector2(-buffer.x, relativePos.y);
		List<Vector2> positions = new List<Vector2>() {upPos, downPos, leftPos};
		var shortestDistance = Vector2.Distance(relativePos, upPos);
		var closestPos = upPos;
		foreach (var pos in positions)
		{
			var dist = Vector2.Distance(relativePos, pos);
			if (dist < shortestDistance)
			{
				shortestDistance = dist;
				closestPos = pos;
			}
		}
		return obstruction.TransformPoint(closestPos);
	}

	private static bool CheckRectangleOverlap(Vector2 center1, Vector2 scale1, Vector2 center2,Vector2 scale2)
	{
		var horizontalOverlap = CheckLineOverlap(center1.x, scale1.x, center2.x, scale2.x);
		var verticalOverlap = CheckLineOverlap(center1.y, scale1.y, center2.y, scale2.y);
		return horizontalOverlap && verticalOverlap;
	}

	private static bool CheckLineOverlap(float center1, float scale1, float center2, float scale2)
	{
		var dist = Mathf.Abs(center1 - center2);
		if (dist > (scale1 / 2f + scale2 / 2f))
		{
			return false;
		}
		return true;
	}
	
	private static bool CheckPointOverlapTilemap(Vector2 worldPos, Tilemap map)
	{
		var cellPos = map.WorldToCell(worldPos);
		var output = map.HasTile(cellPos);
		if (output)
		{
			Debug.Log("Touching tile: " + map.GetTile(cellPos));
		}
		return map.HasTile(cellPos);
	}
	
	// Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn.
	protected void OnDrawGizmos()
	{
		if (posChecks == null)
		{
			return;
		}
		foreach (var pos in posChecks)
		{
			Gizmos.DrawSphere(pos, 0.1f);
		}
	}
	
	private void TurnPlaceholderRed()
	{
		currentPlaceholder.transform.GetChild(0).gameObject.SetActive(true);
	}
	private void TurnPlaceholderNormal()
	{
		currentPlaceholder.transform.GetChild(0).gameObject.SetActive(false);
	}
}
