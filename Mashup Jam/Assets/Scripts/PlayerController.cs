using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 transformVector = new Vector3(10, 0, 0);
        transformVector = transformVector.normalized * speed;
        rb.MovePosition(rb.transform.position + transformVector);
    }
}
