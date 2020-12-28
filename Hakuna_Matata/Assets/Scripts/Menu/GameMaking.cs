using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaking : MonoBehaviour
{
    // 인게임으로 전송할 게임오브젝트
    public GameObject transport;
    private Transport trans;
    // 캐릭터 버튼들 (클릭 못하게 하기 위함)
    public GameObject[] characterBtn = new GameObject[5];
    // 게임시작 버튼 프리팹
    public GameObject gameStartBtn;
    // 플레이어 인원 수
    public int playerNum;
    // 각 플레이어별 선택한 캐릭터 번호
    private int[] playerCharacterNum;
    // 플레이어 선택 카운트
    private int count;
    // 캐릭터 선택 가능 여부 (HowManyPlayerUI가 꺼졌는지 여부)
    private bool canChoose;

    // 캐릭터 선택 가능 여부 반환 (Xbtn 용)
    public bool getCanChoose()
    {
        return canChoose;
    }

    // 플레이어 인원 수 지정 (HowManyPlayerUI에서 호출됨)
    public void setPlayerNum(int num)
    {
        playerNum = num;
        playerCharacterNum = new int[num];
        canChoose = true;
        foreach(GameObject c in characterBtn)
        {
            c.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }

    // 플레이어 캐릭터 지정 함수
    public void setPlayerCharacter(int character)
    {
        if (canChoose)
        {
            characterBtn[character].GetComponent<SelectCharacter>().setSelected();
            playerCharacterNum[count] = character;
            characterBtn[character].GetComponent<SelectCharacter>().setArrow(count);
            // 카운트 증가
            count++;
            // 카운트에 도달한 경우, 게임시작 버튼 출력
            if (count >= playerNum)
            {
                // 더이상 버튼 클릭 못하도록 잠금
                for (int i = 0; i < 5; i++)
                    characterBtn[i].GetComponent<SelectCharacter>().setSelected();
                // 게임시작 버튼 출력
                Instantiate(gameStartBtn, new Vector2(0, -4.03f), Quaternion.identity);
                // Transport의 변수 세팅
                // 인원수 세팅
                trans.setPlayers(playerNum);
                // 각 인원 캐릭터 세팅
                for (int i = 0; i < playerNum; i++)
                {
                    trans.setCharacters(i, playerCharacterNum[i]);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canChoose = false;
        count = 0;
        DontDestroyOnLoad(transport);
        trans = transport.GetComponent<Transport>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
