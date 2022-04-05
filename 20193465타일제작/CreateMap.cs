using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject Tile;
    public int Tilenum1 = 3;//행
    public int Tilenum2 = 3;//열

    public GameObject[] CreatedTile; //만들어진 타일을 저장하는 배열

    void Start()
    {
        CreatedTile = new GameObject[Tilenum1 * Tilenum2];
        int CreatedTileIndex = 0;
        int xPos = 0;
        int zPos = 0;

        for (int i=0; i < Tilenum1; i++)
        {
            zPos = 0;

            for (int k = 0; k < Tilenum2; k++) 
            {
                GameObject CreatedT= Instantiate(Tile, new Vector3(xPos,0,zPos),Quaternion.identity);
                CreatedTile[CreatedTileIndex] = CreatedT;

                CreatedTileIndex++;
                zPos += 5;
            }
            xPos += 5;
        }
    }

    void Update()
    {

    }
}
