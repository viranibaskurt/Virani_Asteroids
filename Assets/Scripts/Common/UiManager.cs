using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    public Button PlayButton => playButton;
    private void Start()
    {
        SetMainScreenActive(true);
        SetGameOverScreenActive(false);
        playButton.onClick.AddListener(OnPlayPressed);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayPressed);
    }

    public void SetMainScreenActive(bool state)
    {
        playButton.gameObject.SetActive(state);
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnPlayPressed()
    {
        SetMainScreenActive(false);
    }

    public void SetGameOverScreenActive(bool state)
    {
        gameOverText.gameObject.SetActive(state);
    }
}
