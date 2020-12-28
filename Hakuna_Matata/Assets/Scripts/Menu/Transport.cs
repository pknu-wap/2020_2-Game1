using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    // 넘겨줄 정보들
    // 1. 플레이어 인원
    public int players;
    // 2. 각 플레이어가 선택한 캐릭터
    public int[] character;

    // 설정
    // 1. 플레이어 인원 설정 함수
    public void setPlayers(int num)
    {
        // 플레이어 총 인원
        players = num;
        // 플레이어 총 인원에 해당하는 캐릭터 갯수 설정
        character = new int[num];
    }
    // 2. 각 캐릭터 설정
    public void setCharacters(int i, int num)
    {
        // 지정할 플레이어 번호와 선택한 캐릭터 번호를 둘다 받아와야 함
        character[i] = num;
    }

    // 반환
    // 1. 플레이어 인원 반환 함수
    public int getPlayers()
    {
        return players;
    }
    // 2. 각 캐릭터 설정 반환
    public int[] getCharacters()
    {
        return character;
    }
}
