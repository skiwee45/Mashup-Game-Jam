using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private int level = 1; 
    // Start is called before the first frame update
    void Start()
    {
	    DontDestroyOnLoad(gameObject);
    }

    public void startGame() {
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
        Debug.Log("Loading New Level");
        SceneManager.LoadScene("Level" + level);
    }
}
