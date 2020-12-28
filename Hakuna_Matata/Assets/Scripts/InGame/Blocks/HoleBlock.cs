using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBlock : Block
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

        // 현재 플레이어는 2턴간 쉬어야 함
        lever.allStats.setResultText("구멍에 빠졌다..");
        lever.allStats.setResultInfoText("2턴간 쉬어야 합니다.");
        // 홀카운트 오브젝트 생성
        gameManager.createHoleCountObj();
        // 해당 플레이어 홀카운트 3으로 지정
        gameManager.getNowPlayer().setHoleCount(3);
        Debug.Log("해당 블럭은 HoleBlock 입니다. 플레이어는 2턴간 쉬어야 합니다.");

        // 다음 플레이어 게임 진행
        lever.initLever();   // (임시) 레버 초기화
        gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
        gameManager.game();// (임시) 다음 플레이어의 게임 진행
    }
}
