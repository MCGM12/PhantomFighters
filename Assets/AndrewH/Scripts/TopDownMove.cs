using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMove : MonoBehaviour {
    public float scalar;
    public float speedCap;
    private Rigidbody2D rb2;

    private float inX = 0;
    private float inY = 0;

    // Start is called before the first frame update
    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //Player movement  
        inX = Input.GetAxis("Horizontal");
        inY = Input.GetAxis("Vertical");
        if (rb2.velocity.magnitude < speedCap) {
            rb2.AddForce(new Vector2(inX, inY) * scalar * Time.deltaTime);
        }
    }
}
