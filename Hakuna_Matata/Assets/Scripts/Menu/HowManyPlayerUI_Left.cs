using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowManyPlayerUI_Left : MonoBehaviour
{
    public HowManyPlayerUI ui;

    private void OnMouseDown()
    {
        ui.leftBtn();
    }
}
