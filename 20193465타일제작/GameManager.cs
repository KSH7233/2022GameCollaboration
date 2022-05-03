using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab; //Ÿ�� �������� �޴� ����
    public GameObject PlayerPrefab; //ĳ���� �������� �޴� ����

    [SerializeField] public static GameObject[] CreatedTile;

    [SerializeField] private int xNum = 3; //Ÿ�� ��
    [SerializeField] private int zNum = 3; //Ÿ�� ��

    //static���� �� ���̱� ������, �������� �ٸ� �������� ����.
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
        float xPos = 0; //�Ҵ��� ������ ũ�⸦ �ӽ÷� �����ϴ� ����
        float zPos = 0;

        CreatedTile = new GameObject[xNum * zNum]; //�࿭ ���� ��ŭ�� �迭 ������ �Ҵ��Ѵ�. 
        int CreatedTileIndex = 0;

        Tile tile = GameObject.Find("TilePrefab").GetComponent<Tile>();

        for (int x = 0; x < xNum; x++)
        {
            zPos = 0; //�ʱ�ȭ

            for (int y = 0; y < zNum; y++)
            {
                GameObject CreatedT = Instantiate(TilePrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
                CreatedTile[CreatedTileIndex] = CreatedT;

                CreatedTileIndex++;
                zPos += tile.Tilezsize;
                Debug.Log(zPos);
            }
            xPos += tile.Tilexsize; //�� �̵�
        }
    }
}
