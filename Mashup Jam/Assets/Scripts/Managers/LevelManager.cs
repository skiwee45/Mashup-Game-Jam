using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public static LevelManager i;

    [SerializeField]
    private int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Awake()
    {
         if (!i)
         {
             i = this;
             DontDestroyOnLoad(gameObject);
         }
         else
	         DestroyImmediate(gameObject);
    }

    public void loadLevel(int level)
    {
        this.level = level;
        LoadLevel();
    }

    public void loadLevel(Text text)
    {
        this.level = int.Parse(text.text);
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
        SceneManager.LoadScene("Level" + level);
    }
}
