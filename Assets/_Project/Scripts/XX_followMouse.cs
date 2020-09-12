using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XX_followMouse : MonoBehaviour
{
    private Vector3 targetPos = Vector3.zero;

    private float speed = 1f;
    private Plane plane;
    private void Start()
    {
        plane = new Plane(Vector3.up, 0);
    }

    private void Update()
    {
        

        float dist;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out dist))
        {
            Vector3 targetPos = ray.GetPoint(dist);
        }
      

        //targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targetDirection = transform.position - targetPos;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

    }
}
