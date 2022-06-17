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

    private int childNum;
    public int ChildNum { get => childNum; }

    int goldnum = 0; //경민
    public int GoldNum { get => goldnum; }

    public Material[] mats;

    GameObject g; //객체화된 금 오브젝트를 저장하는 변수

    private Material mat;
    private void Start()
    {
        mats = gameObject . GetComponent<MeshRenderer>().materials;
        mat=gameObject.GetComponent<MeshRenderer>().material;

        SetGold();
    }

    public void SetPos(int x, int y, int z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void ChildNumbering(int num)
    {
        childNum = num;
    }


    public void Set4Direction(Place u, Place d, Place l, Place r)
    {
        up = u;
        down = d;
        left = l;
        right = r;
    }

    public void NoticeControl(bool activate) //with boxcollider
    {
        if (0 < transform.childCount) //왜 0인지...? 매직넘버. 자식오브젝트가 있으면 실행되도록 한것인지
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

    public void ChangeVisitedTileToUnvisitedTile()
    {
        gameObject.GetComponent<MeshRenderer>().material = mats[0];
    }

    public void SetGold()
    {
        int ran = Random.Range(0, 6);
        Vector3 goldposition = gameObject.transform.position;
        goldposition.y += 3;

        if (ran < 3)//50%
        {
            //재화 1개 i번째 타일 위에 생성
            g = Instantiate(GameManager.instance.Gold_sprites[0], goldposition, Quaternion.Euler(new Vector3(0, 70, 0)));
            g.transform.parent = gameObject.transform;
            goldnum = 1;
        }
        else if (ran < 5)//34%
        {
            //재화 2개 i번째 타일 위에 생성
            g=Instantiate(GameManager.instance.Gold_sprites[1], goldposition, Quaternion.Euler(new Vector3(0, 70, 0)));
            g.transform.parent = gameObject.transform;
            goldnum = 2;
        }
        else if (ran < 6)//16%
        {
            //재화 3개 i번째 타일 위에 생성
            g=Instantiate(GameManager.instance.Gold_sprites[2], goldposition, Quaternion.Euler(new Vector3(0, 70, 0)));
            g.transform.parent = gameObject.transform;
            goldnum = 3;
        }
    }

    private void OnMouseUpAsButton() //추가할것
    {
        GameManager.instance.SetLastClickedPlace(this);
        Debug.Log("LastClicked : " + GameManager.instance.LastClickedPlace);
        GameManager.instance.UnitStepSelection(this); //move

        Destroy(g);

        NoticeControl(false);

        gameObject.GetComponent<MeshRenderer>().material = mats[1];

        visited = true;

        GameManager.instance.IsolCheck();
    }

    public void AIclicked()
    {
        GameManager.instance.SetLastClickedPlace(this);
        Debug.Log("LastClicked : " + GameManager.instance.LastClickedPlace);
        GameManager.instance.UnitStepSelection(this); //move

        Destroy(g);

        NoticeControl(false);

        gameObject.GetComponent<MeshRenderer>().material = mats[1];

        visited = true;

        GameManager.instance.IsolCheck();
    }

    public void SetGoldZero()
    {
        goldnum = 0;
    }

    private void Update()
    {
        if(g != null)
        {
            g.transform.Rotate(new Vector3(0, 50 * Time.deltaTime, 0));
        }
        
    }

}
