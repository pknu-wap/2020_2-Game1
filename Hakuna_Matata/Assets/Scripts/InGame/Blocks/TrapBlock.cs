using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : Block
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override IEnumerator processEvent()
    {
        yield return new WaitForSeconds(1.5f);
        // 현재 플레이어의 열쇠 제거
        // 오디오 재생
        audioSource.Play();

        if (gameManager.getNowPlayer().getPlayerKeys() > 0)
            gameManager.getNowPlayer().setPlayerKeysPlus(-1);
        lever.allStats.setResultText("열쇠를 잃었다..");
        lever.allStats.setResultInfoText("덫을 밟았습니다.");
        Debug.Log("해당 블럭은 TrapBlock입니다. 플레이어의 열쇠를 빼앗습니다.");

        // 다음 플레이어 게임 진행
        lever.initLever();   // (임시) 레버 초기화
        gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
        gameManager.game();// (임시) 다음 플레이어의 게임 진행
    }
}
