using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBlock : Block
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override IEnumerator processEvent()
    {
        yield return new WaitForSeconds(1.5f);
        // 오디오 재생
        audioSource.Play();

        // BlackTrader 위치 초기화
        gameManager.initBlackTraderPos();
        lever.allStats.setResultText("쥐가 도망쳤다!");
        lever.allStats.setResultInfoText("쥐의 위치를 변경합니다.");
        Debug.Log("해당 블럭은 CatBlock입니다. BlackTrader의 위치를 변경합니다.");

        // 다음 플레이어 게임 진행
        lever.initLever();   // (임시) 레버 초기화
        gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
        gameManager.game();// (임시) 다음 플레이어의 게임 진행
    }
}
