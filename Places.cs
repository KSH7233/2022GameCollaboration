using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Places : MonoBehaviour //나중에 자식 클래스를 가지게 될 원형 클래스
{
    //이 클래스는 장소의 부모 오브젝트Places에 넣을것임


    [SerializeField] private List<Place> places = new List<Place>();
    public Place[] Place { get { return places.ToArray(); } }


    //플레이어는 어느 place에 있는가
    public Place FindPlayerPlace(Vector3 playerStand)
    {

        for (int i = 0; i < places.Count; i++)
        {
            var pos = places[i].transform.position;
            
            if (Vector3.Distance(playerStand ,pos) <= 0.001f)
            {
                return places[i];
                Debug.Log(places[i]);
            }
        }
        Debug.Log("None");
        return null;
        
    }

    public Place PlaceFind(int placeNum)
    {
        return Place[placeNum];
    }

    public int PlaceCount()
    {
        return Place.Length;
    }

}
