using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField] private Vector3 _startLocation = Vector3.zero;

    private float _repeatWidth =0;

    void Start()
    {
        _startLocation = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        if (transform.position.x < _startLocation.x - _repeatWidth)
        {
            transform.position = _startLocation;
        }
    }
}
