using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _distanceText;
    
    private float _totalDistance;

    [SerializeField] private PlayerController player;

    void Update()
    {
        _totalDistance += player.GetMoveSpeed() * Time.deltaTime;
        _distanceText.text = $"Distance:{(int)_totalDistance}m";
    }
}