using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _squareSpeed;
    [SerializeField] private GameObject _WallRight;
    [SerializeField] private GameObject _WallLeft;
    [SerializeField] private GameObject _trampolineIMG;
    private Animator _TrampolineController;
    private float _leftWallHitBox;
    private float _rightWallHitBox;
    private float fixedTimePosX = 0f;
    public static Vector2 SquareMovingForce = new Vector2(0f,0f);

    private void Start()
    {
        _TrampolineController = _trampolineIMG.GetComponent<Animator>();
        _leftWallHitBox = _WallLeft.transform.position.x + _WallLeft.transform.localScale.x/2f + transform.localScale.x/2f;
        _rightWallHitBox = _WallRight.transform.position.x - _WallRight.transform.localScale.x/2f - transform.localScale.x/2f;
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
            MovingMobile();

        else
            MovingPC();
    }   

    private void FixedUpdate()
    {
        fixedTimePosX = transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            PlayAnimation();
        }
    }


    private void MovingMobile()
    {
        if (Input.touchCount > 0) 
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 platformPosBeforeMoving = new(transform.position.x,transform.position.y,transform.position.z);
                transform.position = new(Math.Clamp(touchPosition.x,  _leftWallHitBox ,_rightWallHitBox),
                    transform.position.y);
            }
        SquareMovingForce = new(Math.Clamp((transform.position.x - fixedTimePosX) *_squareSpeed *2f,-15f,15f), 0f);  
    }

    private void MovingPC()
    {
        if (Input.GetMouseButton(0)) 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 platformPosBeforeMoving = new(transform.position.x,transform.position.y,transform.position.z);
                transform.position = new(Math.Clamp(mousePos.x,  _leftWallHitBox ,_rightWallHitBox),
                        transform.position.y);
                
                SquareMovingForce = new(Math.Clamp((transform.position.x - fixedTimePosX) *_squareSpeed *2f,-15f,15f), 0f);
            }
    }

    private void PlayAnimation()
    {
        _TrampolineController.SetTrigger("collision");
    }

}
