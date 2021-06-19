using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private int level; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HI");
        level = 1;
	    DontDestroyOnLoad(gameObject);
        LoadLevel();
    }

    public void Restart() {
        LoadLevel();
    }

    public void Next() {
        level++;
        LoadLevel();
    }

    private void LoadLevel() {
        SceneManager.LoadScene("Level" + level);
    }
}
