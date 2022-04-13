using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    BoxCollider BoxCollide;

    public float Tilexsize = 0;
    public float Tilezsize = 0;
    public bool IsVisited = false;

    private GameObject player;
    

    void Start()
    {
        BoxCollide = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        Tilexsize = BoxCollide.bounds.size.x;
        Tilezsize = BoxCollide.bounds.size.z;
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        if(IsVisited==true)
        {
            return;
        }
        SetPlayerPosition();

    }

    void SetPlayerPosition()
    {
        player.transform.position = gameObject.transform.position;
        IsVisited = true;
    }
}
