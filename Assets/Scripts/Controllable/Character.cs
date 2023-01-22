using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour, IControllable
{
    [SerializeField] private float _speed = 5f;
    [Header("Phisic")]
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _gravityOnGround = 2;
    [SerializeField] private float _frictionAir = 0.1f;
    [SerializeField] private float _frictionGround = 1f;
    [Header("isGroundChecker")]
    [SerializeField] private float _checkGroundRadius = 0.4f;
    [SerializeField] private Transform _groundCheckerPivot;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;
    private Vector2 _direction;
    private float _velocityGravity;
    private float _velocityMove;
    private float _friction;
    private bool _isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsOnGround();
        if (_isGrounded)
        {
            _friction = _frictionGround;
            if (_velocityGravity < 0)
            {
                _velocityGravity = -_gravityOnGround;
            }
        }
        else
        {
            _friction = _frictionAir;
        }
            


        DoGravity();
        DoFriction();
        Movement();
    }

    private void DoGravity()
    {
        _velocityGravity += -_gravity * Time.fixedDeltaTime;

        _controller.Move(Vector3.up * _velocityGravity * Time.fixedDeltaTime);
    }

    private void DoFriction()
    {
        if(_velocityMove > 0)
        {
            _velocityMove-=_friction * Time.fixedDeltaTime;
        }
    }

    private bool IsOnGround()
    {
        bool result = Physics.CheckSphere(_groundCheckerPivot.position, _checkGroundRadius, _groundMask);
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundCheckerPivot.position, _checkGroundRadius);
    }

    private void Movement()
    {
        _controller.Move(_direction * _velocityMove * Time.fixedDeltaTime);
    }

    public void Push()
    {
        _velocityGravity = 0;
        _direction = -(Utils.GetMousePosition()-transform.position).normalized;
        _velocityMove = Mathf.Sqrt(_speed * 2 * _gravity);
    }
}
