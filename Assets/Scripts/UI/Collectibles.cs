using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public TMP_Text TimerText;
    private int collectibles;
    public int totalCollectibles;
    private bool collected;
    private string collectiblesText;

    private void OnEnable()
    {
        PlayerController.E_SecretCollected += HandleCollectEvent;
    }

    private void OnDisable()
    {
        PlayerController.E_SecretCollected -= HandleCollectEvent;
    }

    void Start()
    {
        collectibles = 0;

        collectiblesText = $"{collectibles}/{totalCollectibles}";
        TimerText.text = collectiblesText;
    }

    void Update()
    {
        if (collected)
        {
            collectibles += 1;

            collectiblesText = $"{collectibles}/{totalCollectibles}";
            TimerText.text = collectiblesText;

            collected = false;
        }
    }

    void HandleCollectEvent()
    {
        collected = true;
    }
}