using System;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameOver : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI gameOverStatsTMP;
    [SerializeField] TextMeshProUGUI AdButtonTMP;
    [SerializeField] private GameObject CrossIMG;
    [SerializeField] private GameObject AdButton;
    private Button _adButton;
    public GameOver Game { get; private set; }
    private void Start()
    {
        _adButton = AdButton.GetComponent<Button>();

        CrossIMG.SetActive(false);

        GameManager.Manager.gameOver = true;

        if (Data.isSecondTry)
        {
            CrossIMG.SetActive(true);
            _adButton.interactable = false;
        }

        int dataPlayerScore = Data.GetPlayerScore();

        if (dataPlayerScore < ScoreManager.Manager.SCORE)
        {
            Data.SetPlayerScore(ScoreManager.Manager.SCORE);
            dataPlayerScore = ScoreManager.Manager.SCORE;
            YandexGame.NewLeaderboardScores("PaperBasketScoreLeaderboard", ScoreManager.Manager.SCORE);
        }

        gameOverStatsTMP.text = $"{GameManager.Manager.GetStringUI("SCORE")}:  {ScoreManager.Manager.SCORE}\n"
            +$"{GameManager.Manager.GetStringUI("MAX SCORE")}: {dataPlayerScore}";

        AdButtonTMP.text = $"{GameManager.Manager.GetStringUI("CONTINUE")}";
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }


    public void GamePlayOver()
    {

    }
    public void OnRetryButton()
    {
        if (Data.isSecondTry)
        {
            Data.isSecondTry = false;
            _adButton.interactable = false;
            CrossIMG.SetActive(false);
        }
        SceneManager.LoadScene(0);
    }
    public void OnAdButton()
    {
        YandexGame.RewVideoShow(1);

    }

    public void OnMenuButton()
    {

    }

    private void GameContinue()
    {
        Data.isSecondTry = true;
        Data.isGameContinue = true;
        ScoreManager.Manager.ContinueScore();
        SceneManager.LoadScene(0);
    }
    public void Rewarded(int id)
    {
        if (id == 1)
            GameContinue();
    }
}
