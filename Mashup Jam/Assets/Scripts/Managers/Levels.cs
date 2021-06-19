using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : Singleton<Levels>
{
    private int level; 
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
	    DontDestroyOnLoad(gameObject);
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() {
        LoadLevel();
    }

    private void LoadLevel() {
        SceneManager.LoadScene("Level" + level);
    }
}
