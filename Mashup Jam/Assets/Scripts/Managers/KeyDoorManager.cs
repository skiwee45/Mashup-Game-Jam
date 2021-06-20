using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KeyDoorManager : Singleton<KeyDoorManager>
{
	private List<string> keys;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		keys = new List<string>();
	}
	
	public void AddKey(string keyName)
	{
		keys.Add(keyName);

	}
	
	public bool GetKeyStatus(string keyName)
	{
		return keys.Contains(keyName);
	}
	
	public void RemoveKey(string keyName)
	{
		keys.Remove(keyName);
	}
	
	public void Reset()
	{
		keys.Clear();
	}
}