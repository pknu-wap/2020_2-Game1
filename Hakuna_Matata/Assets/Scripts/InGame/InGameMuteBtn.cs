using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMuteBtn : MonoBehaviour
{
    public InGameMuteBtn mute;
    public SpriteRenderer renderer;
    public Sprite notmuted, muted;
    private bool isMuted = false;

    private void OnMouseDown()
    {
        if (!isMuted)
        {
            isMuted = true;
            renderer.sprite = muted;
            AudioListener.volume = 0;
        }

        else
        {
            isMuted = false;
            renderer.sprite = notmuted;
            AudioListener.volume = 1;
        }
    }
}
