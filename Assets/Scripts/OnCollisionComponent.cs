using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private string _tag;


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _tag)
        {
            _event.Invoke();
        }
            Debug.Log("CollisinWithPlaye");
    }
}
