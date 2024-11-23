using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class Data
{
    // static public Data Manager { get ; private set ; }
    static public int dataScore = 0;
    static public bool isGameContinue = false;
    static public bool isSecondTry = false;
    static private int _languageIndex;
    static private bool _isMuted;
    static private int _playerScore;

    static public int GetLanguageIndex()
    {
        // if (PlayerPrefs.HasKey("languageIndex"))
        //     return PlayerPrefs.GetInt("languageIndex");

        // return 1;
        return _languageIndex;
    }

    static public void SetLanguageIndex(int languageIndex)
    {
        // PlayerPrefs.SetInt("languageIndex",languageIndex);
        _languageIndex = languageIndex;
        YandexGame.savesData.languageIndex = _languageIndex;
    }

    static public bool GetIsMuted()
    {
        return _isMuted;
    }

    static public void SetIsMuted(bool isMuted)
    {
        //PlayerPrefs.SetInt("IsMuted", Convert.ToInt16(isMuted));
        _isMuted = isMuted;
        YandexGame.savesData.isMuted = _isMuted;
    }

    static public int GetPlayerScore()
    {
        // if (PlayerPrefs.HasKey("playerScore"))
        //     return PlayerPrefs.GetInt("playerScore");
        
        // return 0;
        return _playerScore;
    }

    static public void SetPlayerScore(int playerScore)
    {
        //PlayerPrefs.SetInt("playerScore", playerScore);
        _playerScore = playerScore;
        YandexGame.savesData.playerScore = _playerScore;
    }

    static public void GetAllData()
    {
        //_languageIndex = YandexGame.savesData.language == "ru" ? 1 : 0;
        _languageIndex = YandexGame.savesData.languageIndex;
        _isMuted = YandexGame.savesData.isMuted;
        _playerScore = YandexGame.savesData.playerScore;
    }

    static public void SaveAllData()
    {
        //YandexGame.savesData.language = _languageIndex == 0 ? "en" : "ru";
        YandexGame.SaveProgress();
    }
}
