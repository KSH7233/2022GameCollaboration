using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    BoxCollider BoxCollide;
    new Renderer renderer;

    public float Tilexsize = 0;
    public float Tilezsize = 0;
    public bool IsVisited = false;

    private GameObject player;  //�������� �ν����� â�� ������Ʈ ��ü�� �Ҵ��� �� ����

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        BoxCollide = GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player");

        Tilexsize = BoxCollide.bounds.size.x;
        Tilezsize = BoxCollide.bounds.size.z;
    }

    private void OnMouseDown()
    {
        if(IsVisited==true)
        {
            Debug.Log("�� Ÿ���� �Ծ��� Ÿ�� �Դϴ�.");
        }
        else if(IsVisited==false)
        {
            SetPlayerPosition();
        }
    }

    void SetPlayerPosition()
    {
        player.transform.position = gameObject.transform.position;
        IsVisited = true;
        renderer.material.color = Color.red;
    }
}
