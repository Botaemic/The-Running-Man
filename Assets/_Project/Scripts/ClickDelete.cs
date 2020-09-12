using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDelete : MonoBehaviour
{
    void Update()
    {
        // Test for mouse click
        if (Input.GetMouseButtonUp(0))
        {
            //Create Ray with mouseposition in world space
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //cast a ray from the camera with the created Ray
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //delete object that was "clicked"
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
