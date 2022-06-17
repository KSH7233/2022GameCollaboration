using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private int thisTurnPlayer = 0; // 0 번부터 시작
    public int ThisTurnPlayer { get => thisTurnPlayer; }
    private int maxPlayerNum = 2;

    //private int selectedUnitNum = 0; //0 ~ 3
    //public int SelectedUnitNum { get => selectedUnitNum; }
    private int turnNum = 0;
    public int TurnNum { get => turnNum; }

    [SerializeField] Places placeContainer; //attach
    public Places PlaceContainer { get { return placeContainer; } }

    [SerializeField] Player[] playerPrefabs; //attach

    List<Player> playerList = new List<Player>();

    public Player[] PlayerList { get { return playerList.ToArray(); } }
    [SerializeField] List<SpriteFlip> playerSprite = new List<SpriteFlip>();


    Place lastClickedPlace;
    public Place LastClickedPlace { get => lastClickedPlace; }

    [SerializeField] Text resultText; //attach

    bool isolCheck = false;
    bool gameCheck = false;

    //경민
    public GameObject[] Gold_sprites;
    public GameObject island;
    public Text Player1_Gold;
    public Text Player2_Gold;

    static int bonusMove = 3;
    public int leftmovenum; //캐릭터가 고립된 동안, 다른 캐릭터가 움직이는 횟수

    private int GoalGoldNum = 40; //획득해야하는 금 수.

    public AudioSource audioSource;
    public AudioClip hookSound;

    bool ismove = false;
    public bool Ismove { get => ismove; }

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

        audioSource = GetComponent<AudioSource>();
        resultText.gameObject.SetActive(false);

        placeContainer.TileSetting();
        //placeContainer.AllPlaceNoticeOff();
        leftmovenum = bonusMove;
    }

    public void HookSound()
    {
        audioSource.volume = 1.0f;
        audioSource.Play();
    }

    void playerUnitAdd()
    {   
        playerPrefabs = Resources.LoadAll<Player>("Prefabs/Players");

        int count = playerPrefabs.Length;// Mathf.Min(playerPrefabs.Length, 2); 여러플레이어를 추가한다하면 2를 변수로 만들기
        for(int i = 0; count > i; i++) //플레이어수
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

        Vector3 clickedUnitPos = playerList[thisTurnPlayer].transform.position; //클릭한 플레이어가 있는 위치
        Place basePlace = placeContainer.FindPlayerPlace(clickedUnitPos); //Searching place
        Place tmpPlace = basePlace;

        

    }

   
    public void UnitStepSelection(Place selectedPlace)  //유닛 위치 바꾸는 메소드, 언제호출되는지???
    {

        Player unit = PlayerList[thisTurnPlayer];
       // Player nonunit = PlayerList[OtherPlayerIndex(thisTurnPlayer)]; //캐릭터의 방어 모션을 위해

        //if (unit.playerState == PLAYERSTATE.WATING)
        //{
        //    unit.ChangeUnitState(PLAYERSTATE.PLAY);
        //    unit.gameObject.SetActive(true);
        //}

        //nonunit.Isdeffence = true;
        unit.Movement();
        unit.getgoldnum += selectedPlace.GoldNum;
        selectedPlace.SetGoldZero();

        Facing();
        Invoke("FourDirectionNotice", 0.0f);
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

    public void ResetGameEnv() //플레이어 한 명이 무인도에 갇혔을때, 타일을 초기화할 함수
    {
        for ( int i =0;i<placeContainer.Place.Length;i++) //타일 초기화
        {
            if(placeContainer.FindPlayerPlace(playerList[thisTurnPlayer].transform.position)
               != placeContainer.Place[i])
            {
                placeContainer.Place[i].visited = false;
                placeContainer.Place[i].ChangeVisitedTileToUnvisitedTile();
            }            
            
            placeContainer.Place[i].NoticeControl(false);
            
            FourDirectionNotice(); //4방향켜기

            //placeContainer.Place[i].SetGold();
        }

        isolCheck = false;

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

    public bool GameCheck() //목표재화를 다 얻었는지 확인하는 함수
    {
        if (PlayerList[thisTurnPlayer].getgoldnum>= GoalGoldNum)
        {
            Debug.Log("Goal");

            gameCheck = true;

            return true;
        }

        return false;
    }

    

    public bool CheckIsolation()
    {

        if(lastClickedPlace != null)
        {
            if (lastClickedPlace.Up.visited && lastClickedPlace.Down.visited && lastClickedPlace.Left.visited && lastClickedPlace.Right.visited)
            {
                //PlayerList[thisTurnPlayer].SetPlayerState(PLAYERSTATE.PLAY2);
                //PlayerList[OtherPlayerIndex(thisTurnPlayer)].SetPlayerState(PLAYERSTATE.ISOL);
                Debug.Log("checkIsol true and");
                return true;
            }

        }

        Debug.Log("checkIsol false and");
        return false;
    }

    public void IsolCheck()
    {
        isolCheck = CheckIsolation();

        Debug.Log("Status check?");

        if(playerList[thisTurnPlayer].playerState == PLAYERSTATE.PLAY)
        {
            if (!isolCheck)
            {
                TurnChange();
            }
            else
            {
                PlayerList[thisTurnPlayer].SetPlayerState(PLAYERSTATE.PLAY2);

                PlayerList[OtherPlayerIndex(thisTurnPlayer)].SetPlayerState(PLAYERSTATE.ISOL);
                playerList[OtherPlayerIndex(thisTurnPlayer)].transform.position = island.transform.position;



            }
        }
        else if(playerList[thisTurnPlayer].playerState == PLAYERSTATE.PLAY2)
        {
            leftmovenum--;
            Debug.Log("left move : " + leftmovenum);
            if(leftmovenum <= 0)
            {
                playerList[thisTurnPlayer].SetPlayerState(PLAYERSTATE.PLAY);
                
                leftmovenum = bonusMove;
                Debug.Log("left move : " + leftmovenum);
                TurnChange();
            }
        }

        


    }

    public void ResetGold()
    {
        for (int i=0;i<placeContainer.Place.Length;i++) 
        {
            placeContainer.Place[i].SetGold();
        }
    }

    public void Reset() //메인메뉴로 돌아감
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Menu");
    }

    public void Facing()
    {
        if (playerList[0].transform.position.x > playerList[1].transform.position.x)
        {
            playerList[0].gameObject.transform.Rotate(new Vector3(0, 0, 0));
            playerList[1].gameObject.transform.Rotate(new Vector3(0, 180, 0));
        }
        else if(playerList[0].transform.position.x == playerList[1].transform.position.x)
        {
            playerList[0].gameObject.transform.Rotate(new Vector3(0, 0, 0));
            playerList[1].gameObject.transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {
            playerList[1].gameObject.transform.Rotate(new Vector3(0, 180, 0));
            playerList[0].gameObject.transform.Rotate(new Vector3(0, 0, 0));
        }


    }

    public void AICheck(Place sidePlace,ref int big, ref Place best)
    {
        if (sidePlace.Up.visited && sidePlace.Down.visited && sidePlace.Left.visited && sidePlace.Right.visited)
        {
            big = 10;
            best = sidePlace;
            return;
        }

        if (big < sidePlace.GoldNum)
        {
            big = sidePlace.GoldNum;
            best = sidePlace;            
        }
    }

    public void AImove() //clicked unit control
    {
        if(thisTurnPlayer == 1)
        {
            Place best = lastClickedPlace;

            int big = 0;

            if (!lastClickedPlace.Up.visited)
            {
                AICheck(lastClickedPlace.Up, ref big, ref best);
            }
            if(!lastClickedPlace.Down.visited)
            {
                AICheck(lastClickedPlace.Down, ref big, ref best);
            }
            if(!lastClickedPlace.Left.visited)
            {
                AICheck(lastClickedPlace.Left, ref big, ref best);
            }
            if(!lastClickedPlace.Right.visited)
            {
                AICheck(lastClickedPlace.Right, ref big, ref best);
            }

            if(best == lastClickedPlace)
            {
                if (!lastClickedPlace.Up.visited)
                {
                    best = lastClickedPlace.Up;
                }
                if (!lastClickedPlace.Down.visited)
                {
                    best = lastClickedPlace.Down;
                }
                if (!lastClickedPlace.Left.visited)
                {
                    best = lastClickedPlace.Left;
                }
                if (!lastClickedPlace.Right.visited)
                {
                    best = lastClickedPlace.Right;
                }
            }

            best.AIclicked();
        }
    }

    public void SetIsmove(bool check)
    {
        ismove = check;

        //Debug.Log("Movi is " + check);
    }


    private void Update() 
    {
        if(thisTurnPlayer == 1 && !ismove)
        {
            Invoke("AImove", 1.0f);
        }
        

        if (isolCheck)
        {
            ResetGameEnv();
        }

        Player1_Gold.text = PlayerList[0].getgoldnum.ToString() + " / 40";
        Player2_Gold.text = PlayerList[1].getgoldnum.ToString() + " / 40";

        if (GameCheck()) //gamecheck field
        {
            placeContainer.AllPlaceNoticeOff();

            PlayerList[OtherPlayerIndex(thisTurnPlayer)].Isdie = true;
            
            resultText.text = "Player " + thisTurnPlayer.ToString() + " Win!";
            resultText.gameObject.SetActive(true);
            Invoke("Reset", 3.0f);
        }

    }   


}


