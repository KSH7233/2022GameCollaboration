using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� �� �湮�ߴ� Ÿ�Ͽ� �� �� �ִ� ������
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
