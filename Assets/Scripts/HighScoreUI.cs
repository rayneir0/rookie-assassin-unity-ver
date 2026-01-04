using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI bestTimeText;

    void Update()
    {
        if (GameManager.Instance == null) return;

        // Shows the current time text saved from the gameTimer in GameManager
        // Time stops in the WinMessage and gets sent to GameManager -> Uses that time
        float time = GameManager.Instance.gameTimer;
        currentTimeText.text = $"{time:F2}s";

        // Best time is the lowest saved time
        float bestTime = GameManager.Instance.bestTime;

        if (bestTime < 0)
            bestTimeText.text = "N/A";
        else
            bestTimeText.text = $"{bestTime:F2}s";
    }
}
