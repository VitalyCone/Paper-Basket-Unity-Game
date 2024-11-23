using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject _Platform;
    [SerializeField] private int _linePoints;
    [SerializeField] private AudioClip _hitSound;
    private AudioSource _hitSoundSrc;
    private LineRenderer _line;
    private Vector3[] _linePositions;
    private Rigidbody2D _RB;

    void Start()
    {
        _hitSoundSrc = GetComponent<AudioSource>();
        _RB = GetComponent<Rigidbody2D>();
        _line = GetComponent<LineRenderer>();
        _RB.AddForce(SpawnerManager.spawnBallForce);

        _linePositions = new Vector3[_linePoints];
        _line.positionCount = _linePoints;

        for (int i = 0; i< _linePoints; i++)
        {
            _linePositions[i] = transform.position;
        }
    }

    void FixedUpdate()
    {
        DrawPath();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        MoveAndBouncing(collision);
        PlaySound();
    }

    private void MoveAndBouncing(Collision2D collision)
    {
        Vector2 ForcedVector;
        float impactForce = collision.relativeVelocity.magnitude;
        Vector2 impactDirection = collision.relativeVelocity.normalized;
        Vector2 collisionNormal = collision.contacts[0].normal;
        Vector2 reflectedDirection = Vector2.Reflect(impactDirection, collisionNormal);

        if (collision.gameObject.tag == "Backboard" || collision.gameObject.name == name)
        {
            ForcedVector = new(-reflectedDirection.x*impactForce*10f,-reflectedDirection.y*impactForce*10f);
        }
        else if (collision.gameObject.name == _Platform.name)
        {
            ForcedVector = new(-reflectedDirection.x*350f,-reflectedDirection.y*350f);
            ForcedVector+=-Platform.SquareMovingForce*30f;
        }

        else
        {
            ForcedVector = new(-reflectedDirection.x*impactForce*25f,-reflectedDirection.y*impactForce*25f);
        }

        if (_RB != null)
            _RB.AddForce(ForcedVector);
    }

    private void DrawPath()
    {
        for (int i = _linePoints - 1; i > 0; i--)
        {
            _linePositions[i] = _linePositions[i - 1];
        }
        _linePositions[0] = transform.position;

        _line.SetPositions(_linePositions);
    }

    public GameObject GetBallObjectData()
    {
        return gameObject;
    }

    private void PlaySound(float volume = 1f)
    {
        _hitSoundSrc.PlayOneShot(_hitSound, volume);
    }
}
