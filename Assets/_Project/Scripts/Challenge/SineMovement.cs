using UnityEngine;

public class SineMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float height = 0.5f;

    private Vector3 pos = Vector3.zero;
    private void Start()
    {
        Vector3 pos = transform.position;
    }
    void Update()
    {
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y+2;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
