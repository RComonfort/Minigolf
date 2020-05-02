using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI progressGUI;

    int coinsColl;
    int coinsNeeded;

    private void Update() {
        coinsColl = GameManager.instance.CoinsCollected;
        coinsNeeded = GameManager.instance.CoinsNeeded;

        progressGUI.SetText(coinsColl + "/" + coinsNeeded);
    }    

    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player && coinsColl == coinsNeeded)
            GameManager.instance.GameOver();
    }
}
