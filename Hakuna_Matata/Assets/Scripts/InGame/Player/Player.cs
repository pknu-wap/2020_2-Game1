using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 번호
    private int playerNum;
    // 플레이어 열쇠 갯수
    private int playerKeys;
    // 플레이어 코인 갯수
    private int playerCoins;
    // 플레이어 이동 가능
    private bool playerCanMove;
    // 플레이어 이동 관련
    private int playerMoveTemp, playerMoveDest;
    // 플레이어 캐릭터 (번호)
    private int playerCharacter;
    // 플레이어 홀카운트 (홀에 걸렸을 경우)
    private int playerHoleCount;
    // 플레이어 애니메이터
    public Animator animator;
    // 플레이어 스프라이트 렌더러
    public SpriteRenderer spriteRenderer;
    // 플레이어 홀카운트 게임 오브젝트
    public GameObject holeCountObj;
    // 플레이어 임시 홀카운트 게임 오브젝트
    private GameObject instantHoleCountObj;
    // 플레이어 홀카운트 스프라이트 렌더러
    public SpriteRenderer holeSpriteRenderer;
    // 플레이어 홀카운트 스프라이트
    public Sprite holeCount0, holeCount1;
    // 플레이어 오디오 소스
    private AudioSource movingSound;

    // 플레이어 생성 - 초기화
    void Start()
    {
        // 플레이어 첫 위치 초기화
        setPlayerLoc();
        // 플레이어 이동 불가
        setPlayerCantMove();
        // 플레이어 점수 0점 세팅
        initPlayerScore();
        // 플레이어 홀카운트 0
        playerHoleCount = 0;
        // 플레이어 오디오 소스 연결
        movingSound = gameObject.GetComponent<AudioSource>();
    }


    // [ 플레이어 번호 관련 ]
    // 1. 플레이어 번호 지정
    public void setPlayerNum(int num)
    {
        playerNum = num;
    }

    // 2. 플레이어 번호 반환
    public int getPlayerNum()
    {
        return playerNum;
    }

    // [ 플레이어 캐릭터 관련 ]
    // 1. 플레이어 캐릭터 지정
    public void setPlayerCharacter(int num)
    {
        playerCharacter = num;
    }

    public int getPlayerCharacter()
    {
        return playerCharacter;
    }

    // [ 플레이어 점수(코인,열쇠) 관련 ]
    // 1. 플레이어 점수 초기화
    public void initPlayerScore()
    {
        playerKeys = 0;
        playerCoins = 0;
    }

    // 2-1. 플레이어 코인 추가 (코인 갯수 설정)
    public void setPlayerCoins(int num)
    {
        playerCoins = num;
    }

    // 2-2. 플레이어 현 코인 갯수 반환
    public int getPlayerCoins()
    {
        return playerCoins;
    }

    public void setPlayerCoinsPlus(int plus)
    {
        playerCoins += plus;
    }

    // 3-1. 플레이어 열쇠 추가 (열쇠 갯수 설정)
    public void setPlayerKeysPlus(int num)
    {
        playerKeys += num;
    }

    // 3-2. 플레이어 현 열쇠 갯수 반환
    public int getPlayerKeys()
    {
        return playerKeys;
    }



    // [ 플레이어 이동 관련 ]
    // 0. 현재 플레이어 첫 위치 초기화
    public void setPlayerLoc()
    {
        playerMoveTemp = 0;
        playerMoveDest = 0;
    }

    // 1. 현재 플레이어에게 이동 권한 부여
    public void setPlayerCanMove()
    {
        playerCanMove = true;
    }

    // 2. 현재 플레이어에게 이동 권한 해제
    public void setPlayerCantMove()
    {
        playerCanMove = false;
    }

    // 3. 현재 플레이어가 이동 권한이 부여되었는가 확인
    public bool getPlayerCanMove()
    {
        return playerCanMove;
    }

    // 4. 현재 플레이어 이동 함수
    public void setPlayerMove(int leverRes, Block[] blocks)
    {
        // 이전 위치 저장
        playerMoveTemp = playerMoveDest;
        // 새로 이동 할 위치 지정
        playerMoveDest += leverRes;
        // 40보다 클 경우, 40을 뺌
        if (playerMoveDest >= 40) playerMoveDest -= 40;
        // 타겟 블럭으로 지정함
        blocks[playerMoveDest].setTargetBlock();

        StartCoroutine(PlayerMovePtoP(blocks));
    }

    // 4-1. 현재 플레이어 이동 함수 (한칸씩 진행되는) 
    IEnumerator PlayerMovePtoP(Block[] blocks)
    {
        // 이전 위치 저장 변수
        int playerMoveLast;
        // 이동 시간 조정용 변수
        float gapTime;

        // 한칸씩 이동하며 목표 위치까지 도달함
        while (playerMoveTemp != playerMoveDest)
        {
            // 이전 위치 저장
            playerMoveLast = playerMoveTemp;
            // 다음 블럭으로 이동
            playerMoveTemp++;
            if (playerMoveTemp >= 40) playerMoveTemp -= 40;

            // 애니메이터 변환
            animator.SetBool("isMoving", true);

            // 무빙 사운드 재생
            movingSound.Play();

            // 1~13블럭까지의 이동 (→)
            if (playerMoveLast < 12)
            {
                spriteRenderer.flipX = false;
                for (int i = 0; i < 20; i++)
                {
                    // 구간별 이동 시간 조정
                    gapTime = 0.005f * Mathf.Abs(i - 10.0f) - 0.002f * Mathf.Abs(i - 10.0f);

                    yield return new WaitForSeconds(gapTime);
                    gameObject.transform.position = new Vector2(transform.position.x + (blocks[playerMoveTemp].transform.position.x - 
                        blocks[playerMoveLast].transform.position.x)*(1/20.0f),transform.position.y);
                }
            }
            // 14~21블럭까지의 이동 (↓)
            else if(playerMoveLast>= 12 && playerMoveLast < 20)
            {
                spriteRenderer.flipX = false;
                for (int i = 0; i < 20; i++)
                {
                    // 구간별 이동 시간 조정
                    gapTime = 0.005f * Mathf.Abs(i - 10.0f) - 0.002f * Mathf.Abs(i - 10.0f);

                    yield return new WaitForSeconds(gapTime);
                    gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - (blocks[playerMoveLast].transform.position.y -
                        blocks[playerMoveTemp].transform.position.y) * (1 / 20.0f));
                }
            }
            // 22~33블럭까지의 이동 (←)
            else if (playerMoveLast >= 20 && playerMoveLast < 32)
            {
                spriteRenderer.flipX = true;
                for (int i = 0; i < 20; i++)
                {
                    // 구간별 이동 시간 조정
                    gapTime = 0.005f * Mathf.Abs(i - 10.0f) - 0.002f * Mathf.Abs(i - 10.0f);

                    yield return new WaitForSeconds(gapTime);
                    gameObject.transform.position = new Vector2(transform.position.x - (blocks[playerMoveLast].transform.position.x -
                        blocks[playerMoveTemp].transform.position.x) * (1 / 20.0f), transform.position.y);
                }
            }
            // 34~1블럭까지의 이동 (↑)
            else if (playerMoveLast >= 32 && playerMoveLast < 40)
            {
                spriteRenderer.flipX = true;
                for (int i = 0; i < 20; i++)
                {
                    // 구간별 이동 시간 조정
                    gapTime = 0.005f * Mathf.Abs(i - 10.0f) - 0.002f * Mathf.Abs(i - 10.0f);

                    yield return new WaitForSeconds(gapTime);
                    gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + (blocks[playerMoveTemp].transform.position.y -
                        blocks[playerMoveLast].transform.position.y) * (1 / 20.0f));
                }
            }
        }

        // 현재 플레이어의 이동이 끝난 이후
        yield return new WaitForSeconds(0.02f);
        // 애니메이터 정지
        animator.SetBool("isMoving", false);
    }

    // 4-2. 현재 플레이어 목표 위치 반환 함수
    public int getPlayerDest()
    {
        return playerMoveDest;
    }

    // [ 플레이어 애니메이션 관련 ]
    // 1. 플레이어 이동 애니메이션
    public void setPlayerMoveAnim()
    {
        animator.SetBool("isMoving", true);
    }
    // 2. 플레이어 정지 애니메이션
    public void setPlayerIdleAnim()
    {
        animator.SetBool("isMoving", false);
    }



    // [ 캐릭터 특성 관련 ]
    // 각 캐릭터 스킬
    protected virtual void skill()
    {
        Debug.Log(playerNum + "번 플레이어 스킬 사용");
    }

    // 각 플레이어 홀카운트
    public int getHoleCount()
    {
        return playerHoleCount;
    }

    public void setHoleCount(int num)
    {
        playerHoleCount = num;
    }

    // 홀카운트 게임 오브젝트 생성
    public void createHoleCountObj(Vector2 pos)
    {
        instantHoleCountObj = Instantiate(holeCountObj, new Vector2(pos.x, pos.y), Quaternion.identity);
    }

    // 홀카운트 스프라이트 (1,2 반환)
    public Sprite getHoleCount0()
    {
        return holeCount0;
    }
    public Sprite getHoleCount1()
    {
        return holeCount1;
    }

    // 현 홀카운트 게임 오브젝트 반환
    public GameObject getHoleCountObj()
    {
        return instantHoleCountObj;
    }

    // 현 홀카운트 게임 오브젝트 삭제
    public void destHoleCountObj()
    {
        Destroy(instantHoleCountObj);
    }
}
