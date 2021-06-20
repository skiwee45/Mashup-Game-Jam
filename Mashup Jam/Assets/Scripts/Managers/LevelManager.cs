using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private int level = 1;
    // Start is called before the first frame update
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
        SceneManager.LoadScene(level);
    }
}
