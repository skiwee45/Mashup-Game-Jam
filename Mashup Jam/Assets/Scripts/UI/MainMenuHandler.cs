using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject nav;

    [SerializeField]
    private string mainMenu;

    private static GameObject og;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // nav.SetActive(false);

        SceneManager.LoadScene(mainMenu);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool menu = scene.name == mainMenu;
        // nav.SetActive(!menu);
    }
}
