using TMPro;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private GameObject _ScoreTMP;
    [SerializeField] private GameObject _LivesTMP;
    [SerializeField] private GameObject _StrickTMP;
    private Animator _scoreTMPController;
    private Animator _livesTMPController;
    private Animator _strickTMPController;
    private TextMeshProUGUI _strickTMP;
    private TextMeshProUGUI _scoreTMP;
    private TextMeshProUGUI _livesTMP;

    void Start()
    {   
        _scoreTMPController = _ScoreTMP.GetComponent<Animator>();
        _livesTMPController = _LivesTMP.GetComponent<Animator>();
        _strickTMPController = _StrickTMP.GetComponent<Animator>();

        _scoreTMP = _ScoreTMP.GetComponent<TextMeshProUGUI>();
        _livesTMP = _LivesTMP.GetComponent<TextMeshProUGUI>();
        _strickTMP = _StrickTMP.GetComponent<TextMeshProUGUI>();
    }

    public void ScoreTableUpdate()
    {
        _strickTMP.text = $"X{ScoreManager.Manager.STRICK}";
        _scoreTMP.text = $"{GameManager.Manager.GetStringUI("SCORE")}: {ScoreManager.Manager.SCORE}";
        _livesTMP.text = $"<3  {ScoreManager.Manager.LIVES}  <3";
    }

    public void PlayScoreTableUpdateAnim(params string[] row)
    {
        foreach (string animName in row)
        {
            if (animName == "lives" || row.Length == 0) 
                _livesTMPController.SetTrigger("run");

            if (animName == "score" || row.Length == 0) 
                _scoreTMPController.SetTrigger("run");

            if (animName == "strick" || row.Length == 0) 
                _strickTMPController.SetTrigger("run");
        }
    }
}


