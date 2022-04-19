using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Places : MonoBehaviour //���߿� �ڽ� Ŭ������ ������ �� ���� Ŭ����
{
    //�� Ŭ������ ����� �θ� ������ƮPlaces�� ��������


    [SerializeField] private List<Place> places = new List<Place>();
    public Place[] Place { get { return places.ToArray(); } }


    //�÷��̾�� ��� place�� �ִ°�
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

    public void AllPlaceNoticeOff()
    {
        for (int i = 0; i < places.Count; i++)
        {
            Place[i].NoticeControl(false); //with boxcollider off
        }

        Debug.Log("all off");
    }

}
