using UnityEngine;

public class HumanoidMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private string _inputHorizontal = "Horizontal";
    [SerializeField] private string _inputJump = "Jump";
    [SerializeField] private string _inputFall = "Fall";

    [Header("Tunning Params")]
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _jumpForceY = 12f;
    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _minStepMovement = 0.01f;
    [SerializeField] [Range(0f,1f)] private float _airControlRatio = 0.5f;
    [SerializeField] private float _fallingTime = 0.15f;

    [Header("Sensing")]
    [SerializeField] private Collider2D _footCollider;


    private float _horizontalDirection = 0f;
    private float _currentSpeed = 0f;
    private float _currentYAcceleration;
    private bool _jump = false;
    private bool _fall = false;
    private bool _descending = false;
    private bool _ableToJump = false;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_footCollider == null)
            _footCollider = GetComponentInChildren<Collider2D>();
    }

    void Update()
    {
        _horizontalDirection = Input.GetAxisRaw(_inputHorizontal);
        _jump = Input.GetAxisRaw(_inputJump) > 0f && _ableToJump;
        _fall = Input.GetAxisRaw(_inputFall) < 0f;

        if (_fall && _footCollider.enabled && _ableToJump)
        {
            _footCollider.enabled = false;
            _descending = true;
            _ableToJump = false;
            Invoke("ActivateFoot", _fallingTime);
        }

        if(_currentYAcceleration < 0f && !_descending && !_footCollider.enabled)
            _footCollider.enabled = true;
    }

    void FixedUpdate()
    {
        if (_jump)
            Jump();
        Vector2 l_movementY = Vector2.zero;
        Vector2 l_movementX = Vector2.zero;

        if (!_ableToJump)
            l_movementY = Fall();

        l_movementX = Move();
        l_movementX *= _ableToJump ? 1f : _airControlRatio;

        _rigidbody.MovePosition(_rigidbody.position + l_movementY + l_movementX);

    }

    private Vector2 Move()
    {
        float l_acceleration = _horizontalDirection != 0 ? _acceleration * _horizontalDirection : Mathf.Sign(_currentSpeed) * -1 * _acceleration;
        _currentSpeed = Mathf.Clamp(_currentSpeed + l_acceleration, -1*_maxSpeed, _maxSpeed);

        if(_currentSpeed*Time.fixedDeltaTime > _minStepMovement || _currentSpeed * Time.fixedDeltaTime < -1 * _minStepMovement)
            return Vector2.right * _currentSpeed * Time.fixedDeltaTime;

        return Vector2.zero;
    }

    private void Jump()
    {
        _currentYAcceleration = _jumpForceY;
        _footCollider.enabled = false;
        _ableToJump = false;
    }

    private Vector2 Fall()
    {
        _currentYAcceleration = Mathf.Clamp(_currentYAcceleration + Physics2D.gravity.y*Time.fixedDeltaTime, Physics2D.gravity.y,_jumpForceY);
        return Vector2.up * ((0.5f * _currentYAcceleration) * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Car" && _footCollider.enabled == true)
            _ableToJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(_footCollider.enabled == true)
            _ableToJump = false;
    }

    public void ActivateFoot()
    {
        _footCollider.enabled = true;
        _descending = false;
    }
}
