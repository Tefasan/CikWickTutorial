using System.IO.Pipes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
[Header("References")]
[SerializeField] private Transform _orientationTransform;

[Header("Movement Settings")]
[SerializeField] private KeyCode _movementKey;
[SerializeField] private float _movementSpeed;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _canJump;
    [Header ("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideGrag;
     [Header("Ground Check Settings")]
     [SerializeField] private float _playerHeight;
     [SerializeField] private LayerMask _groundLayer;
     [SerializeField] private float _groundDrag;

    private Rigidbody _playerRigidbody;

    private float _horizontalInput, _verticalTnput;
    private Vector3 _movementDirecttion;
    
    private bool _isSliding;
    
    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    void Update()
    {
        SetInputs();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }


    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalTnput = Input.GetAxisRaw("Vertical");
          
        if(Input.GetKeyDown(_slideKey))
        {
                _isSliding = true;
                Debug.Log("Player Sliding!");
        }
        else if(Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
            Debug.Log("Player Moving!");
        }
        else if(Input.GetKey(_jumpKey)  && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlaterJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirecttion = _orientationTransform.forward * _verticalTnput
            + _orientationTransform.right * _horizontalInput;
            if(_isSliding)
            {
                _playerRigidbody.AddForce(_movementDirecttion.normalized * _movementSpeed * _slideMultiplier, ForceMode.Force);
            }
            else
            {
                _playerRigidbody.AddForce(_movementDirecttion.normalized * _movementSpeed, ForceMode.Force);
            }
    }

    private void SetPlayerDrag()
    {
            if(_isSliding)
            {
                _playerRigidbody.linearDamping = _groundDrag;
            }
            else
            {
                _playerRigidbody.linearDamping= _groundDrag;
            }
    }


    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        if(flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity
            = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }
    private void SetPlaterJumping()
    {
        _playerRigidbody.linearVelocity = new Vector3 (_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
            _canJump = true;
    }


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}