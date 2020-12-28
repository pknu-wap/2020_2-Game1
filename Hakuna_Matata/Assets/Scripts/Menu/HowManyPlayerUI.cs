using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyPlayerUI : MonoBehaviour
{
    // 게임메이킹 객체 참조
    public GameMaking gameMaking;
    // 플레이어 인원수
    private int playerNum;
    // 플레이어 인원수 스프라이트 렌더러
    public SpriteRenderer playerNumSprite;
    // 각 스프라이트들
    public Sprite two, three, four;

    public int getPlayerNum()
    {
        return playerNum;
    }

    public void leftBtn()
    {
        switch (playerNum)
        {
            case 2:
                playerNum = 4;
                playerNumSprite.sprite = four;
                break;
            case 3:
                playerNum = 2;
                playerNumSprite.sprite = two;
                break;
            case 4:
                playerNum = 3;
                playerNumSprite.sprite = three;
                break;
            default:
                break;
        }
    }

    public void rightBtn()
    {
        switch (playerNum)
        {
            case 2:
                playerNum = 3;
                playerNumSprite.sprite = three;
                break;
            case 3:
                playerNum = 4;
                playerNumSprite.sprite = four;
                break;
            case 4:
                playerNum = 2;
                playerNumSprite.sprite = two;
                break;
            default:
                break;
        }
    }

    public void okBtn()
    {
        gameMaking.setPlayerNum(playerNum);
        Destroy(gameObject);
    }

    private void Start()
    {
        // 초기 인원수 2
        playerNum = 2;
        // 초기 스프라이트 2
        playerNumSprite.sprite = two;
    }
}
