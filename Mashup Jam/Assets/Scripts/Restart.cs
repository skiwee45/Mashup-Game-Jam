using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame() {
        Debug.Log("HI");
        SceneManager.LoadScene("Level1");
    }
}
