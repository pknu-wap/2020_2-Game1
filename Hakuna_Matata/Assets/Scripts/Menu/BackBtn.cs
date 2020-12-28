using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBtn : MonoBehaviour
{
    public JockBoBtn jockbo;

    private void OnMouseDown()
    {
        if (!jockbo.getCreated())
            SceneManager.LoadScene("Menu");
    }
}
