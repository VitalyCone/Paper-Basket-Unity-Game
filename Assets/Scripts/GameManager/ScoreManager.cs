using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager: MonoBehaviour
{
    static public ScoreManager Manager{get;private set;}

    [HideInInspector] public int SCORE = 0;
    [HideInInspector] public int STRICK = 1;
    [HideInInspector] public int LIVES = 3;
    [HideInInspector] public int PLUSSCORE = 10;
    [HideInInspector] public int PLUSLIVES = 1;
    [HideInInspector] public  int MINUSLIVESBYBOMB = 3;
    private const int constLives = 3;
    private float _spawnFrequency;


    private void Awake()
    {
        Manager = this;

        SCORE = Data.dataScore;
        Data.dataScore = 0;
    }

    public void ResetAll()
    {
        ChangeScore(true);
        ChangeStrick(true);
        LIVES = constLives;

        MainCanvas.Manager.UpdateScoreTable();
    }
    public void ChangeScore(bool isGameOver = false)
    {
        if (!isGameOver)
            SCORE+= GetPlusScore();
        else
            SCORE = 0;
        
        MainCanvas.Manager.UpdateScoreTable("score");
    }

    public void ContinueScore()
    {
        Data.dataScore += SCORE;
    }

    public void ChangeStrick(bool isStrickOver = false)
    {
        if (!isStrickOver)
            STRICK++;
        else
            STRICK = 1;
        MainCanvas.Manager.UpdateScoreTable("strick");
    }
    public void ChangeLives(bool isPlusLives = true, bool isBomb = true)
    {  
        if (isPlusLives)
            LIVES++;

        else if (!isBomb)
        {
            LIVES--;
            STRICK = 1;
        }
        else if (isBomb)
        {
            LIVES -= MINUSLIVESBYBOMB;
            STRICK = 1;
        }
        CheckLivesGameOver();

        MainCanvas.Manager.UpdateScoreTable("lives");
    }
    public int GetPlusScore()
    {
        if (STRICK > 15)
        {
            PLUSSCORE = 50;
        }
        else if (STRICK > 10)
        {
            PLUSSCORE = 40;
        }
        else if (STRICK > 5)
        {
            PLUSSCORE = 30;
        }
        else if (STRICK > 2)
        {
            PLUSSCORE = 20;
        }
        else
        {
            PLUSSCORE = 10;
        }

        return PLUSSCORE;
    }
    
    public float GetSpawnFrequency()
    {
        if (STRICK > 15)
        {
            _spawnFrequency = 0.5f;
        }
        else if (STRICK > 10)
        {
            _spawnFrequency = 1;
        }
        else if (STRICK > 5)
        {
            _spawnFrequency = 1.5f;
        }
        else if (STRICK > 2)
        {
            _spawnFrequency = 2f;
        }
        else
        {
            _spawnFrequency = 3f;
        }

        return _spawnFrequency;
    }

    private void CheckLivesGameOver()
    {
        if(LIVES <= 0)
        {
            GameManager.Manager.GameOver();
        }
    }
}