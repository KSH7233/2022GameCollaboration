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

    public int goldnum; //���
    public Material[] mats;

    GameObject g; //��üȭ�� �� ������Ʈ�� �����ϴ� ����

    private Material mat;
    private void Start()
    {
        mats = gameObject . GetComponent<MeshRenderer>().materials;
        mat=gameObject.GetComponent<MeshRenderer>().material;

        SetGold();
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
        if (0 < transform.childCount) //�� 0����...? �����ѹ�. �ڽĿ�����Ʈ�� ������ ����ǵ��� �Ѱ�����
        {
            transform.GetChild(0).gameObject.SetActive(activate); //place��  �¿����� notice�� �����ϴ��� �ؾ���
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

        if (ran < 3)
        {
            //��ȭ 1�� i��° Ÿ�� ���� ����
            g = Instantiate(GameManager.instance.Gold_sprites[0], goldposition, Quaternion.Euler(new Vector3(0, 70, 0)));
            g.transform.parent = gameObject.transform;
            goldnum = 1;
        }
        else if (ran < 5)
        {
            //��ȭ 2�� i��° Ÿ�� ���� ����
            g=Instantiate(GameManager.instance.Gold_sprites[1], goldposition, Quaternion.Euler(new Vector3(0, 70, 0)));
            g.transform.parent = gameObject.transform;
            goldnum = 2;
        }
        else if (ran < 6)
        {
            //��ȭ 3�� i��° Ÿ�� ���� ����
            g=Instantiate(GameManager.instance.Gold_sprites[2], goldposition, Quaternion.Euler(new Vector3(0, 70, 0)));
            g.transform.parent = gameObject.transform;
            goldnum = 3;
        }
    }

    private void OnMouseUpAsButton() //�߰��Ұ�
    {
        Debug.Log("place is clicked");
        GameManager.instance.SetLastClickedPlace(this);
        Debug.Log("LastClicked : " + GameManager.instance.LastClickedPlace);
        GameManager.instance.UnitStepSelection(this);

        Destroy(g);

        NoticeControl(false);

        gameObject.GetComponent<MeshRenderer>().material = mats[1];

        visited = true;
    }

}
