using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoinBlock : Block
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
        // 현재 플레이어 15코인 추가
        gameManager.getNowPlayer().setPlayerCoinsPlus(15);
        lever.allStats.setResultText("+ 15 코인");
        lever.allStats.setResultInfoText("15 코인을 얻습니다.");
        Debug.Log("해당 블럭은 GetCoinBlock입니다. 현재 플레이어는 15코인을 얻습니다.");

        // 다음 플레이어 게임 진행
        lever.initLever();   // (임시) 레버 초기화
        gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
        gameManager.game();// (임시) 다음 플레이어의 게임 진행
    }
}
