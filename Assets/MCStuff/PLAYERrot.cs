using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERrot : MonoBehaviour
{
    float speed = 20f;
    GameObject player;

    void Start()
    {
       player = gameObject;
    }

    
    void Update()
    {
        Rotation();
    }
    private void Rotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
