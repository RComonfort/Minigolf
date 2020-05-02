﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            GameManager.instance.ResetPlayer();
        }
    }
}
