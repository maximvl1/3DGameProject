using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    //Movement related
    public float walkMoveSpeed = 8f, sprintMoveSpeed = 13f;
    float moveSpeed;
    [HideInInspector]
    public Vector3 move;


    //Gravity related
    public float gravity = -65f, jumpHeight = 3f;
    Transform groundCheck;
    LayerMask groundMask;
    float groundDistance = 0.4f;
    //[HideInInspector]
    public Vector3 gravityVelocity;


    //WallRun related
    Transform leftCheck;
    Transform rightCheck;
    LayerMask wallMask;
    float wallJumpCount = 1;


    //Sliding Related
    public float slideTime = 0.25f, slideAmount = 3f;
    float slideTimeLeft;
    [HideInInspector]
    public bool isSliding, isGrounded, isSprinting, wallLeft, wallRight, canWallJump, isMoving, playFootstep;

    //Head bobbing related
    public float headBobTime = 0.2f, headBobAmount = 1f;
    float headBobTimeLeft = 0.2f;
    [HideInInspector]
    public bool headBobUpDirection = false;

    //Fall Damage related
    [HideInInspector]
    public bool fallDamageTriggered = false, triggerDeath = false;
    public float fallDamageThreshold = -100;


    void Start()
    {
        groundCheck = transform.GetChild(0).gameObject.GetComponentInChildren<Transform>();
        groundMask = LayerMask.GetMask("Ground");

        leftCheck = transform.GetChild(1).gameObject.GetComponentInChildren<Transform>();
        rightCheck = transform.GetChild(2).gameObject.GetComponentInChildren<Transform>();
        wallMask = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        checkWalls();
        checkGrounded();
        Move();
        ApplyGravity();
    }

    void Move()
    {
        if (isGrounded && gravityVelocity.y < 0)
            gravityVelocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (z != 0 || x != 0)
        {
            isMoving = true;
            headBobTimeLeft -= Time.deltaTime;
            if (headBobTimeLeft <= 0)
            {
                if (!isSprinting)
                    headBobTimeLeft = headBobTime;
                else
                    headBobTimeLeft = headBobTime / 2;

                if (!headBobUpDirection && isGrounded && !isSliding)
                    playFootstep = true;

                headBobUpDirection = !headBobUpDirection;
            }
            else
                playFootstep = false;

            float headBobAmountNow;
            if (headBobUpDirection)
                headBobAmountNow = headBobAmount;
            else
                headBobAmountNow = -headBobAmount;

            if (isGrounded && !isSliding)
                Camera.main.transform.Translate(new Vector3(0, headBobAmountNow, 0), Space.World);
            else
                Camera.main.transform.localPosition = new Vector3(0, 1.2f, 0.15f);

        }
        else if (z == 0 && x == 0)
        {
            isMoving = false;
            Camera.main.transform.localPosition = new Vector3(0, 1.2f, 0.15f);
        }


        if (isGrounded && !isSliding)
            move = transform.right * x + transform.forward * z;
        else if (!isGrounded && Input.GetKey(KeyCode.LeftShift) && !isSliding)
            move = (transform.right * x / 2) + (transform.forward * z * 2);

        if ((wallRight || wallLeft) && !isGrounded)
        {
            if (wallJumpCount > 0)
                canWallJump = true;
            else
                canWallJump = false;

            move = (transform.right * x * 0f) + (transform.forward * z * 3.5f);
            float cameraTilt = 0f;

            if (wallRight)
                cameraTilt = 8f;
            else if (wallLeft)
                cameraTilt = -8f;

            if (Input.GetKeyDown(KeyCode.Space) && canWallJump)
            {
                wallJumpCount--;
                gravityVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
                move = (transform.right * x * 6f) + (transform.forward * z * 3f);
            }

            Camera.main.transform.Rotate(new Vector3(0, 0, cameraTilt));
        }

        else if (!wallRight && !wallLeft)
            wallJumpCount = 1f;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = sprintMoveSpeed;
            isSprinting = true;
        }
        else
        {
            moveSpeed = walkMoveSpeed;
            isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && (!wallLeft || !wallRight))
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding && isSprinting)
        {
            controller.height -= slideAmount;
            controller.center = new Vector3(0, slideAmount / 2, 0);
            slideTimeLeft = slideTime;
            isSliding = true;
        }

        if (isSliding)
        {
            slideTimeLeft -= Time.deltaTime;
            if (slideTimeLeft <= 0)
            {
                controller.height += slideAmount;
                controller.center = new Vector3(0, 0, 0);
                slideTimeLeft = slideTime;
                isSliding = false;
            }
        }

        controller.Move(move * moveSpeed * Time.deltaTime);

    }

    void checkWalls()
    {
        wallLeft = Physics.CheckSphere(leftCheck.position, 0.5f, wallMask);
        wallRight = Physics.CheckSphere(rightCheck.position, 0.5f, wallMask);
    }

    void checkGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && fallDamageTriggered)
        {
            print(gravityVelocity.y);
            triggerDeath = true;
        }
    }

    void ApplyGravity()
    {
        if ((wallRight || wallLeft) && !isGrounded)
            gravity = -30f;
        else if (isSliding)
            gravity = -1000f;
        else
            gravity = -65f;

        gravityVelocity.y += gravity * Time.deltaTime;

        if (gravityVelocity.y <= fallDamageThreshold)
            fallDamageTriggered = true;

        controller.Move(gravityVelocity * Time.deltaTime);
    }
}
