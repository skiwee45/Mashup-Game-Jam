using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : Singleton<Levels>
{
    // Start is called before the first frame update
    void Start()
    {
        // SceneManager.LoadScene("Level1");
        // DontDestroyOnLoad();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart() {
        SceneManager.LoadScene("Level1");
    }
}
