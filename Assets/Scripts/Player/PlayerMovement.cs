using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 _moveDirection;
    
    void Start()
    {
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
        transform.Translate(Vector3.right * (_moveDirection.x * (speed * Time.fixedDeltaTime)));
        transform.Translate(Vector3.up * (_moveDirection.y * (speed * Time.fixedDeltaTime)));
    }
}
