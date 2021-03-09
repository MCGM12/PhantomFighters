using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown2 : MonoBehaviour {
    public float scalar;
    public float speedCap;
    private Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //Player movement  
        float inX = Input.GetAxis("Horizontal");
        float inY = Input.GetAxis("Vertical");
        rb2.velocity = new Vector2(inX, inY) * scalar;
        /*if (rb2.velocity.magnitude < speedCap) {
            rb2.velocity += new Vector2(inX, inY) * acceleration * Time.deltaTime;
        }*/
    }
}
