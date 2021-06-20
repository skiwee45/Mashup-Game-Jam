using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    private static int level;

    public void loadLevel(Text text)
    {
        level = int.Parse(text.text);
        LoadLevel();
    }

    public void Restart()
    {
        LoadLevel();
    }

    public void Next()
    {
        level++;
        LoadLevel();
    }

    private void LoadLevel()
	{
		GetComponent<KeyDoorManager>().Reset();
	    SceneManager.LoadScene(level + 1);
    }
}
