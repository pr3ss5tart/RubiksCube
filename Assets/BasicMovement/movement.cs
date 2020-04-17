using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float movespeed;
    public Rigidbody rb;
    bool hasJump = false;
    public Collider box;
    float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 5f;    
        distToGround = box.bounds.size.y;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        ///Basic movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x,0f,z) * movespeed * Time.deltaTime;

        transform.Translate(move, Space.Self);

        /*Jump*/
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
                rb.AddForce(new Vector3(0f, 20f, 0f));
        }
    }
}
