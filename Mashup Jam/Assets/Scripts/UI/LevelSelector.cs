using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private int lastLevel;
    [SerializeField]
    private Text levelSelection;
    public void Forward()
    {
        change(+1);
    }

    public void Backward()
    {
        change(-1);
    }

    private void change(int change)
    {
        int oldLevel = int.Parse(levelSelection.text);
        int newLevel = oldLevel + change;
        if(newLevel < 1 || newLevel > lastLevel) return;
        levelSelection.text = newLevel.ToString();
    }
}
