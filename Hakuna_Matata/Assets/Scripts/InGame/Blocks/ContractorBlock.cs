using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractorBlock : Block
{
    // 모든 플레이어 저장
    private Player[] players;

    private AudioSource audioSource;

    protected override IEnumerator processEvent()
    {
        yield return new WaitForSeconds(1.5f);
        // 오디오 재생
        audioSource.Play();
        // 모든 플레이어 50코인 삭제
        for (int i = 0; i < gameManager.getPlayerCount(); i++)
        {
            if(!(players[i] == gameManager.getNowPlayer()))
                players[i].setPlayerCoins(0);
         }
        lever.allStats.setResultText("용병을 만났다!");
        lever.allStats.setResultInfoText("현재 플레이어를 제외한\n플레이어들은 모든 코인을 잃습니다.");
        Debug.Log("해당 블럭은 LooseCoinBlock입니다. 모든 플레이어는 50코인을 잃습니다.");

        // 다음 플레이어 게임 진행
        lever.initLever();   // (임시) 레버 초기화
        gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
        gameManager.game();// (임시) 다음 플레이어의 게임 진행
    }

    private void Start()
    {
        // 플레이어 얻어옴
        players = gameManager.getPlayer();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}
