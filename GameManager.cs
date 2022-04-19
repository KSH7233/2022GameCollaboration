using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private int thisTurnPlayer = 0; // 0 번부터 시작
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
        resultText.gameObject.SetActive(false);
        //placeContainer.AllPlaceNoticeOff();

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

   
    public void UnitStepSelection(Place selectedPlace) 
    {

        Player unit = playerList[thisTurnPlayer];           
                 
        //if (unit.playerState == PLAYERSTATE.WATING)
        //{
        //    unit.ChangeUnitState(PLAYERSTATE.PLAY);
        //    unit.gameObject.SetActive(true);
        //}

        unit.Movement(); //unit move 
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

    public bool GameCheck()
    {
        bool flag = false;

        if (lastClickedPlace != null)
        {         

            if (lastClickedPlace.Up.visited && lastClickedPlace.Down.visited && lastClickedPlace.Left.visited && lastClickedPlace.Right.visited)
            {
                flag = true;
            }
            
        }
       

        return flag;
    }

    public void Reset()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(gameObject.scene.name);
    }

    private void Update() 
    {  
        if(GameCheck())
        {
            
            resultText.text = "Player " + thisTurnPlayer.ToString() + " Win!";
            resultText.gameObject.SetActive(true);
            Invoke("Reset",3.0f);
        }

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


