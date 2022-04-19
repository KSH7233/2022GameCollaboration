using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private Place up;
    [SerializeField] private Place down;
    [SerializeField] private Place left;
    [SerializeField] private Place right;

    public Place Up { get { return up; } }
    public Place Down { get { return down; } }
    public Place Left { get { return left; } }
    public Place Right { get { return right; } }

    public bool visited /*{ get; private set; }*/ = false;

    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = Color.green;
    }


    public void Set4Direction(Place u, Place d, Place l, Place r)
    {
        up = u;
        down = d;
        left = l;
        right = r;
        Debug.Log(u);
        Debug.Log(d);
        Debug.Log(l);
        Debug.Log(r);
    }

    public void NoticeControl(bool activate) //with boxcollider
    {
        if (0 < transform.childCount)
        {
            transform.GetChild(0).gameObject.SetActive(activate); //place에  온오프할 notice를 삽입하던가 해야함
            gameObject.GetComponent<BoxCollider>().enabled = activate;
        }
    }

    public void VisitedNoticeOff()
    {
        if (visited)
            NoticeControl(false);
        else
            NoticeControl(true);
    }


    private void OnMouseUpAsButton() //추가할것
    {
        Debug.Log("place is clicked");
        GameManager.instance.SetLastClickedPlace(this);
        Debug.Log("LastClicked : " + GameManager.instance.LastClickedPlace);
        GameManager.instance.UnitStepSelection(this);

        NoticeControl(false);
        mat.color = Color.yellow;
        visited = true;
    }

}
