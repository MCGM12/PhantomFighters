using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown2 : MonoBehaviour {
    public float currScalar;
    private float scalar;
    public float phaseScalar;
    public float powerUpScalar;
    private Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        scalar = currScalar;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //Player movement  
        float inX = Input.GetAxis("HorizontalMove");
        float inY = Input.GetAxis("VerticalMove");
        rb2.velocity = new Vector2(inX, inY) * currScalar;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.tag == "InnerWall") {
            currScalar /= phaseScalar;
        }
        if (collision.tag == "Powerup") {
            currScalar *= powerUpScalar;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "InnerWall") {
            currScalar = scalar;
        }
    }
}
