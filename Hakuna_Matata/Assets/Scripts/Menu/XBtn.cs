using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBtn : MonoBehaviour
{
    // 현재 족보 창
    public GameObject gameObject;
    // 족보 버튼
    private JockBoBtn jockBo;

    private void Start()
    {
        jockBo = GameObject.FindGameObjectWithTag("Jockbo").GetComponent<JockBoBtn>();
    }

    private void OnMouseDown()
    {
        jockBo.setNotCreated();
        Destroy(gameObject);
    }
}
