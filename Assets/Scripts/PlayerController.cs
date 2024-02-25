using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _playerInput;

    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float turningSpeed = 5f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        GetPlayerInput();
        Look();
    }
    private void LateUpdate()
    {
        Move();
    }

    private void GetPlayerInput()
    {
        _playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void Look()
    { 
        if(_playerInput != Vector3.zero)
        {

            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            var newInput = matrix.MultiplyPoint3x4(_playerInput);


            var relative = (transform.position + newInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turningSpeed * Time.deltaTime);
        }
    }
    private void Move()
    {
        _rigidbody.MovePosition(transform.position + (transform.forward * _playerInput.magnitude) * movementSpeed * Time.deltaTime);
    }
}
