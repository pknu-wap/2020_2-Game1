using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameBackBtn : MonoBehaviour
{
    private GameObject menuBGM;

    private void Start()
    {
        menuBGM = GameObject.FindGameObjectWithTag("MenuBGM");
    }

    private void OnMouseDown()
    {
        menuBGM.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Menu");
    }
}
