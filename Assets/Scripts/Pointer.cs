using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform _aim;

    private void LateUpdate()
    {
        _aim.position = Utils.GetMousePosition();
    }

}