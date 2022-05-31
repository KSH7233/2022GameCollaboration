using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private int thisTurnPlayer = 0; // 0 ������ ����
    private int maxPlayerNum = 2;

    //private int selectedUnitNum = 0; //0 ~ 3
    //public int SelectedUnitNum { get => selectedUnitNum; }
    private int turnNum = 0;
    public int TurnNum { get => turnNum; }

    [SerializeField] Places placeContainer; //attach
    [SerializeField] Player[] playerPrefabs; //attach

    List<Player> playerList = new List<Player>(); 

    public Player[] PlayerList { get { return playerList.ToArray(); } }
    [SerializeField] List<SpriteFlip> playerSprite = new List<SpriteFlip>();


    Place lastClickedPlace;
    public Place LastClickedPlace { get =>lastClickedPlace; }

    [SerializeField] Text resultText; //attach

    //���
    public GameObject[] Gold_sprites;
    public GameObject island;
    public Text Player1_Gold;
    public Text Player2_Gold;

    public  int stayinisland = 3; //���ϰ� ���ǰ� �Ұ���
    public  int leftmovenum; //ĳ���Ͱ� ���� ����, �ٸ� ĳ���Ͱ� �����̴� Ƚ��

    private int GoalGoldNum = 20; //ȹ���ؾ��ϴ� �� ��.

    private void Awake()
    {
        if(null == instance)
{
            instance = this;
            //DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("Game Start");

        playerUnitAdd();
        Place4DirectionCheck(3);

        leftmovenum = stayinisland;

        resultText.gameObject.SetActive(false);
        //placeContainer.AllPlaceNoticeOff();

    }

    void playerUnitAdd()
    {   
        playerPrefabs = Resources.LoadAll<Player>("Prefabs/Players");

        int count = playerPrefabs.Length;// Mathf.Min(playerPrefabs.Length, 2); �����÷��̾ �߰��Ѵ��ϸ� 2�� ������ �����
        for(int i = 0; count > i; i++) //�÷��̾��
        {
            Debug.Log("player" + i);

            Player player = Instantiate(playerPrefabs[i]);

            player.StartPosSet(i);

            playerList.Add(player);
            //PlayerList[i] = player;
        }       
        
     }

    public void TurnChange()
    {
        thisTurnPlayer++;

        if (thisTurnPlayer >= maxPlayerNum)
            thisTurnPlayer = 0;

        Debug.Log("Next turn");
    }



    //public Place FindStandingPlace(Vector3 position)
    //{
    //    return placeContainer.FindPlayerPlace(position);
    //}


    public void ClickedUnitControl()
    {

        Vector3 clickedUnitPos = playerList[thisTurnPlayer].transform.position; //Ŭ���� �÷��̾ �ִ� ��ġ
        Place basePlace = placeContainer.FindPlayerPlace(clickedUnitPos); //Searching place
        Place tmpPlace = basePlace;

    }

   
    public void UnitStepSelection(Place selectedPlace)  //���� ��ġ �ٲٴ� �޼ҵ�, ����ȣ��Ǵ���???
    {

        Player unit = PlayerList[thisTurnPlayer];
        Player nonunit = PlayerList[OtherPlayerIndex(thisTurnPlayer)]; //ĳ������ ��� ����� ����

        //if (unit.playerState == PLAYERSTATE.WATING)
        //{
        //    unit.ChangeUnitState(PLAYERSTATE.PLAY);
        //    unit.gameObject.SetActive(true);
        //}

        if (unit.Isinisland == true) //ĳ���Ͱ� ���ε��� �ִ°� �ƴϸ� �̵�
        {
            for (int i = 0; i < stayinisland; i++)
            {
                nonunit.Movement();
                nonunit.getgoldnum += selectedPlace.goldnum;
                leftmovenum -= 1;
            }
        }
        else
        {
            unit.Movement();
            unit.getgoldnum += selectedPlace.goldnum;
            unit.Isattack = true;
        }

        TurnChange();
        FourDirectionNotice();
    }

    public void FourDirectionNotice()
    {
        if(lastClickedPlace.Up != null)
        {
            lastClickedPlace.Up.VisitedNoticeOff();
        }
        
        if(lastClickedPlace.Down != null)
        {
            lastClickedPlace.Down.VisitedNoticeOff();
        }
        
        if(lastClickedPlace.Left != null)
        {
            lastClickedPlace.Left.VisitedNoticeOff();
        }
        
        if(lastClickedPlace.Right != null)
        {
            lastClickedPlace.Right.VisitedNoticeOff();
        }
    }

    public int OtherPlayerIndex(int thisTurnPlayer)
    {
        if (thisTurnPlayer < 1) //thisturnPlayer == 0
            return 1;
        else
            return 0;
    }

    public void ResetGameEnv() //�÷��̾� �� ���� ���ε��� ��������, Ÿ���� �ʱ�ȭ�� �Լ�
    {
        for ( int i =0;i<placeContainer.Place.Length;i++) //Ÿ�� �ʱ�ȭ
        {
            placeContainer.Place[i].visited= false;
            placeContainer.Place[i].ChangeVisitedTileToUnvisitedTile();
            placeContainer.Place[i].VisitedNoticeOff();
            placeContainer.Place[i].SetGold();
        }

        //for(int i=0;i<maxPlayerNum;i++)
        //{
        //    if(PlayerList[i].Isinisland==true) //ĳ���Ͱ� ���ε��� ������, �ٸ� ĳ������ ������ġ�� �ٽ� ����
        //    {
        //        int otherplayerindex = OtherPlayerIndex(i);
        //        PlayerList[otherplayerindex].StartPosSet(otherplayerindex);
        //    }
        //    else
        //    {
        //        Debug.Log("�ش� �÷��̾�� ���ε��� ���� �ʽ��ϴ� : ",PlayerList[i]);
        //    }
        //}
    }


    public void Place4DirectionCheck(int fieldWidth)
    {
        Place tmpUP;
        Place tmpDown;
        Place tmpLeft;
        Place tmpRight;

        for(int i = 0; i < placeContainer.Place.Length; i++)
        {
            if(i - fieldWidth < 0) //up check
            {
                tmpUP = placeContainer.Place[i]; //self
            }
            else
            {
                tmpUP = placeContainer.Place[i - fieldWidth];
            }

            if(i + fieldWidth >= placeContainer.Place.Length) //down check;
            {
                tmpDown = placeContainer.Place[i];
            }
            else
            {
                tmpDown = placeContainer.Place[i + fieldWidth];
            }

            if(i % fieldWidth == 0) //left check
            {
                tmpLeft = placeContainer.Place[i];
            }
            else
            {
                tmpLeft = placeContainer.Place[i - 1];
            }

            if((i + 1) % fieldWidth == 0) //right check
            {
                tmpRight = placeContainer.Place[i];
            }
            else
            {
                tmpRight = placeContainer.Place[i + 1];
            }

            placeContainer.Place[i].Set4Direction(tmpUP, tmpDown, tmpLeft, tmpRight);

            
        }
        
    }

    public void SetLastClickedPlace(Place place)
    {
        lastClickedPlace = place;
    }

    public void AllNoticeOff()
    {
        placeContainer.AllPlaceNoticeOff();
    }

    public int GameCheck() //��ǥ��ȭ�� �� ������� Ȯ���ϴ� �Լ�
    {
        if (PlayerList[thisTurnPlayer].getgoldnum>= GoalGoldNum)
        {
            return thisTurnPlayer;
        }
        else if (PlayerList[OtherPlayerIndex(thisTurnPlayer)].getgoldnum>=GoalGoldNum)
        {
            return OtherPlayerIndex(thisTurnPlayer);
        }
        return 0;
    }

    public bool CheckIsolation()
    {
        bool check = false;

        if(lastClickedPlace != null)
        {
            if (lastClickedPlace.Up.visited && lastClickedPlace.Down.visited && lastClickedPlace.Left.visited && lastClickedPlace.Right.visited)
            {
                PlayerList[thisTurnPlayer].transform.position = island.transform.position;
                PlayerList[thisTurnPlayer].Isinisland = true;
                check = true;
                ResetGameEnv();
            }   
        }

        return check;
    }

    public void ResetGold()
    {
        for (int i=0;i<placeContainer.Place.Length;i++)
        {
            placeContainer.Place[i].SetGold();
        }
    }

    public void Reset() //���θ޴��� ���ư�
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Menu");
    }

    private void Update() 
    {
        if(GameCheck()>0)
        {
            resultText.text = "Player " + GameCheck().ToString() + " Win!";
            resultText.gameObject.SetActive(true);
            Invoke("Reset", 3.0f);
        }
       
        CheckIsolation();

        if(leftmovenum<=0)
        {
            PlayerList[thisTurnPlayer].Isinisland = false;
            PlayerList[OtherPlayerIndex(thisTurnPlayer)].Isinisland = false;

            leftmovenum = stayinisland;
        }

        Player1_Gold.text = PlayerList[0].getgoldnum.ToString();
        Player2_Gold.text = PlayerList[1].getgoldnum.ToString();

        //if (playerSprite[0].transform.position.x < playerSprite[1].transform.position.x)
        //{
        //    playerSprite[0].SpriteFlipX(true);
        //    playerSprite[1].SpriteFlipX(false);
        //}
        //else if(playerSprite[0].transform.position.x > playerSprite[1].transform.position.x)
        //{
        //    playerSprite[0].SpriteFlipX(false);
        //    playerSprite[1].SpriteFlipX(true);
        //}
    }   


}


