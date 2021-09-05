using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private CharacterController _cc;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private Camera _playerCamera;

    [Header("Player Settings")]
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _minHealth;
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _jumpHeight = 5;
    [SerializeField]
    private float _gravity = 20f;
    [Header("Camera Settings")]
    [SerializeField][Range(0.1f,1)]
    private float _horizontalSensitivity;
    [SerializeField][Range(0.1f, 1)]
    private float _verticalSensitivity;
    private float _yVelocity;

    public int Health { get; set; }



    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Health = _maxHealth;
        
    }

    private void Update()
    {
      
        Movement();
        ///*CalculateMovement*/();

        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
      
    }



    private void Movement()
    {
        if (_cc.isGrounded == true)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontalInput, 0, verticalInput);
            _velocity = _direction * _speed;

            _velocity = transform.TransformDirection(_velocity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;
        _cc.Move(_velocity * Time.deltaTime);
    }

    private void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //look left-right
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _horizontalSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //look up-down
        Vector3 currentRoatationCamera = _playerCamera.transform.localEulerAngles;
        currentRoatationCamera.x -= mouseY * _verticalSensitivity;
        currentRoatationCamera.x = Mathf.Clamp( currentRoatationCamera.x, 0f, 26f);
        _playerCamera.transform.localRotation = Quaternion.AngleAxis(currentRoatationCamera.x, Vector3.right);
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        

        if (Health < _minHealth)
        {
            Debug.Log(gameObject.name + " is dead");
            Destroy(this.gameObject);
        }
    }
}
