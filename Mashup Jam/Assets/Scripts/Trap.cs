using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor.EditorGUI;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField]
    private float speed = 1F;

    private Vector3 start;

    void Start() {
        start = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float y = Mathf.Sin(Time.time * speed) * 0.5F - 0.5F;
        transform.position = start + new Vector3(0, y, 0);
    }
}
