using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    // [변수]
    // 게임매니저 참조
    public GameManager gameManager;
    // 다이스 객체 참조
    public Dice[] dice = new Dice[5];
    // 모든 정보 창 객체 참조
    public AllStats allStats;
    // 현재 이동량 창 객체 참조
    public GameObject movedField;
    private TextMesh movedFieldText;
    // 사운드 참조
    public AudioSource slotAudio;

    // 결과 관련
    // 결과 얻은 코인
    private int coins;
    // 결과 얻은 값 (이름)
    private string result;
    // 이동 횟수 (1,2번 주사위 결과 합 = 2~12 중 1개)
    private int howManyMove;

    // 현재 레버 객체 관련
    // 푸쉬 가능 여부 변수 (게임매니저에서 조작)
    private bool canPush;
    // 레버 클릭 횟수 체크 (총 2회 수행)
    private int pushCount;
    // 레버 함수 종료 체크 (1,2회 모두 종료)
    private bool pushEnd;
    // 레버 애니메이션
    private Animator animator;


    // [ 함수 ]
    // 각종 초기화 함수
    public void initLever()
    {
        pushCount = 0;  // 푸쉬 함수 호출 가능 횟수 초기화
        pushEnd = false;// 함수 종료 초기화
    }

    // 코인 갯수 반환 함수
    public int getCoins()
    {
        return coins;
    }

    // 이동 횟수 반환 함수
    public int getHowManyMoved()
    {
        return howManyMove;
    }

    // 이동 횟수 초기화 함수
    public void setHowManyMovedZero()
    {
        howManyMove = 0;
    }

    // 푸쉬 종료 반환 함수
    public bool getPushEnd()
    {
        return pushEnd;
    }

    // 푸쉬 카운트 반환 함수 (5번 주사위가 끝나고 갬블을 위함)
    public int getPushCount()
    {
        return pushCount;
    }

    // 푸쉬 가능 여부 변수 (게임 매니저에서 사용됨)
    public void setCanPush(bool b)
    {
        canPush = b;
    }

    // 레버 클릭 했을 경우 수행
    private void OnMouseDown()
    {
        // 레버가 푸쉬 가능한 상태이고, 푸쉬되지 않았으면
        if (canPush && !dice[0].getDiceRolling() && !dice[1].getDiceRolling() &&
             !dice[2].getDiceRolling() && !dice[3].getDiceRolling() && !dice[4].getDiceRolling())    
        {
            // 푸쉬 됨 (푸쉬 불가하게 함)
            canPush = false;
            // 푸쉬 카운트 증가
            pushCount++;
            // 푸쉬 애니메이션
            animator.SetTrigger("isClicked");
            // 푸쉬 사운드
            slotAudio.Play();
            // 푸쉬 함수 호출
            pushLever();    

            Debug.Log(pushCount + "번째 푸쉬를 수행합니다");
        }
    }

    // 푸쉬 함수
    private void pushLever()
    {
        // 5개의 주사위 회전
        for (int i = 0; i < 5; i++)
            StartCoroutine(dice[i].rollDice()); // 주사위 멈춤
    }

    // 1회차 푸쉬 결과값으로, 1,2번 주사위의 결과 값을 이용한 위치 이동량 ( 2 ~ 12 )
    public void move(int dice)
    {
        howManyMove += dice;
    }

    // 2회차 이후, 5번 주사위에서 호출하여 최종 계산
    public void calculate()
    {
        // 핵심 알고리즘 수행 (결과 코인, 이름 저장)
        core(dice[0].getRolledNum(), dice[1].getRolledNum(), dice[2].getRolledNum(), dice[3].getRolledNum(), dice[4].getRolledNum());

        // 푸쉬 종료 상태로 (1,2차 푸쉬 모두 완료)
        pushEnd = true;

        // 화면에 출력
        allStats.setResultText(result);
        allStats.setResultInfoText(coins + " 코인을 획득합니다.");
        allStats.setNextInfoText("플레이어가 이동합니다.");

        // 게임매니저를 이용하여 이동
        gameManager.thisPlayerMove();

        // 각 주사위 초기화 진행
        for (int i = 0; i < 5; i++)
            dice[i].initDice();

    }

    // 게임 핵심 알고리즘 (레버를 내림으로 얻은 주사위 값으로 계산)
    public void core(int dice1, int dice2, int dice3, int dice4, int dice5)
    { 
        // 1. hakuna matata
        if (dice1 == dice2 && dice1 == dice3 && dice1 == dice4 && dice1 == dice5)
        {
            coins = 50; result = "Hakuna Matata";
        }
        // 2. 4 of a kind 
        else if (dice1 != dice2 && dice2 == dice3 && dice2 == dice4 && dice2 == dice5)  // Dice 1 빼고 같은경우
        {
            coins = 35; result = "4 of a kind";
        }
        else if (dice2 != dice1 && dice1 == dice3 && dice1 == dice4 && dice1 == dice5)  // Dice 2 빼고 같은경우
        {
            coins = 35; result = "4 of a kind";
        }
        else if (dice3 != dice2 && dice2 == dice1 && dice2 == dice4 && dice2 == dice5)  // Dice 3 빼고 같은경우
        {
            coins = 35; result = "4 of a kind";
        }
        else if (dice4 != dice2 && dice2 == dice3 && dice2 == dice1 && dice2 == dice5)  // Dice 4 빼고 같은경우
        {
            coins = 35; result = "4 of a kind";
        }
        else if (dice5 != dice2 && dice2 == dice3 && dice2 == dice4 && dice2 == dice1)  // Dice 5 빼고 같은경우
        {
            coins = 35; result = "4 of a kind";
        }
        // 3. Full House
        else if (dice1 == dice2 && dice3 == dice4 && dice3 == dice5 && dice2 != dice5)  // ooxxx
        {
            coins = 35; result = "Full House";
        }
        else if (dice1 == dice3 && dice2 == dice4 && dice2 == dice5 && dice1 != dice5)  // oxoxx
        {
            coins = 35; result = "Full House";
        }
        else if (dice1 == dice4 && dice2 == dice3 && dice2 == dice5 && dice1 != dice5)  // oxxox
        {
            coins = 35; result = "Full House";
        }
        else if (dice1 == dice5 && dice2 == dice3 && dice2 == dice4 && dice2 != dice5)  // oxxxo
        {
            coins = 35; result = "Full House";
        }
        else if (dice2 == dice3 && dice1 == dice4 && dice1 == dice5 && dice1 != dice2)  // xooxx
        {
            coins = 35; result = "Full House";
        }
        else if (dice2 == dice4 && dice1 == dice3 && dice1 == dice5 && dice1 != dice2)  // xoxox
        {
            coins = 35; result = "Full House";
        }
        else if (dice2 == dice5 && dice1 == dice3 && dice1 == dice4 && dice1 != dice2)  // xoxxo
        {
            coins = 35; result = "Full House";
        }
        else if (dice3 == dice4 && dice1 == dice2 && dice1 == dice5 && dice4 != dice5)  // xxoox
        {
            coins = 35; result = "Full House";
        }
        else if (dice3 == dice5 && dice1 == dice2 && dice1 == dice4 && dice4 != dice5)  // xxoxo
        {
            coins = 35; result = "Full House";
        }
        else if (dice4 == dice5 && dice1 == dice2 && dice1 == dice3 && dice3 != dice5)  // xxxoo
        {
            coins = 35; result = "Full House";
        }

        // 4. Large Straight
        else if (dice1 + dice2 + dice3 + dice4 + dice5 == 15 && dice1 * dice2 * dice3 * dice4 * dice5 == 120)   // 12345
        {
            coins = 30; result = "Large Straight";
        }
        else if (dice1 + dice2 + dice3 + dice4 + dice5 == 20 && dice1 * dice2 * dice3 * dice4 * dice5 == 720)   // 23456
        {
            coins = 30; result = "Large Straight";
        }

        // 5. Odd
        else if (dice1 % 2 == 1 && dice2 % 2 == 1 && dice3 % 2 == 1 && dice4 % 2 == 1 && dice5 % 2 == 1)
        {
            // 5-1. Odd Decalcomanie
            if (dice1 + dice2 + dice3 + dice4 + dice5 == 13)
            {
                coins = 30; result = "Odd Decalcomanie";
            }
            else if (dice1 + dice2 + dice3 + dice4 + dice5 == 15)
            {
                coins = 30; result = "Odd Decalcomanie";
            }
            else if (dice1 + dice2 + dice3 + dice4 + dice5 == 17)
            {
                coins = 30; result = "Odd Decalcomanie";
            }
            // 5-2. Odd
            else
            {
                coins = 15; result = "Odd";
            }

        }

        // 6. Even
        else if (dice1 % 2 == 0 && dice2 % 2 == 0 && dice3 % 2 == 0 && dice4 % 2 == 0 && dice5 % 2 == 0)
        {
            // 5-1. Even Decalcomanie
            if (dice1 + dice2 + dice3 + dice4 + dice5 == 18)
            {
                coins = 30; result = "Even Decalcomanie";
            }
            else if (dice1 + dice2 + dice3 + dice4 + dice5 == 20)
            {
                coins = 30; result = "Even Decalcomanie";
            }
            else if (dice1 + dice2 + dice3 + dice4 + dice5 == 22)
            {
                coins = 30; result = "Even Decalcomanie";
            }
            // 5-2. Even
            else
            {
                coins = 15; result = "Even";
            }

        }

        // 7. Decalcomanie
        else if (dice1 == dice2 && dice3 == dice4)  // ooxx?
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice3 && dice2 == dice4)  // oxox?
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice4 && dice2 == dice3)  // oxxo?
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice2 && dice3 == dice5)  // oox?x
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice3 && dice2 == dice5)  // oxo?x
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice5 && dice2 == dice3)  // oxx?o
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice2 && dice4 == dice5)  // oo?xx
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice4 && dice2 == dice5)  // ox?ox
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice5 && dice2 == dice4)  // ox?xo
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice3 && dice4 == dice5)  // o?oxx
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice4 && dice3 == dice5)  // o?xox
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice1 == dice5 && dice3 == dice4)  // o?xxo
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice2 == dice3 && dice4 == dice5)  // ?ooxx
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice2 == dice4 && dice3 == dice5)  // ?oxox
        {
            coins = 20; result = "Decalcomanie";
        }
        else if (dice2 == dice5 && dice3 == dice4)  // ?oxxo
        {
            coins = 20; result = "Decalcomanie";
        }

        // 8. Small Straight
        else if (dice1 + dice2 + dice3 + dice4 == 10 && dice1 * dice2 * dice3 * dice4 == 24)  // 5빼고
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice3 + dice4 == 14 && dice1 * dice2 * dice3 * dice4 == 120)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice3 + dice4 == 18 && dice1 * dice2 * dice3 * dice4 == 360)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice3 + dice5 == 10 && dice1 * dice2 * dice3 * dice5 == 24)  // 4빼고
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice3 + dice5 == 14 && dice1 * dice2 * dice3 * dice5 == 120)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice3 + dice5 == 18 && dice1 * dice2 * dice3 * dice5 == 360)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice4 + dice5 == 10 && dice1 * dice2 * dice4 * dice5 == 24)  // 3빼고
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice4 + dice5 == 14 && dice1 * dice2 * dice4 * dice5 == 120)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice2 + dice4 + dice5 == 18 && dice1 * dice2 * dice4 * dice5 == 360)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice3 + dice4 + dice5 == 10 && dice1 * dice3 * dice4 * dice5 == 24)  // 2빼고
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice3 + dice4 + dice5 == 14 && dice1 * dice3 * dice4 * dice5 == 120)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice1 + dice3 + dice4 + dice5 == 18 && dice1 * dice3 * dice4 * dice5 == 360)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice2 + dice3 + dice4 + dice5 == 10 && dice2 * dice3 * dice4 * dice5 == 24)  // 1빼고
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice2 + dice3 + dice4 + dice5 == 14 && dice2 * dice3 * dice4 * dice5 == 120)
        {
            coins = 15; result = "Small Straight";
        }
        else if (dice2 + dice3 + dice4 + dice5 == 18 && dice2 * dice3 * dice4 * dice5 == 360)
        {
            coins = 15; result = "Small Straight";
        }


        // 0. Else
        else
        {
            coins = (dice1 + dice2 + dice3 + dice4 + dice5) / 5;
            result = "Others...";
        }

        Debug.Log(result + "얻은 코인 갯수 : " + coins);
    }

    // 이동량 창에 셋팅
    public void setMovedField()
    {
        movedFieldText.text = howManyMove.ToString();
    }

    // 레버 생성과 함께 초기화
    void Start()
    {
        // 애니메이터 초기화
        animator = gameObject.GetComponent<Animator>();
        // 각종 변수 초기화
        initLever();
        // 이동 횟수 초기화
        setHowManyMovedZero();
        // 이동량 창 초기화
        movedFieldText = movedField.GetComponent<TextMesh>();
    }
}
