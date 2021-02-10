using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 transf;
    
    void Update()
    {
        transf.x = GameObject.FindWithTag("Player").transform.position.x;
        transf.y = GameObject.FindWithTag("Player").transform.position.y;
        transf.z = -10f;
 
        transform.position = transf;
    }
}
