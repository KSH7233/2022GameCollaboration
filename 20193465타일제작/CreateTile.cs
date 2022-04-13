using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTile : MonoBehaviour
{
    public GameObject TilePrefab;
    public int xNum = 3; //��
    public int zNum = 3; //��

    public static GameObject[] CreatedTile;

    private float xPos = 0; //�Ҵ��� ������ ũ�⸦ �ӽ÷� �����ϴ� ����
    private float zPos = 0;

    void Start()
    {
        CreatedTile = new GameObject[xNum * zNum]; //�࿭ ���� ��ŭ�� �迭 ������ �Ҵ��Ѵ�. 
        int CreatedTileIndex = 0;

        Tile tile = GameObject.Find("TilePrefab").GetComponent<Tile>();
       
        for (int x=0; x<xNum;x++)
        {
            zPos=0; //�ʱ�ȭ

            for (int y=0;y<xNum;y++)
            {
                GameObject CreatedT = Instantiate(TilePrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
                CreatedTile[CreatedTileIndex] = CreatedT;

                CreatedTileIndex++;
                zPos += tile.Tilezsize;
            }
            xPos += tile.Tilexsize; //�� �̵�
        }
    }

    void Update()
    {
        
    }
}
