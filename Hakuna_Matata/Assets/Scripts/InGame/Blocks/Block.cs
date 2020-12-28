using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // 게임매니저 참조
    public GameManager gameManager;
    // 레버 참조
    public Lever lever;

    // [ 변수 선언 ]
    // 현재 플레이어 번호
    public int nowPlayer;
    // 해당 블럭 번호
    public int blockNum;
    // 해당 블럭 컬라이더
    public BoxCollider2D collider2D;

    // 현재 블럭이 이동될 타겟블럭인가?
    // ---> 타겟 블럭이면, 블럭의 기능이 활성화 됨
    // ---> 타겟 블럭이 아니면, 블럭의 기능이 해제 됨 (그냥 지나가는 블럭이 됨)
    public bool targetBlock = false;

    // 현재 플레이어 지정
    public void setNowPlayer(int num)
    {
        nowPlayer = num;
    }

    // 블럭 번호 반환
    public int getBlockNum()
    {
        return blockNum;
    }

    // 타겟 블럭 지정
    public void setTargetBlock()
    {
        targetBlock = true;
    }
    
    // 타겟 블럭별 이벤트 수행
    protected virtual IEnumerator processEvent() {
        yield return new WaitForSeconds(0.0f);
    }

    // 플레이어가 블럭에 닿았을 경우
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 타겟 블럭인 경우에만 이벤트 수행함
        if (collider.gameObject.tag == "Player" && targetBlock && nowPlayer == gameManager.getPlayerNum())
        {
            // 타겟블럭 해제
            targetBlock = false;
            // 이벤트 수행
            StartCoroutine(processEvent()); 
        }
    }


}
