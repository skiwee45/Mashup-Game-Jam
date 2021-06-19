using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 transformVector = new Vector3(1, 0, 0);
        transformVector = transformVector.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.transform.position + transformVector);
    }
}
