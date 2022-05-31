using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SpriteFlipX(bool isFlip)
    {
        if (gameObject.activeSelf)
            sprite.flipX = isFlip;

        
    }

}
