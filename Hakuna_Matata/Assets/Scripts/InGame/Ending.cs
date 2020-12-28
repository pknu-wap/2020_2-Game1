using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{

    // 게임 음악 없앰, 엔딩 음악 재생
    void Start()
    {
        GameObject.FindGameObjectWithTag("XBtn").GetComponent<BoxCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("MuteBtn").GetComponent<BoxCollider2D>().enabled = false;
        Destroy(GameObject.FindGameObjectWithTag("GameBGM"));
        gameObject.GetComponent<AudioSource>().Play();
    }
}

