using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool consumed = false;

    private void CollectCoin() {
        GameManager.instance.AddCoinCollected();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        Player player = other.GetComponent<Player>();

        if (player && !consumed)
        {
            
            consumed = true;

            CollectCoin();
        }
    }
}
