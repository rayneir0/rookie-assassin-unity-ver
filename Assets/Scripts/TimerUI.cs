using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    void Update()
    {
        if (GameManager.Instance == null) return;
        // Formats the time
        float time = GameManager.Instance.gameTimer;
        timerText.text = $"{time:F2} s";
    }
}
