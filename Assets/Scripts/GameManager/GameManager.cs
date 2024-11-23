using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    static public GameManager Manager {get; private set;}
    //private Data DataManager;

    [Header("Unvisible GameObjects on start")]

    [SerializeField] private GameObject StrickTMP;
    [SerializeField] private GameObject ScoreTMP;
    [SerializeField] private GameObject LivesTMP;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private GameObject GameOverPanel;

    [Space]

    [Header("Visible Gameobjects on start")]

    [SerializeField] private GameObject TapToPlayTMP;
    [SerializeField] private GameObject PaperBasketTMP;
    [SerializeField] private GameObject RusLangButton;
    [SerializeField] private GameObject EngLangButton2;

    [Header("Camera Rink")]

    [SerializeField] private SpriteRenderer _rink;

    [Header("Music")]

    [SerializeField] private AudioClip _backgrSound;
    private AudioSource _backgrSoundSrc;
    private float currPitch = 0.6f;
    private float currVolume = 0.1f;

    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool gameOver = false;
    private int _langIndex;
    private Dictionary<string,string[]> _translateMap = new()
    {
        {"PAPER BASKET", new string[]{"PAPER\nBASKET","ПАПЕР\nБАСКЕТ"}},
        {"TAP TO PLAY", new string[]{"TAP TO PLAY","НАЖМИ ЧТОБЫ ИГРАТЬ"}},
        {"SCORE", new string[]{"SCORE","СЧЕТ"}},
        {"MAX SCORE", new string[]{"MAX SCORE","МАКС. СЧЕТ"}},
        {"CONTINUE", new string[]{"CONTINUE","ПРОДОЛЖИТЬ"}},
    };


    private void Start()
    {
        Manager = this;
        
        Debug.Log(YandexGame.savesData.language);

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = _rink.bounds.size.x / _rink.bounds.size.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = _rink.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = _rink.bounds.size.y / 2 * differenceInSize;
        }

        if(Data.isGameContinue)
        {
            PaperBasketTMP.SetActive(false);
            StrickTMP.SetActive(true);
            ScoreTMP.SetActive(true);
            LivesTMP.SetActive(true);

            Data.isGameContinue = false;
        }

        Data.GetAllData();

        _langIndex = Data.GetLanguageIndex();


        UpdateAllText();

        _backgrSoundSrc = GetComponent<AudioSource>();
        PlayBackgroundMusic();

        //VolumeMuteOrUnmute(true);

        YandexGame.FullscreenShow();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += Data.GetAllData;
    }

    private void OnDisable()
    {
        Data.SaveAllData();
        YandexGame.GetDataEvent -= Data.GetAllData;
    }
    

    public void GameStart()
    {
        if (isRunning == false && gameOver == false)
        {
            isRunning = true;

            MainCanvas.Manager.UpdateScoreTable();
            
            PaperBasketTMP.SetActive(false);
            StrickTMP.SetActive(true);
            ScoreTMP.SetActive(true);
            LivesTMP.SetActive(true);

            TapToPlayTMP.SetActive(false);
            Spawner.SetActive(true);
            RusLangButton.SetActive(false);
            EngLangButton2.SetActive(false);

            currPitch = 1f;
            MusicSettings();
        }
    }

    public void GameContinue()
    {

    }

    public void GameOver()
    {
        isRunning = false;

        GameOverPanel.SetActive(true);
        StrickTMP.SetActive(false);
        ScoreTMP.SetActive(false);
        LivesTMP.SetActive(false);
        PaperBasketTMP.SetActive(true);
        Spawner.SetActive(false);
        RusLangButton.SetActive(true);
        EngLangButton2.SetActive(true);

        UnPlayBackgroundMusic();
    }
    
    public string GetStringUI(string value)
    {
        return _translateMap[value][_langIndex];
    }

    public void ChangeToEnglish()
    {
        Data.SetLanguageIndex(0);
        _langIndex = 0;
        UpdateAllText();
    }

    public void ChangeToRussian()
    {
        Data.SetLanguageIndex(1);
        _langIndex = 1;
        UpdateAllText();
    }

    public void UpdateAllText()
    {
        TextMeshProUGUI tapToPlay = TapToPlayTMP.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI paperBasket = PaperBasketTMP.GetComponent<TextMeshProUGUI>();

        tapToPlay.text = GetStringUI("TAP TO PLAY");
        paperBasket.text = GetStringUI("PAPER BASKET");

        MainCanvas.Manager.UpdateScoreTable();
    }
    
    public void PlayBackgroundMusic()
    {
        _backgrSoundSrc.clip = _backgrSound;
        _backgrSoundSrc.loop = true;
        _backgrSoundSrc.volume = currVolume;
        _backgrSoundSrc.pitch = currPitch;
        _backgrSoundSrc.Play();
    }

    public void UnPlayBackgroundMusic()
    {
        _backgrSoundSrc.Stop();
    }

    public void MusicSettings()
    {
        _backgrSoundSrc.volume = currVolume;
        _backgrSoundSrc.pitch = currPitch;
    }

    // public void VolumeMuteOrUnmute(bool Start = false)
    // {
    //     // if (!Start)
    //     // {
    //     //     if (Data.GetIsMuted())
    //     //     {
    //     //         AudioListener.volume = 1;
    //     //         Data.SetIsMuted(false);
    //     //     }
    //     //     else
    //     //     {
    //     //         AudioListener.volume = 0;
    //     //         Data.SetIsMuted(true);
    //     //     }
    //     // }
    //     // else
    //     // {
    //     //     if (Data.GetIsMuted())
    //     //         AudioListener.volume = 0;
    //     //     else
    //     //         AudioListener.volume = 1;
    //     // }   
    //     if(Start)
    //     {
    //         if (Data.GetIsMuted())
    //         {
    //             AudioListener.volume = 0;
    //             MainCanvas.Manager.ChangeVolume();
    //         }
    //     }
    // }
}
