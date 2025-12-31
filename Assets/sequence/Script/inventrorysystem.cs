using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventrorysystem : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer; // Reference to the target GameObject's SpriteRenderer
    public Sprite[] sprites; // Array to hold multiple sprites
    public GameObject[] background;
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("index", 0);
        SetActiveBackground();
    }
    public void Next()
    {
        index++;
        if (index >= background.Length)
            index = 0;
        SetActiveBackground();
    }
    public void Previous()
    {
        index--;
        if (index < 0)
            index = background.Length - 1;
        SetActiveBackground();
    }
    public void Selected()
    {
        if (index >= 0 && index < sprites.Length)
            targetSpriteRenderer.sprite = sprites[index];
    }
    void SetActiveBackground()
    {
        for (int i = 0; i < background.Length; i++)
        {
            background[i].SetActive(i == index);
        }
        PlayerPrefs.SetInt("index", index);
        PlayerPrefs.Save();
    }
}
