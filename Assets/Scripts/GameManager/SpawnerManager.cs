using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [Header("Game Objects")]
    [SerializeField] private GameObject _Ball;
    [SerializeField] private GameObject _Bomb;
    [SerializeField] private GameObject _Heart;
    [SerializeField] private float _bombChance;
    [SerializeField] private float _heartChance;

    public static Vector2 spawnBallForce;
    private Vector2[] _spawnLocation = 
    {
        new(2f,2.5f),
        new(-2f,2.5f)
    };
    private Vector2[] _spawnForce = 
    {
        new(-65f,65f),
        new(65f,65f)
    };

    private void FixedUpdate()
    {
        float spawnFrequency = ScoreManager.Manager.GetSpawnFrequency();

        if (Time.fixedTime % spawnFrequency == 0f)
        {
            int num = Random.Range(0,_spawnLocation.Length);
            int randomNum = Random.Range(0,100);
            spawnBallForce = _spawnForce[num];

            if (randomNum <= _bombChance)
                Instantiate(_Bomb,_spawnLocation[num], new Quaternion(0f,0f,0f,0f));

            else if (randomNum <= _heartChance+_bombChance)
                Instantiate(_Heart,_spawnLocation[num], new Quaternion(0f,0f,0f,0f));

            else
                Instantiate(_Ball,_spawnLocation[num], new Quaternion(0f,0f,0f,0f));
        }
    }

    // public void SpawnAddedScoreTP(string addedScore, Vector2 triggerPosition, Color color)
    // {
    //     TextMeshProUGUI text = _AddedScoreTMP.GetComponent<TextMeshProUGUI>();
    //     text.text = addedScore;
    //     text.color = color;
    //     triggerPosition.y += _triggerHeight;
    //     Instantiate(_AddedScoreTMP, triggerPosition , new Quaternion(0f,0f,0f,0f), _scoreTMPparent);
    // }
}
