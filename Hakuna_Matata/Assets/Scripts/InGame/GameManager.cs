using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 트랜스포트 객체 참조
    public GameObject transport;
    // instantiate용 플레이어 오브젝트
    private GameObject[] playerObject;

    // 레버 객체 참조
    public Lever lever;
    // 다이스 객체 참조
    public Dice[] dice = new Dice[5];
    // 블럭 객체 참조
    public Block[] blocks = new Block[40];
    // 플레이어 스탯 객체 참조
    public PlayerStats[] playerStats = new PlayerStats[4];
    // 플레이어 캐릭터 프리팹
    public GameObject[] characterPrefabs = new GameObject[5];
    // 플레이어들 객체 참조
    private Player[] player;
    // 총 플레이어 인원
    private int manyPlayer;
    // 각 플레이어의 캐릭터 (번호)
    private int[] character;
    // 엔딩 UI 참조
    public GameObject Ending;
    public GameObject instantEnding;
    public EndingUI endingUI;
    // 첫번째 플레이 기준
    private bool firstPlaying = true;

    // 현재 플레이어관련
    // 현재 진행중인 플레이어
    private Player nowPlayer;
    // 현재 진행중인 플레이어 번호
    private int playerNum;

    // 현재 게임 관련
    // Black Trader 게임 오브젝트 참조
    public GameObject blakTrader;
    // Black Trader의 현재 위치
    protected int blackTraderPos;
    // Key Block의 위치
    protected Block[] keyBlocks = new Block[10];

    // [[[ 게임 시스템 함수 ]]]
    public void game()
    {
        // 0. 우선 다음 플레이어로 먼저 지정함 (첫번째 플레이 이외)
        if (firstPlaying)
            firstPlaying = false;
        else
            setNextPlayer();
        setNowPlayerToBlock();
        nowPlayer = player[playerNum];

        // 1. 홀카운트 체크
        checkHoleCount();

        // 1-1. 새로 진행할 플레이어 Stats의 배경을 초록색으로 바꿈
        playerStats[playerNum].setMyTurn();
        
        // 2. 레버 푸쉬 가능하게 변경 (해당 lever의 함수에서, nowPlaying을 false로 바꿔줌)
        lever.setCanPush(true);
        // 2-1. 설명
        lever.allStats.setNextInfoText((playerNum + 1) + "번째 플레이어는 레버를 당겨주세요.");
    }

    // 홀카운트 체크 함수 (game의)
    private void checkHoleCount()
    {
        // ★ 홀카운트가 3 이면, 2로 바뀌어야 됨
        if (nowPlayer.getHoleCount() == 3)
        {
            // 1로 바꾸고,
            nowPlayer.setHoleCount(2);
            // 홀카운트 게임 오브젝트 스프라이트도 1로 변경해야 함
            nowPlayer.getHoleCountObj().GetComponent<SpriteRenderer>().sprite = nowPlayer.getHoleCount1();
            // 다음 플레이어로 권한 이동
            setNextPlayer();
            nowPlayer = player[playerNum];
            setNowPlayerToBlock();  // 블럭들 다음 플레이어 지정
            // 다음 플레이어의 홀카운트가 0이 아닐경우(다음 플레이어도 구멍에 있을 경우)
            if (nowPlayer.getHoleCount() > 0)
                checkHoleCount();   // 재귀호출
        }
        // ★ 홀카운트가 2 이면, 1으로 바뀌어야 됨
        else if (nowPlayer.getHoleCount() == 2)
        {
            // 1로 바꾸고,
            nowPlayer.setHoleCount(1);
            // 홀카운트 게임 오브젝트 스프라이트도 1로 변경해야 함
            nowPlayer.getHoleCountObj().GetComponent<SpriteRenderer>().sprite = nowPlayer.getHoleCount0();
            // 다음 플레이어로 권한 이동
            setNextPlayer();
            nowPlayer = player[playerNum];
            setNowPlayerToBlock();  // 블럭들 다음 플레이어 지정
            // 다음 플레이어의 홀카운트가 0이 아닐경우(다음 플레이어도 구멍에 있을 경우)
            if (nowPlayer.getHoleCount() > 0)
                checkHoleCount();   // 재귀호출
        }
        // ★ 홀카운트가 1 이면 파괴하고, 플레이할 수 있어야 됨
        else if (nowPlayer.getHoleCount() == 1)
        {
            // 0으로 바꾸고,
            nowPlayer.setHoleCount(0);
            // 홀카운트 게임 오브젝트 파괴
            nowPlayer.destHoleCountObj();
        }
    }

    // 다음 플레이어 지정
    public void setNowPlayerToBlock()
    {
        foreach (Block b in blocks)
        {
            b.setNowPlayer(playerNum);
        }
    }

    // 다음 플레이어로 전환
    public void setNextPlayer()
    {
        playerNum++;
        if (playerNum >= manyPlayer) playerNum -= manyPlayer;
    }

    // 현재 플레이어 반환
    public Player getNowPlayer()
    {
        return nowPlayer;
    }

    // 현재 플레이어 번호 반환
    public int getPlayerNum()
    {
        return playerNum;
    }

    // 현재 플레이어 Stats 배경 빨간색으로 (내턴이 아니게됨)
    public void notMyTurn()
    {
        playerStats[playerNum].setNotMyTurn();
    }

    // 현재 플레이어 이동 함수
    // 레버푸시가 종료되었을 경우, 플레이어가 이동함
    public void thisPlayerMove()
    {
        Debug.Log((playerNum + 1) + "번 플레이어가 " + lever.getHowManyMoved() + "칸을 이동합니다.");
        // 현 플레이어 이동
        nowPlayer.setPlayerMove(lever.getHowManyMoved(), blocks);
        // 현 플레이어 이동량 초기화 (Lever의 함수 호출)
        lever.setHowManyMovedZero();
        // 현 플레이어 점수 적용
        nowPlayer.setPlayerCoinsPlus(lever.getCoins());
    }

    // 현재 플레이어들 초기화 함수
    private void initPlayers()
    {
        // 인원수 만큼 플레이어 instatiate를 저장할 게임 오브젝트 생성
        playerObject = new GameObject[manyPlayer];
        // 인원수 만큼 플레이어 객체 생성
        player = new Player[manyPlayer];

        for (int i = 0; i < manyPlayer; i++) {
            // 플레이어 생성
            playerObject[i] = Instantiate(characterPrefabs[character[i]], new Vector2(-7.2f, 4.2f), Quaternion.identity);
            // 플레이어 객체에 오브젝트 연결
            player[i] = playerObject[i].GetComponent<Player>();
            // 플레이어들의 캐릭터 지정
            player[i].setPlayerCharacter(character[i]);
            // 플레이어 스코어 초기화
            player[i].initPlayerScore();
            // 플레이어들과 플레이어 스탯창 연동
            playerStats[i].setPlayer(player[i]);
        }

        if(manyPlayer == 2)
        {
            playerStats[2].destPlayerStats();
            playerStats[3].destPlayerStats();
        }
        else if(manyPlayer == 3)
        {
            playerStats[3].destPlayerStats();
        }
    }

    // 플레이어 인원수 반환
    public int getPlayerCount()
    {
        return manyPlayer;
    }

    // 모든 플레이어 반환
    public Player[] getPlayer()
    {
        return player;
    }

    // 현재 Black Trader의 위치 반환
    public int getBlackTraderPos()
    {
        return blackTraderPos;
    }

    // Black Trader의 위치 초기화 (고양이 블럭을 밟았을때, 게임 시작했을때)
    // 또한, Black Trader 게임 오브젝트 위치 변경
    public void initBlackTraderPos()
    {
        blackTraderPos = UnityEngine.Random.Range(0, 10);
        blakTrader.transform.position = new Vector2(keyBlocks[blackTraderPos].transform.position.x, keyBlocks[blackTraderPos].transform.position.y + 0.4f);
    }

    // Key Block 지정
    private void setKeyBlocks()
    {
        for(int i = 0; i<10; i++)
        {
            keyBlocks[i] = blocks[i * 4 + 3];
        }
    }

    // Key Block 지정
    public Block[] getKeyBlocks()
    {
        return keyBlocks;
    }

    // 홀카운트 게임 오브젝트 생성
    public void createHoleCountObj()
    {
        nowPlayer.createHoleCountObj(playerStats[playerNum].playerCharacter.transform.position);
    }

    // 엔딩 UI 실행
    public void execEnding()
    {
        instantEnding = Instantiate(Ending, new Vector2(0, 0), Quaternion.identity);
        endingUI = instantEnding.GetComponent<EndingUI>();
        endingUI.setWinner(playerStats[playerNum]);
    }


    // 게임 시스템 수행
    void Start()
    {
        // 트랜스포트 객체 받아옴
        transport = GameObject.FindGameObjectWithTag("Transport");
        // 트랜스포트 객체를 이용하여 인원수 받아옴
        manyPlayer = transport.GetComponent<Transport>().getPlayers();
        // 트랜스포트 객체를 이용하여 캐릭터 받아옴
        character = new int[manyPlayer];
        character = transport.GetComponent<Transport>().getCharacters();
        // 트랜스포트 오브젝트 삭제
        Destroy(transport);

        // 플레이어 초기화
        initPlayers();
        // 진행중 플레이어 번호 초기화
        playerNum = 0;

        // Key Blocks 지정
        setKeyBlocks();
        // Black Trader의 위치 지정
        initBlackTraderPos();

        // 게임 실행
        game();
    }

    
}
