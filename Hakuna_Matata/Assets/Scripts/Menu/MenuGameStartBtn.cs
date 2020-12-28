using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameStartBtn : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("GameMaking");
    }
}
