using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject item1;

    private bool Issetitem = false;

    private void Update()
    {
        //타일이 게임 시작 시 생성되기 때문에, start함수에서 아래 코드를 실행하면
        //tilenum을 구하는 코드에서 null오류가 생김.
        //타일을 미리 구성하고 배열로 관리한다면, 아래 코드들을 start문에 넣어도 괜찮음.
        if(Issetitem==false)
        {
            int tilenum = GameManager.CreatedTile.Length;

            //타일을 돌아가면서 아이템 생성
            for (int i = 0; i < tilenum; i++)
            {
                int Randnum = Random.Range(0, 6); //0~5의 숫자 중 하나를 선택한다.

                if (Randnum == 1) // 1/5 확률로 i번째 타일 위에 아이템을 생성한다.
                {
                    Instantiate(item1, GameManager.CreatedTile[i].transform.position, Quaternion.identity);
                }
            }
            Issetitem = true;
        }
    }
}
