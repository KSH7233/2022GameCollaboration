using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    BoxCollider BoxCollide;
    new Renderer renderer;

    public float Tilexsize = 3;
    public float Tilezsize = 3;
    public bool IsVisited = false;

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        BoxCollide = GetComponent<BoxCollider>();

        Tilexsize = BoxCollide.bounds.size.x;
        Tilezsize = BoxCollide.bounds.size.z;
    }

    private void OnMouseDown()
    {
        if(IsVisited==true)
        {
            Debug.Log("�� Ÿ���� �Ծ��� Ÿ�� �Դϴ�.");
            //�÷��̾ GoVIsitedTile �������� ���� ���¶��
            if(GameManager.Player1.GetComponent<CharacterInfo>().IsGet_GovisitedTile_item==true)
            {
                SetPlayerPosition();
                GameManager.Player1.GetComponent<CharacterInfo>().IsGet_GovisitedTile_item = false;
            }
        }
        else if(IsVisited==false)
        {
            SetPlayerPosition();
        }
    }

    void SetPlayerPosition()
    {
        GameManager.Player1.transform.position = gameObject.transform.position;
        IsVisited = true;
        renderer.material.color = Color.red;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Tile")
        {
            for(int i=0;i<collision.contacts.Length;i++)
            {
                Debug.Log(collision.contacts[i].normal); //�����
            }
        }
    }
}
