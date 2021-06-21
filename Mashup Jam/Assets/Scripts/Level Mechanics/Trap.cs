using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
	private float interval = 1f; //how many seconds per interval
	[SerializeField]
	private float speed = 0.5f; //how many second switching takes
	[SerializeField]
	private	GameObject UpTrap;

	private Vector3 upPos;
	private Vector3 downPos;
	
	private bool isTrapUp = true;
	
	private Collider2D collider;
	
	//timer variables
	private float timeToChange;
	private float lerpTime = 0;

	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		upPos = UpTrap.transform.position;
		downPos = upPos + new Vector3(0, -1, 0);
		timeToChange = Time.time + interval;
		collider = UpTrap.GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
	    if (Time.time >= timeToChange)
	    {
	    	if (isTrapUp)
	    	{
	    		LowerTrap();
	    	} else
	    	{
	    		RaiseTrap();
	    	}
	    	
	    }
    }
    
	private void LowerTrap()
	{
		if (lerpTime < speed)
		{
			var newPos = Vector3.Lerp(upPos, downPos, lerpTime / speed);
			UpTrap.transform.position = newPos;
			lerpTime += Time.fixedDeltaTime;
			return;
		}
		collider.enabled = false;
		lerpTime = 0;
		timeToChange = Time.time + interval;
		isTrapUp = false;
	}
	
	private void RaiseTrap()
	{
		if (lerpTime < speed)
		{
			var newPos = Vector3.Lerp(downPos, upPos, lerpTime / speed);
			UpTrap.transform.position = newPos;
			lerpTime += Time.fixedDeltaTime;
			return;
		}
		collider.enabled = true;
		lerpTime = 0;
		timeToChange = Time.time + interval;
		isTrapUp = true;
	}
}
