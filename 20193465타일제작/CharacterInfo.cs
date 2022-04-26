using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public bool IsGet_GovisitedTile_item = false;

    private GameObject clicked_tile; //Ŭ���� Ÿ���� �����ϴ� �Լ�

    void Update()
    {
        //if�� ��ø�� ���Ƽ� �Ŀ� �����غ���.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag=="Tile")
                {
                    clicked_tile = hit.transform.gameObject;
                }
            }
            //�������� ���� �����̸鼭, Ŭ���� Ÿ���� �� �� �湮�ߴ� Ÿ���̶��
            if(IsGet_GovisitedTile_item==true && clicked_tile.GetComponent<Tile>().IsVisited==true)
            {
                gameObject.transform.position = clicked_tile.transform.position; //Ŭ���� Ÿ�Ϸ� ����.
                IsGet_GovisitedTile_item = false;
            }
        }
    }
}
