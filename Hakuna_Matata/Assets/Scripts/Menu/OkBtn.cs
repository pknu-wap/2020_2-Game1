using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkBtn : MonoBehaviour
{
    public HowManyPlayerUI ui;

    private void OnMouseDown()
    {
        ui.okBtn();
    }

}
