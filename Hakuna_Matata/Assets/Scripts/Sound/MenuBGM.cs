using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGM : MonoBehaviour
{
    // 오디오 소스
    public GameObject audio;
    // 오디오가 만들어졌는지 확인
    private static bool audioCreated = false;

    public void init()
    {
        if (!audioCreated)
        {
            audioCreated = true;
            audio.GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(audio);
        }
        else
        {
            Destroy(audio);
        }
    }

    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
