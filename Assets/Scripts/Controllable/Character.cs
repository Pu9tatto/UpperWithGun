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
    [Header("isColliderChecker")]
    [SerializeField] private float _checkGroundRadius = 0.4f;
    [SerializeField] private Transform _groundCheckerPivot;
    [SerializeField] private Transform _roofCheckerPivot;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _controller;
    private Vector2 _direction;
    private float _velocityGravity;
    private float _velocityMove;
    private float _friction;
    private bool _isGrounded;
    private bool _isRoofed;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGrounded = ColliderChecker(_groundCheckerPivot);
        _isRoofed = ColliderChecker(_roofCheckerPivot);
        if (_isGrounded)
        {
            _friction = _frictionGround;
            if (_velocityGravity < 0)
            {
                _direction.y = 0;
                _velocityGravity = -_gravityOnGround;
            }
        }
        else
        {
            _friction = _frictionAir;
        }
        if (_isRoofed)
        {
            _direction.y = 0;
        }     


        DoGravity();
        DoFriction();
        Movement();
    }

    private void DoGravity()
    {
        _velocityGravity += -_gravity * Time.fixedDeltaTime;

        _controller.Move(_velocityGravity * Time.fixedDeltaTime * Vector3.up);
    }
    private void Movement()
    {
        _controller.Move(_velocityMove * Time.fixedDeltaTime * _direction);
    }

    private void DoFriction()
    {
        if(_velocityMove > 0)        
            _velocityMove-=_friction * Time.fixedDeltaTime;
        
    }

    private bool ColliderChecker(Transform checkerPivot) => Physics.CheckSphere(checkerPivot.position, _checkGroundRadius, _groundMask);
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundCheckerPivot.position, _checkGroundRadius);
        Gizmos.DrawSphere(_roofCheckerPivot.position, _checkGroundRadius);
    }


    public void Push()
    {
        _velocityGravity = 0;
   
        _direction = GetDirection(Utils.AngleBetweenTwoPints(Utils.GetMousePosition(), transform.position));
        _velocityMove = Mathf.Sqrt(_speed * 2 * _gravity);
    }

    private Vector2 GetDirection(float angle)
    {
        
        if(angle >= -22 && angle < 22)
            return new Vector2(-1, 0);
        if(angle >= 22 && angle < 66) 
            return new Vector2(-0.5f, -0.25f);
        if(angle>=66 && angle< 118)
            return new Vector2(0,-1);
        if (angle >= 118 && angle < 158)
            return new Vector2(0.5f, -0.25f);
        if (angle >= 158 || angle < -158)
            return new Vector2(1, 0);
        if (angle >= -158 && angle < -118)
            return new Vector2(0.5f, 0.9f);
        if (angle >= -118 && angle < -66)
            return new Vector2(0, 1);
        if (angle >= -66 && angle < -22)
            return new Vector2(-0.5f, 0.9f);
        return Vector2.zero;

    }

}
