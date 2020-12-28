using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    public GameObject winner;

    public void setWinner(PlayerStats playerStats)
    {
        winner.GetComponent<SpriteRenderer>().sprite = playerStats.playerCharacter.sprite;
    }
}
