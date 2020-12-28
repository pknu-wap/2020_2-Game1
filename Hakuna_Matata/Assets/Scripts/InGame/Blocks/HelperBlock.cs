using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperBlock : Block
{
    // 키블럭들 위치 (BlackTrader가 존재하는 위치)
    private Block[] keyBlocks;
    // BlackTrader와의 거리
    private int distance;

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

        // 현재 플레이어를 BlackTrader 칸까지 이동시켜줌
        lever.allStats.setResultText("두더지를 만났다!");
        lever.allStats.setResultInfoText("쥐에게 곧장 이동합니다.");
        keyBlocks = gameManager.getKeyBlocks();
        // 34~40번 칸에 있을 경우
        if (keyBlocks[gameManager.getBlackTraderPos()].getBlockNum() - 33 > 0)
            distance = keyBlocks[gameManager.getBlackTraderPos()].getBlockNum() - 33;
        // 그 이외의 칸에 있을 경우
        else
        {
            distance = keyBlocks[gameManager.getBlackTraderPos()].getBlockNum() + 7;
        }
        // 플레이어 바로 이동
        gameManager.getNowPlayer().setPlayerMove(distance, gameManager.blocks);

        Debug.Log("해당 블럭은 HelperBlock 입니다. 플레이어가 BlackTrader로 바로 이동합니다.");
    }
}
