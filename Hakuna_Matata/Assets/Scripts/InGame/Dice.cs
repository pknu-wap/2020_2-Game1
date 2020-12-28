using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    // 레버 오브젝트 참조
    public Lever lever;

    // 현재 주사위 애니메이터
    public Animator animator;  
    // 현재 주사위 번호
    public int diceNum;
    // 현재 주사위 값
    private int rollNum;
    // 현재 주사위 회전 회차 여부 (1회차/2회차 여부)
    private int diceRollingCount;
    // 현재 주사위 회전 여부
    private bool diceRolling;
    // 현재 주사위 잠금/해제 여부
    private bool diceLocked;
    // 주사위 잠금용 철창 프리팹
    public GameObject diceLockObject;
    // 주사위 잠금용 철창 객체
    private GameObject lockObject;
    // 주사위 잠금용 철창 객체 생성 여부
    private bool lockObjectOn;
    // 주사위 잠금 카운트
    private static int diceLockCount;

    // 플레이어 이동량 (Lever에서 반환됨)
    private int moved;

    // 현재 주사위 회전 여부 반환
    public bool getDiceRolling()
    {
        return diceRolling;
    }

    // 현재 주사위가 잠금 상태인지, 해제 상태인지 반환
    public bool getDiceLocked()
    {
        return diceLocked;
    }

    // 현재 주사위의 번호 대입 (각 1,2,3,4,5번 주사위)
    public void setDiceNum(int num) 
    {
        diceNum = num;
    }

    // 현재 주사위의 번호 반환 (각 1,2,3,4,5번 주사위)
    public int getDiceNum() 
    {
        return diceNum;
    }

    // 현재 주사위의 회전 결과 저장
    private void setRolledNum(int num)
    {
        rollNum = num;
    }

    // 현재 주사위의 회전 결과 반환
    public int getRolledNum()
    {
        return rollNum;
    }

    // (레버를 밀었을 경우)현재 주사위를 굴려서, 그 결과를 저장함
    public IEnumerator rollDice()
    {
        // 현재 주사위가 잠금되지 않았을경우에만 수행
        // 주사위 회전 시작
        if (!diceLocked)
        {
            // 주사위 회전 시작
            diceRolling = true;
            // 주사위 애니메이션 시작
            animator.SetInteger("rolledNum", 0);
            animator.SetBool("isDiceRolling", true);
        }

        // *초후, 회전 정지
        yield return new WaitForSeconds(2.0f + diceNum * 0.5f);   // 주사위 별, 수행 속도 다름
        if (!diceLocked)
        {
            // 주사위 랜덤 결과값 변수에 대입
            setRolledNum(UnityEngine.Random.Range(1, 7));
            Debug.Log(diceNum + " 번 주사위 값 : " + rollNum);

            // 주사위 애니메이션 정지
            animator.SetInteger("rolledNum", rollNum);
            animator.SetBool("isDiceRolling", false);
            // 주사위 회전 정지
            diceRolling = false;

            // 주사위 회전 카운트 증가
            diceRollingCount++;
        }

        // 주사위에 따른 기능 수행
        if (lever.getPushCount() == 1)
        {
            // 1,2번 주사위의 1회차 이후에 이동량에 대한 연산 수행
            if (diceNum == 1 || diceNum == 2)
                lever.move(getRolledNum());
            // 5번 주사위의 1회차 이후에 레버 사용 가능하게 하는 연산 수행
            else if (diceNum == 5)
            {
                lever.setCanPush(true);
                lever.setMovedField();  // 이동량 창에 이동량 셋팅
                lever.allStats.setResultInfoText("레버를 한번 더 당길 수 있습니다.");
                lever.allStats.setNextInfoText("주사위 2개까지 잠금할 수 있습니다.");
            }
        }
        else if (lever.getPushCount() == 2)
        {
            // 5번 주사위의 2회차 이후에 결과에 대한 연산 수행
            if (diceNum == 5)
                lever.calculate();
        }
            
    }

    // 다이스 초기화 함수 (주사위 던진 횟수 0, 잠금 해제, 잠금 카운트 0, 철창있으면 삭제)
    public void initDice()
    {
        diceRollingCount = 0;
        diceRolling = false;
        diceLocked = false;
        diceLockCount = 0;
        if (lockObjectOn)
        {
            lockObjectOn = false;
            Destroy(lockObject);
        }
        else
            lockObjectOn = false;
    }

    // (레버 2회차의 주사위 잠금 기능) 현재 주사위를 잠금시켜서, rollDice를 호출 못하게 함
    private void OnMouseDown()
    {
        // 현재 다이스가 한번 돌려지고 난 뒤에, 클릭이 수행될 시
        if (diceRollingCount == 1 && diceLockCount < 2 && !diceLocked)
        {
            diceLocked = true;  // 현재 다이스 잠금
            diceLockCount++;    // 다이스 잠금 카운트 상승 (3개 이상 불가능)
            lockObject = Instantiate(diceLockObject, transform.localPosition, Quaternion.identity);    // 잠금 오브젝트 생성
            lockObjectOn = true;
        }

        // 현재 다이스가 잠금 된 경우에, 다시 누르면 해제됨
        else if (diceLocked)
        {
            diceLocked = false;
            diceLockCount--;
            lockObjectOn = false;
            Destroy(lockObject);
        }
    }

    private void Start()
    {
        initDice();
    }
}
