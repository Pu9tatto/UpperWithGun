using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private string _tag;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == _tag)
        {
            _event.Invoke();
        }
    }
}
