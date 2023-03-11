using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private Animator _spriteAnimator;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteAnimator = GetComponent<Animator>();
        _moveDirection = new Vector2();
    }

    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;
        if (moveY > 0)
        {
            _spriteAnimator.SetBool("FacingFront", false);
        }
        if (moveY < 0)
        {
            _spriteAnimator.SetBool("FacingFront", true);
        }
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveDirection * (speed * Time.fixedDeltaTime));
    }
}
