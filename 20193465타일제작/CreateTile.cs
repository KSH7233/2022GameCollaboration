using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTile : MonoBehaviour
{
    public GameObject TilePrefab;
    public int xNum = 3; //행
    public int zNum = 3; //열

    public static GameObject[] CreatedTile;

    private float xPos = 0; //할당할 발판의 크기를 임시로 저장하는 변수
    private float zPos = 0;

    void Start()
    {
        CreatedTile = new GameObject[xNum * zNum]; //행열 곱한 만큼의 배열 공간을 할당한다. 
        int CreatedTileIndex = 0;

        Tile tile = GameObject.Find("TilePrefab").GetComponent<Tile>();
       
        for (int x=0; x<xNum;x++)
        {
            zPos=0; //초기화

            for (int y=0;y<xNum;y++)
            {
                GameObject CreatedT = Instantiate(TilePrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
                CreatedTile[CreatedTileIndex] = CreatedT;

                CreatedTileIndex++;
                zPos += tile.Tilezsize;
            }
            xPos += tile.Tilexsize; //행 이동
        }
    }

    void Update()
    {
        
    }
}
