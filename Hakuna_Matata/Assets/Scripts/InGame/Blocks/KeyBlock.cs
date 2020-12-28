using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBlock : Block
{
    // Black Trader가 등장할 위치 지정을 위해, 각 keyBlock의 번호를 지정함
    public int keyBlockNum;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    protected override IEnumerator processEvent()
    {
        yield return new WaitForSeconds(1.5f);

        // 현재 위치에 blackTrader가 있을 경우
        if (keyBlockNum == gameManager.getBlackTraderPos())
        {
            // 현재 플레이어가 코인이 100개 이상인 경우
            if (gameManager.getNowPlayer().getPlayerCoins() >= 100)
            {
                // 오디오 재생
                audioSource.Play();

                lever.allStats.setResultText("열쇠 획득!");
                lever.allStats.setResultInfoText("100코인으로 열쇠를 1개 얻습니다.");
                Debug.Log("해당 블럭은 KeyBlock입니다. 현재 플레이어는 100코인을 지불하고 열쇠를 1개 얻습니다.");
                gameManager.getNowPlayer().setPlayerKeysPlus(1);
                gameManager.getNowPlayer().setPlayerCoinsPlus(-100);
                // 승리 조건
                if (gameManager.getNowPlayer().getPlayerKeys() >= 3)
                {
                    gameManager.execEnding();
                }
                // 승리하지 못한 경우
                else
                {
                    // 다음 플레이어 게임 진행
                    lever.initLever();   // (임시) 레버 초기화
                    gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
                    gameManager.game();// (임시) 다음 플레이어의 게임 진행
                }
            }
            // 그렇지 않은 경우
            else
            {
                lever.allStats.setResultText("코인 부족");
                lever.allStats.setResultInfoText("코인이 부족하여 열쇠를 얻을수 없습니다.");
                Debug.Log("해당 블럭은 KeyBlock입니다. 현재 플레이어는 100코인이 없으므로 열쇠를 얻을수 없습니다.");

                // 다음 플레이어 게임 진행
                lever.initLever();   // (임시) 레버 초기화
                gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
                gameManager.game();// (임시) 다음 플레이어의 게임 진행
            }
        }
        // 없을 경우
        else
        {
            lever.allStats.setResultText("다음 차례");
            lever.allStats.setResultInfoText(" ");
            Debug.Log("해당 블럭은 기능이 없습니다.");

            // 다음 플레이어 게임 진행
            lever.initLever();   // (임시) 레버 초기화
            gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
            gameManager.game();// (임시) 다음 플레이어의 게임 진행
        }
    }
}
