using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : Block
{
    protected override IEnumerator processEvent()
    {
        yield return new WaitForSeconds(1.5f);
        lever.allStats.setResultText("다음 차례");
        lever.allStats.setResultInfoText(" ");
        Debug.Log("해당 블럭은 기능이 없습니다.");

        // 다음 플레이어 게임 진행
        lever.initLever();   // (임시) 레버 초기화
        gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
        gameManager.game();// (임시) 다음 플레이어의 게임 진행

    }
}
