using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayScene : MonoBehaviour
{
    public GameObject imageUI;
    public GameObject gamestartbutton;
    public Sprite[] sprites;

    Image image;

    int index = 0;

    void Start()
    {
        image = imageUI.GetComponent<Image>();
        image.sprite = sprites[index];
    }

    public void ChangeSprite()
    {
        index += 1;
        image.sprite = sprites[index];
    }

    private void Update()
    {
        if (index == sprites.Length - 1) 
        {
            index = 0;
            gamestartbutton.SetActive(true);
            Destroy(gameObject);
        }
    }
}
