using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab; //타일 프리팹을 받는 변수
    public GameObject PlayerPrefab; //캐릭터 프리팹을 받는 변수

    [SerializeField] public static GameObject[] CreatedTile;

    [SerializeField] private int xNum = 3; //타일 행
    [SerializeField] private int zNum = 3; //타일 열

    //static으로 할 것이기 때문에, 프리팹은 다른 변수에서 받음.
    public static GameObject Player1;

    void Start()
    {
        CreateTiles();
        Player1 = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        //Instantiate(Player2, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        
    }

    private void CreateTiles()
    {
        float xPos = 0; //할당할 발판의 크기를 임시로 저장하는 변수
        float zPos = 0;

        CreatedTile = new GameObject[xNum * zNum]; //행열 곱한 만큼의 배열 공간을 할당한다. 
        int CreatedTileIndex = 0;

        Tile tile = GameObject.Find("TilePrefab").GetComponent<Tile>();

        for (int x = 0; x < xNum; x++)
        {
            zPos = 0; //초기화

            for (int y = 0; y < zNum; y++)
            {
                GameObject CreatedT = Instantiate(TilePrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
                CreatedTile[CreatedTileIndex] = CreatedT;

                CreatedTileIndex++;
                zPos += tile.Tilezsize;
                Debug.Log(zPos);
            }
            xPos += tile.Tilexsize; //행 이동
        }
    }
}
