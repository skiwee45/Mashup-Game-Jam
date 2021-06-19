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
	private bool fullPortalPlaced = false;
	private Camera mainCamera;
	private GameObject startPortalPlaceholder;
	private GameObject endPortalPlaceholder;
	private GameObject startPortal;
	private GameObject endPortal;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		startPortalPlaceholder = Instantiate(startPortalPlaceholderPrefab);
		endPortalPlaceholder = Instantiate(endPortalPlaceholderPrefab);
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
		//call method
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
		if (fullPortalPlaced)
		{
			startPortalPlaceholder.SetActive(false);
		}
		//TODO: Replace placeholder with real portal
		//TODO: Change runtime variabes
		//TODO: SetActive the other placeholder
	}
}
