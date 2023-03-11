using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveDirection = new Vector2();
    }

    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

        _moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveDirection * (speed * Time.fixedDeltaTime));
    }
}
