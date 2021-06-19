using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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
	
	//runtime
	private bool fullPortalPlaced = true;
	private Camera mainCamera;
	private GameObject startPortal;
	private GameObject startPortalPlaceholder;
	private GameObject endPortal;
	private GameObject endPortalPlaceholder;
	
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
		GameObject placeholder;
		if (fullPortalPlaced)
		{
			placeholder = startPortalPlaceholder;
		} else
		{
			placeholder = endPortalPlaceholder;
			pos = pos.Clamp(Vector2.negativeInfinity, new Vector2(startPortal.transform.position.x, Mathf.Infinity));
		}
		
		placeholder.transform.position = pos;
	}
	
	private void PlacePortal()
	{
		if (fullPortalPlaced) //start portal
		{
			//destroy last portal
			Destroy(startPortal);
			startPortal = null;
			Destroy(endPortal);
			endPortal = null;
			
			//create new portal
			startPortalPlaceholder.SetActive(false);
			startPortal = Instantiate(startPortalPrefab, startPortalPlaceholder.transform.position, Quaternion.identity);
			
			//update runtime variables
			fullPortalPlaced = false;
			endPortalPlaceholder.SetActive(true);
		} else //end portal
		{
			//create new portal
			endPortalPlaceholder.SetActive(false);
			endPortal = Instantiate(endPortalPrefab, endPortalPlaceholder.transform.position, Quaternion.identity);
			
			//add endportal to startportal
			startPortal.GetComponent<PortalStart>().portalEnd = endPortal;
			
			//update runtime variables
			fullPortalPlaced = true;
			startPortalPlaceholder.SetActive(true);
		}
	}
}
