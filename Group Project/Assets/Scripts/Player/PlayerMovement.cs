using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float jumpHeight;
    public bool isGrounded;
    public LayerMask groundLayer;

    public float runSpeed;
    public float walkSpeed;
    public float sneakSpeed;
    public MoveSpeed moveSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = MoveSpeed.run;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = MoveSpeed.sneak;
        }
        else
        {
            moveSpeed = MoveSpeed.walk;
        }

        switch (moveSpeed)
        {
            case MoveSpeed.run:
                rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * runSpeed);
                break;
            case MoveSpeed.sneak:
                rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * sneakSpeed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * sneakSpeed);
                break;
            default:
                rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed);
                break;

        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        }
    }
    public enum MoveSpeed
    {
        run,
        walk,
        sneak
    }
}
