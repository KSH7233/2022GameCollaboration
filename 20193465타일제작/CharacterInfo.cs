using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public bool IsGet_GovisitedTile_item = false;

    private GameObject clicked_tile; //클릭한 타일을 저장하는 함수

    void Update()
    {
        //if문 중첩이 많아서 후에 수정해보기.
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
            //아이템을 먹은 상태이면서, 클릭한 타일이 한 번 방문했던 타일이라면
            if(IsGet_GovisitedTile_item==true && clicked_tile.GetComponent<Tile>().IsVisited==true)
            {
                gameObject.transform.position = clicked_tile.transform.position; //클릭한 타일로 가기.
                IsGet_GovisitedTile_item = false;
            }
        }
    }
}
