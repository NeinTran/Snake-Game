using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject title;
    public Snake player;
    private int score;


    private void Awake() {
        Application.targetFrameRate = 60;
        gameOver.SetActive(false);
        title.SetActive(true);
        Pause();
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();
        playButton.SetActive(false);
        gameOver.SetActive(false);
        title.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        player.ResetState();

    }

    public void Pause() {
        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void GameOver() {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        Pause();
    }
    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
    }
}
