using UnityEngine;

public class NetTrigger : MonoBehaviour
{
    [SerializeField] private GameObject FatherBackboard;
    [SerializeField] private GameObject _Ball;
    [SerializeField] private GameObject _Bomb;
    [SerializeField] private GameObject _Heart;
    [SerializeField] private AudioClip _netSound;
    private AudioSource _netSoundSrc;

    private Backboard _fatherBackboardScript;
    // public satic float shoots = 0f;
    // public sttic float shootsStrick = 1f;
    // public sttic float plusScore = 10f;
    // private flat takingMinusLives = 3f;

    private void Start()
    {
        _fatherBackboardScript = FatherBackboard.GetComponent<Backboard>();
        _netSoundSrc = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.transform.position.y > transform.position.y)
        {
            _fatherBackboardScript.PlayBackboardAnimation();
            PlaySound();

            if (collider.gameObject.name == _Ball.name + "(Clone)")
                BallCollide(collider.gameObject);

            else if (collider.gameObject.name == _Bomb.name + "(Clone)")
                BombCollide(collider.gameObject);

            else if (collider.gameObject.name == _Heart.name + "(Clone)")
               HeartCollide(collider.gameObject);
        }
    }

    private void BallCollide(GameObject GJ)
    {
        // shoots+= plusScore;
        // shootsStrick++;

        // MainCanvas.Manager.UpdateScoreTable("score","strick");
        // _fatherBackboardScript.ShowAddedScore($"+{plusScore}" , transform.position, new Color(1f, 0.565f, 0.035f, 1f));

        // Destroy(GJ.gameObject);

        ScoreManager.Manager.ChangeStrick();
        ScoreManager.Manager.ChangeScore();
        _fatherBackboardScript.ShowAddedScore($"+{ScoreManager.Manager.PLUSSCORE}" , transform.position, new Color(1f, 0.565f, 0.035f, 1f));
        Destroy(GJ.gameObject);
    }
    private void BombCollide(GameObject GJ)
    {
        // BallOutTrigger.lives-=takingMinusLives;
        // shootsStrick = 1f;
        // Destroy(GJ.gameObject);

        // if (BallOutTrigger.lives <= 0f)
        //     PBManager.Game.GameOver();

        // _fatherBackboardScript.ShowAddedScore($"-{takingMinusLives} <3" , transform.position, Color.gray);
        // MainCanvas.Manager.UpdateScoreTable("strick" , "lives");
        
        ScoreManager.Manager.ChangeStrick(true);
        ScoreManager.Manager.ChangeLives(false);
        _fatherBackboardScript.ShowAddedScore($"-{ScoreManager.Manager.MINUSLIVESBYBOMB} <3" , transform.position, Color.gray);
        Destroy(GJ.gameObject);
    }
    private void HeartCollide(GameObject GJ)
    {
        // BallOutTrigger.lives++;
        // _fatherBackboardScript.ShowAddedScore($"+ 1 <3" , transform.position, Color.red);
        // MainCanvas.Manager.UpdateScoreTable("lives");
        // Destroy(GJ.gameObject);
        ScoreManager.Manager.ChangeLives(true);
        _fatherBackboardScript.ShowAddedScore($"+ 1 <3" , transform.position, Color.red);
        Destroy(GJ.gameObject);
    }

    private void PlaySound(float volume = 1f)
    {
        _netSoundSrc.PlayOneShot(_netSound, volume);
    }
}
