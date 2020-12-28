using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleportBlock : Block
{
    private int moved;

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

        // 앞으로 1~5칸 이동
        moved = UnityEngine.Random.Range(1, 6);
        lever.allStats.setResultText(moved + "칸 앞으로!");
        lever.allStats.setResultInfoText("새를 만나서 날아갑니다.");
        gameManager.getNowPlayer().setPlayerMove(moved, gameManager.blocks);
        
        Debug.Log("해당 블럭은 RandomTeleportBlock 입니다. 플레이어가 앞으로 랜덤하게 이동합니다.");
    }
}
