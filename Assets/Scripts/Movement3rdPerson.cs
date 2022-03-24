using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3rdPerson : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float speed = 6f;
    public float jumpHeight = 3f;
    public int maxJumps = 2;
    public float turnSmoothTime = 0.1f;
    public float gravityMultiplier = 1;
    public float groundDistance = 0.2f;

    private float turnSmoothVelocity;
    private float baseGravity = -9.81f;
    private float gravity;
    private int currentJumps;
    private Vector3 velocity;
    private bool isGrounded;
    
    void Start(){
        currentJumps = maxJumps;
        gravity = baseGravity * gravityMultiplier;
    }
    void Update(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float moveLR = Input.GetAxisRaw("Horizontal");
        float moveFB = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveLR, 0f, moveFB).normalized;

        if (direction.magnitude > 0.1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && currentJumps > 1){
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            currentJumps -= 1;
        }

        if (isGrounded){
            currentJumps = maxJumps;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
