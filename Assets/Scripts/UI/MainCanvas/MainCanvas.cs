using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas Manager {get; private set;}
    [SerializeField] private Sprite _mutePNG;
    [SerializeField] private Sprite _unmutePNG;
    [SerializeField] private GameObject volumeButton;
    private Image _volumeImage;
    private ScoreTable _scoreTable;

    private void Start()
    {
        Manager = this;
        _scoreTable = GetComponentInChildren<ScoreTable>();
        _volumeImage = volumeButton.GetComponent<Image>();

        ChangeVolume(true);
    }
    
    public Transform GetTransform()
    {
        return transform;
    }

    public void UpdateScoreTable(params string[] animRow)
    {
        _scoreTable.ScoreTableUpdate();
        _scoreTable.PlayScoreTableUpdateAnim(animRow);
    }

    public void ChangeToEnglishButton()
    {
        GameManager.Manager.ChangeToEnglish();
    }
    
    public void ChangeToRussianButton()
    {
        GameManager.Manager.ChangeToRussian();
    }

    public void ChangeVolume(bool start)
    {
        bool isMuted = Data.GetIsMuted();

        if (start)
        {
            if(isMuted)
            {
                _volumeImage.sprite = _mutePNG;
                AudioListener.volume = 0f;
            }
            else
            {
                _volumeImage.sprite = _unmutePNG;
                AudioListener.volume = 1f;
            }
        }
        else
        {
            Data.SetIsMuted(!isMuted);
            AudioListener.volume = isMuted == true ? 1f : 0f;
            _volumeImage.sprite = isMuted == true ? _unmutePNG : _mutePNG;
            // if(isMuted)
            // {
            //     Data.SetIsMuted(false);
            //     AudioListener.volume = 1f;
            //     _volumeImage.sprite = _unmutePNG;
            // }
            // else
            // {
            //     Data.SetIsMuted(true);
            //     AudioListener.volume = 0f;
            //     _volumeImage.sprite = _mutePNG;
            // }
        }
    }
}
