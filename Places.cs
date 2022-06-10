using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Places : MonoBehaviour //���߿� �ڽ� Ŭ������ ������ �� ���� Ŭ����
{
    //�� Ŭ������ ����� �θ� ������ƮPlaces�� ��������
    [SerializeField] Place tilePrefab;

    private List<Place> places = new List<Place>();
    public Place[] Place { get { return places.ToArray(); } }

    [SerializeField] public int widthSize;
    public int WidthSize { get => widthSize; }

    public void TileSetting()
    {

        int numbering = 0;

        for (int z = 0; widthSize > z; z++)
        {
            for (int x = 0; widthSize > x; x++)
            {
                Place tile = Instantiate(tilePrefab);

                tile.SetPos(x * 10, 0, -z * 10);

                tile.transform.parent = gameObject.transform;

                tile.ChildNumbering(numbering++);

                places.Add(tile);
            }
        }


        GameManager.instance.Place4DirectionCheck(widthSize); //����� ���� �Ұ�
    }


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
        //Debug.Log("None");
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

    public void AllPlaceNoticeOn()
    {
        for (int i = 0; i < places.Count; i++)
        {
            Place[i].NoticeControl(true); //with boxcollider on
        }

        Debug.Log("all on");
    }

}
