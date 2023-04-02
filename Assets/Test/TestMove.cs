using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class TestMove : NetworkBehaviour
{
    float moveSpeed = 2f;
    Rigidbody2D rb;
    public TextMesh textMesh;    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        textMesh.text = "Hello World";
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputDir = input.normalized;
        rb.velocity = inputDir * moveSpeed;
    }
}
