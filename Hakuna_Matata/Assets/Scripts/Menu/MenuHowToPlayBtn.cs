using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHowToPlayBtn : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
