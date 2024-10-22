using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    private float _speedAmount = 3f;
    private float _duration = 5f;
    private string type = "speed";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision?.GetComponent<PlayerController>();
        if (player != null)
        {
            Buff speedBuff = new Buff(
                type,
                _duration,
                () => { player.ModifySpeed(_speedAmount); },
                () => { player.ModifySpeed(-_speedAmount); }
            );
            player.ApplyBuff(speedBuff);
            Destroy(gameObject);
        }
    }
}