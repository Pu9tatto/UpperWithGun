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
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isRoofed;

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

        _controller.Move(Vector3.up * _velocityGravity * Time.fixedDeltaTime);
    }

    private void DoFriction()
    {
        if(_velocityMove > 0)
        {
            _velocityMove-=_friction * Time.fixedDeltaTime;
        }
    }

    private bool ColliderChecker(Transform checkerPivot)
    {
        bool result = Physics.CheckSphere(checkerPivot.position, _checkGroundRadius, _groundMask);
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_groundCheckerPivot.position, _checkGroundRadius);
        Gizmos.DrawSphere(_roofCheckerPivot.position, _checkGroundRadius);
    }

    private void Movement()
    {
        _controller.Move(_direction * _velocityMove * Time.fixedDeltaTime);
    }

    public void Push()
    {
        _velocityGravity = 0;

        //_direction = -(Utils.GetMousePosition()-transform.position).normalized;
        _direction = GetDirection(AngleBetweenTwoPints(Utils.GetMousePosition(), transform.position));
        _velocityMove = Mathf.Sqrt(_speed * 2 * _gravity);
        Debug.Log(AngleBetweenTwoPints(Utils.GetMousePosition(), transform.position));
    }

    private float AngleBetweenTwoPints(Vector3 pos1, Vector3 pos2) => Mathf.Atan2(pos1.y - pos2.y, pos1.x-pos2.x)* Mathf.Rad2Deg;

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
