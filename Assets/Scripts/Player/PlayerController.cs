using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Singleton

    public static PlayerController instance;

    #endregion

    #region Fields

    [Header("Player Info")]
    [SerializeField] int _viewDirection = 1;
    public bool canTeleport;
    [SerializeField] bool _movementStopped;
    Vector3 _lastVelocity;

    [Header("Movement Config")]
    [SerializeField] float _walkSpeed;
    public float windSpeed;

    [Header("Jump Config")]
    [SerializeField] LayerMask _groundMask;
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _fallMultiplier;
    [SerializeField] float _maxFallVelocity;
    [SerializeField] float _jumpForce;
    [SerializeField] float _coyoteTime;
    [SerializeField] bool _isGrounded;
    float _currentCoyoteTime;

    [Header("Wall Slide Config")]
    [SerializeField] LayerMask _wallMask;
    [SerializeField] Vector3 _slideRange;
    [SerializeField] float _slideSpeed;
    [SerializeField] float _wallJumpMultiplier;
    [SerializeField] float _wallJumpCoyoteTime;
    [SerializeField] bool _isSliding;
    int _wallJumpDirection;
    float _currentWallJumpCoyoteTime;

    [Header("Grappling Hook Config")]
    [SerializeField] LayerMask _grapplingHookMask;
    [SerializeField] DistanceJoint2D _grapplingHookJoint;
    [SerializeField] float _grapplingHookRadius;
    [SerializeField] bool _isGrappling;

    #endregion

    #region Cached Components

    Rigidbody2D _rigidBody;
    Transform _transform;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {

        _rigidBody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        // Singleton

        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

    }

    void Start()
    {

        

    }

    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0)
            _viewDirection = (int)xInput;

        // Jump Method
        _isGrounded = Physics2D.Linecast(new Vector2(_groundCheck.position.x + 0.375f * _viewDirection, _transform.position.y - 0.5f), new Vector2(_groundCheck.position.x + 0.35f * -_viewDirection, _groundCheck.position.y), _groundMask);

        if (_isGrounded)
            _currentCoyoteTime = _coyoteTime;
        else
            _currentCoyoteTime -= Time.deltaTime;

        if (_currentCoyoteTime > 0 && Input.GetButtonDown("Jump") && !_movementStopped)
        {
            _rigidBody.velocity = Vector2.zero;
            Jump();
        }

        // Wall Slide Method
        if (!_isSliding && !_isGrappling && !_movementStopped)
            _wallJumpDirection = (int)xInput;

        _isSliding = Physics2D.Linecast(new Vector2(_transform.position.x + 0.375f * _viewDirection, _transform.position.y), new Vector2(_transform.position.x + _slideRange.x * _wallJumpDirection, _transform.position.y - 0.25f), _wallMask) && !_isGrounded;

        if (_isSliding)
            _currentWallJumpCoyoteTime = _wallJumpCoyoteTime;
        else
            _currentWallJumpCoyoteTime -= Time.deltaTime;

        if (_currentWallJumpCoyoteTime > 0 && Input.GetButtonDown("Jump") && !_movementStopped)
            WallJump();

        // Grappling Hook Method
        if(Physics2D.OverlapCircle(_transform.position, _grapplingHookRadius, _grapplingHookMask) != null)
            _grapplingHookJoint = Physics2D.OverlapCircle(_transform.position, _grapplingHookRadius, _grapplingHookMask).gameObject.GetComponent<DistanceJoint2D>();
        else if(Physics2D.OverlapCircle(_transform.position, _grapplingHookRadius, _grapplingHookMask) == null && !_isGrappling)
            _grapplingHookJoint = null;
        

        if (!_isSliding && !_isGrounded && !_isGrappling && _grapplingHookJoint != null && !_movementStopped && Input.GetButtonDown("Fire1"))
        {

            _isGrappling = true;
            _grapplingHookJoint.connectedBody = _rigidBody;

        }

        if (_isGrappling && Input.GetButtonDown("Jump") && !_movementStopped)
        {

            _grapplingHookJoint.connectedBody = null;
            _isGrappling = false;

            _rigidBody.velocity = Vector2.zero;
            _rigidBody.AddForce(new Vector2((_jumpForce + _rigidBody.velocity.x) * (int)xInput, _jumpForce * 1.25f), ForceMode2D.Impulse);
            /*_rigidBody.AddForce(new Vector2((_jumpForce + _rigidBody.velocity.x) * _wallJumpMultiplier, 0), ForceMode2D.Force);*/

        }

        if (_movementStopped)
            _rigidBody.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {

        // Horizontal Movement Method
        float xInput = Input.GetAxisRaw("Horizontal");
        
        if(!_isGrappling && !_movementStopped)
            Move(xInput);

        // Falling
        if (!_isGrounded && !_isSliding && !_isGrappling && !_movementStopped && Input.GetButtonUp("Jump"))
            _currentCoyoteTime = 0;

        if(_currentCoyoteTime <= 0 && !Input.GetButton("Jump") && !_movementStopped)
        {

            if (_rigidBody.velocity.y < _maxFallVelocity)
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _maxFallVelocity);
            else
                _rigidBody.velocity += Vector2.up * _fallMultiplier;

        }

        if (_isSliding && _rigidBody.velocity.y <= 0 && !_movementStopped)
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _slideSpeed);

    }

    #endregion

    #region Private Methods

    void Move(float xInput)
    {

        _rigidBody.velocity = new Vector2((xInput * _walkSpeed * Time.fixedDeltaTime) + windSpeed * Time.fixedDeltaTime, _rigidBody.velocity.y);

    }

    void Jump()
    {

        _rigidBody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

    }

    void WallJump()
    {

        _currentWallJumpCoyoteTime = 0;

        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _rigidBody.AddForce(new Vector2(_jumpForce * _wallJumpMultiplier * (_isSliding ? -_wallJumpDirection : _wallJumpDirection), 0), ForceMode2D.Force);

    }

    #endregion

    #region Public Methods

    public void StopMovement()
    {

        _movementStopped = true;
        _lastVelocity = _rigidBody.velocity;
        _rigidBody.bodyType = RigidbodyType2D.Kinematic;

    }

    public void ReturnMovement()
    {

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _movementStopped = false;
        _rigidBody.velocity = _lastVelocity;

        if(_rigidBody.velocity.y <= 0.15f)
            _rigidBody.velocity -= Vector2.up * 15;

    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(new Vector2(transform.position.x + 0.375f * _viewDirection, transform.position.y), new Vector2(transform.position.x + _slideRange.x * _wallJumpDirection, transform.position.y - 0.25f));
        //Gizmos.DrawLine(new Vector2(_groundCheck.position.x + 0.375f * _viewDirection, transform.position.y - 0.5f), new Vector2(_groundCheck.position.x + 0.35f * -_viewDirection, _groundCheck.position.y));

        //Gizmos.DrawSphere(transform.position, _grapplingHookRadius);

    }

    #endregion

}
