using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    // 게임 메이킹 객체 참조
    public GameMaking gameMaking;
    // 플레이어 화살표 프리팹
    public GameObject[] arrows = new GameObject[4];
    // 캐릭터 번호 지정
    public int character;
    // 현재 캐릭터가 선택되었는지 확인
    private bool selected = false;

    // GameMaking에서 모든 플레이어가 선택한 이후, 버튼들 잠금
    public void setSelected()
    {
        selected = true;
    }

    // 화살표 오브젝트 생성
    public void setArrow(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                Instantiate(arrows[0], new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity);
                break;
            case 1:
                Instantiate(arrows[1], new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity);
                break;
            case 2:
                Instantiate(arrows[2], new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity);
                break;
            case 3:
                Instantiate(arrows[3], new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity);
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (!selected)
        {
            // 플레이어가 해당 번호의 캐릭터를 선택한것으로 됨
            gameMaking.setPlayerCharacter(character);
        }
    }
}
