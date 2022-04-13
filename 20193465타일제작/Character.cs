using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CreateMap c;

    void Start()
    {
        
        gameObject.transform.position = c.CreatedTile[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
