using TMPro;
using UnityEngine;

public class Backboard : MonoBehaviour
{
    // [SerializeField] private GameObject _NetLeftIMG;
    [SerializeField] private GameObject _NetIMG;
    [SerializeField] private GameObject _AddedScoreTMP;
    [SerializeField] private float _AddedScorePositionY;
    private TextMeshProUGUI _textAddedScore;
    private Animator _AddedScoreTMPController;
    // private Animator _NetControllerLeft;
    private Animator _NetController;

    void Start()
    {
        _AddedScoreTMPController = _AddedScoreTMP.GetComponent<Animator>();
        _NetController = _NetIMG.GetComponent<Animator>();
        _textAddedScore = _AddedScoreTMP.GetComponent<TextMeshProUGUI>();
    }

    public void PlayBackboardAnimation()
    {
        _NetController.SetTrigger("shoot");
    }

    public void ShowAddedScore(string addedScore, Vector2 triggerPosition, Color color)
    {
        _textAddedScore.text = addedScore;
        _textAddedScore.color = color;
        
        Vector2 addedScorePosition = new(triggerPosition.x, triggerPosition.y+_AddedScorePositionY);
        Transform canvasTransform = MainCanvas.Manager.GetTransform();
        
        
        Instantiate(_AddedScoreTMP, addedScorePosition , new Quaternion(0f,0f,0f,0f), canvasTransform);
    }
}
