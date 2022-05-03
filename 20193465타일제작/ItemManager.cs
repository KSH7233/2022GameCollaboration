using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject item1;

    private bool Issetitem = false;

    private void Update()
    {
        //Ÿ���� ���� ���� �� �����Ǳ� ������, start�Լ����� �Ʒ� �ڵ带 �����ϸ�
        //tilenum�� ���ϴ� �ڵ忡�� null������ ����.
        //Ÿ���� �̸� �����ϰ� �迭�� �����Ѵٸ�, �Ʒ� �ڵ���� start���� �־ ������.
        if(Issetitem==false)
        {
            int tilenum = GameManager.CreatedTile.Length;

            //Ÿ���� ���ư��鼭 ������ ����
            for (int i = 0; i < tilenum; i++)
            {
                int Randnum = Random.Range(0, 6); //0~5�� ���� �� �ϳ��� �����Ѵ�.

                if (Randnum == 1) // 1/5 Ȯ���� i��° Ÿ�� ���� �������� �����Ѵ�.
                {
                    Instantiate(item1, GameManager.CreatedTile[i].transform.position, Quaternion.identity);
                }
            }
            Issetitem = true;
        }
    }
}
