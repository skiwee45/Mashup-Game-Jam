using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHandler : MonoBehaviour
{
    [SerializeField]
    private string menu;
    public void ToMenu()
    {
        SceneManager.LoadScene(menu);
    }
}
