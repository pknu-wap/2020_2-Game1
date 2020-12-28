using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBackBtn_No : MonoBehaviour
{
    public GameObject gameObject;

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
