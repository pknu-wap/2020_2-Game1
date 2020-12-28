using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JockBoBtn : MonoBehaviour
{
    // 족보 프리팹
    public GameObject jockbo;
    // 만들어 졌는지 확인
    private bool created = false;

    public void setNotCreated()
    {
        created = false;
    }

    // 만들어 졌는지 확인 (Back버튼)
    public bool getCreated()
    {
        return created;
    }

    private void OnMouseDown()
    {
        if (!created)
        {
            created = true;
            Instantiate(jockbo, new Vector2(0, 0), Quaternion.identity);
        }
    }

}
