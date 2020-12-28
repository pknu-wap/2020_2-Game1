using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamBackXBtn : MonoBehaviour
{
    public GameObject gameObject;

    private void OnMouseDown()
    {
        Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity);
    }
}
