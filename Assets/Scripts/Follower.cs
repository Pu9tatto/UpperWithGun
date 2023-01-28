using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _pivot;

    private Vector3 _position;


    private void Update()
    {
        _position = transform.position;
        _position = Vector3.Lerp(_position, _target.position + _pivot, _speed*Time.deltaTime);
        _position.z = -10;
        transform.position = _position;
    }
}
