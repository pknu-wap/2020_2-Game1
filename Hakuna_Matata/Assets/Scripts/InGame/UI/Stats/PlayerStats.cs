using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // 활성화 플레이어 스탯창
    public GameObject validPlayerStats;
    // 플레이어 캐릭터 초상화 렌더러
    public SpriteRenderer playerCharacter;
    // 플레이어 캐릭터 초싱화 스프라이트
    public Sprite c1, c2, c3, c4, c5;
    // 턴 BG 스프라이트 렌더러
    public SpriteRenderer turnBG;
    // 내턴, 내턴아닐경우 BG 스프라이트
    public Sprite myTurn, notMyTurn;
    // 현재 플레이어 열쇠,코인 갯수 Text
    public TextMesh keysText, coinsText;
    // 현재 참조중인 플레이어
    public Player player;

    // 현재 스탯창이 삭제되었으면 진행안함
    private bool deleted = false;

    // 활성화 되지 못한 플레이어 스탯창 삭제
    public void destPlayerStats()
    {
        deleted = true;
        Destroy(validPlayerStats);
    }

    // 내턴인경우, 초록색 스프라이트로
    public void setMyTurn()
    {
        turnBG.sprite = myTurn;
    }

    // 내턴이 아닌 경우, 빨간색 스프라이트로
    public void setNotMyTurn()
    {
        turnBG.sprite = notMyTurn;
    }

    // 플레이어 지정
    public void setPlayer(Player p)
    {
        player = p;
    }

    // 플레이어 캐릭터 지정
    public void setPlayerCharacter(int characterNum)
    {
        switch (characterNum)
        {
            case 0:
                playerCharacter.sprite = c1;    // 소
                break;
            case 1:
                playerCharacter.sprite = c2;    // 말
                break;
            case 2:
                playerCharacter.sprite = c3;    // 돼지
                break;
            case 3:
                playerCharacter.sprite = c4;    // 양
                break;
            case 4:
                playerCharacter.sprite = c5;    // 토끼
                break;
            default:
                break;
        }
    }

    // 플레이어 스탯창 초기화
    public void initPlayerStats()
    {
        keysText.text = "0";
        coinsText.text = "0";
        setPlayerCharacter(player.getPlayerCharacter());
    }

    private void Start()
    {
        if (!deleted)
            initPlayerStats();
    }

    private void Update()
    {
        if (!deleted)
        {
            keysText.text = player.getPlayerKeys().ToString();
            coinsText.text = player.getPlayerCoins().ToString();
        }
    }
}
