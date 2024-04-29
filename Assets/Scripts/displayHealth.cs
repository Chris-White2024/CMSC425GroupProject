using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayHealth : MonoBehaviour
{
    public combat playerCombat;
    public TextMeshProUGUI healthText;

    void Start()
    {
        healthText.text = "Health: " + playerCombat.health;
        playerCombat = this.GetComponent<combat>();
    }

    public void updateHealth()
    {
        healthText.text = "Health: " + playerCombat.health;
    }
}
