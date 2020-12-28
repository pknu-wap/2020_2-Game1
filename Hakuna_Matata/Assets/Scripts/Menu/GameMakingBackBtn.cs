using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMakingBackBtn : MonoBehaviour
{
    public GameMaking gameMaking;

    private void OnMouseDown()
    {
        if (gameMaking.getCanChoose())
        {
            Destroy(GameObject.FindGameObjectWithTag("Transport"));
            SceneManager.LoadScene("Menu");
        }
    }
}
