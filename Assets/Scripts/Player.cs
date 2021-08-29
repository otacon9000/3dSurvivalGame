using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private Camera _playerCamera;

    [Header("Player Settings")]
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



    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        Movement();

        CameraController();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
      
    }

    private void Movement()
    {
        if (_cc.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontalInput, 0, verticalInput);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }
        _velocity.y -= _gravity * Time.deltaTime;
        _velocity = transform.TransformDirection(_velocity);
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
}
