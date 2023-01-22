using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed;

    private Vector3 _position;


    private void Update()
    {
        _position = transform.position;
        _position = Vector3.Lerp(_position, _target.position, _speed*Time.deltaTime);
        _position.z = -10;
        transform.position = _position;
    }
}
