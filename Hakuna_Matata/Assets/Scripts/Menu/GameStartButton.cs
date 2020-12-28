using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    private GameObject menuBGM;

    private void Start()
    {
        menuBGM = GameObject.FindGameObjectWithTag("MenuBGM");
    }

    private void OnMouseDown()
    {
        menuBGM.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("InGame");
    }
}
