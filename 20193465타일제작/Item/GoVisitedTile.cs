using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//한 번 방문했던 타일에 갈 수 있는 아이템
public class GoVisitedTile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            CharacterInfo characterInfo = gameObject.GetComponent<CharacterInfo>();
            characterInfo.IsGet_GovisitedTile_item = true;
            Destroy(gameObject);
        }
    }
}
