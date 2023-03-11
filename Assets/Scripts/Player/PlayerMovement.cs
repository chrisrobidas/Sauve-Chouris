using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _movement = new Vector2();
    }

    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * (speed * Time.fixedDeltaTime));
    }
}
