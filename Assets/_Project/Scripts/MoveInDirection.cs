using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField] private Vector3 _direction = Vector3.zero;
    [SerializeField] private float _speed = 1f;

    void Update()
    {
        if (LevelManager.Instance.IsGameRunning)
        {
            //transform.Translate(_direction * _speed * Time.deltaTime);
            transform.Translate(_direction * LevelManager.Instance.GameSpeed * Time.deltaTime);
        }
    }
}
