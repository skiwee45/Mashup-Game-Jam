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

    public void startGame()
    {
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
        Debug.Log("Loading New Level");
        IEnumerator coroutine = LoadScene();
        StartCoroutine(coroutine);
    }

    private IEnumerator LoadScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Level" + level, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called

        PlayerManager.Instance.SetSpawnPoint();
        yield return new WaitForEndOfFrame();
    }
}
