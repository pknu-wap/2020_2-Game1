using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleBlock : Block
{
    // 확률
    private int percent;
    // 확률에 대한 코인 획득/제거 량
    private int coins;
    // 확률에 대한 String
    private string percent_s;

    private AudioSource audioSource;
    public AudioClip win, fail; 

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // 현재 플레이어 코인 획득 or 코인 제거 or 열쇠 획득
    protected override IEnumerator processEvent()
    {
        yield return new WaitForSeconds(1.5f);
        // 확률 새로 셋팅
        percent = UnityEngine.Random.Range(0, 100);
        coins = UnityEngine.Random.Range(1, 100);
        // 현재 플레이어 코인 획득 or 코인 제거 or 열쇠 획득
        if (percent < 40)
        {
            audioSource.clip = win;
            audioSource.Play();
            gameManager.getNowPlayer().setPlayerCoinsPlus(coins);
            percent_s = coins + "코인을 주웠다!";

            // 다음 플레이어 게임 진행
            lever.initLever();   // (임시) 레버 초기화
            gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
            gameManager.game();// (임시) 다음 플레이어의 게임 진행
        }
        else if (percent >= 40 && percent < 80)
        {
            audioSource.clip = fail;
            audioSource.Play();
            if (gameManager.getNowPlayer().getPlayerCoins() < coins)
            {
                gameManager.getNowPlayer().setPlayerCoins(0);
                percent_s = "코인을 모두 잃었다..";
            }
            else
            {
                gameManager.getNowPlayer().setPlayerCoinsPlus(-coins);
                percent_s = coins + "코인을 잃었다..";
            }

            // 다음 플레이어 게임 진행
            lever.initLever();   // (임시) 레버 초기화
            gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
            gameManager.game();// (임시) 다음 플레이어의 게임 진행
        }
        else if (percent >= 80 && percent < 100)
        {
            audioSource.clip = win;
            audioSource.Play();
            gameManager.getNowPlayer().setPlayerKeysPlus(1);
            percent_s = "열쇠를 얻었다!";
            // 승리 조건
            if (gameManager.getNowPlayer().getPlayerKeys() >= 3)
            {
                gameManager.execEnding();
            }
            else
            {
                // 다음 플레이어 게임 진행
                lever.initLever();   // (임시) 레버 초기화
                gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
                gameManager.game();// (임시) 다음 플레이어의 게임 진행
            }
        }
        // 버그
        else
        {
            // 다음 플레이어 게임 진행
            lever.initLever();   // (임시) 레버 초기화
            gameManager.notMyTurn();    // (임시) 이전 플레이어의 Stat 배경 빨간색으로 변경
            gameManager.game();// (임시) 다음 플레이어의 게임 진행
        }
        
        // 위의 확률에 대한 결과 출력
        lever.allStats.setResultText(percent_s);
        lever.allStats.setResultInfoText("아무도 결과를 알 수 없습니다.");
        Debug.Log("해당 블럭은 GambleBlock입니다. 현재 플레이어는 코인 얻기 or 잃기 or 열쇠 얻기합니다.");
    }
}
