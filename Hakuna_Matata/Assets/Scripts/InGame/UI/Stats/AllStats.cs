using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStats : MonoBehaviour
{
    // 결과 Text
    public TextMesh result;
    // 결과 정보 Text
    public TextMesh resultInfo;
    // 다음 플레이어 Text
    public TextMesh nextInfo;

    // 결과 Text 변경 함수
    public void setResultText(string newText)
    {
        result.text = newText;
    }

    // 결과 정보 Text 변경 함수
    public void setResultInfoText(string newText)
    {
        resultInfo.text = newText;
    }

    // 다음 플레이어 Text 변경 함수
    public void setNextInfoText(string newText)
    {
        nextInfo.text = newText;
    }
}
