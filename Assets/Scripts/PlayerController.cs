﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, runSpeed, gravityModifier, jumpPower;
    public CharacterController charCon;

    Vector3 moveInput;

    public Transform camTrans;

    public float mouseInputSensetivity;
    public bool invertX;
    public bool invertY;

    private bool canJump, canDoubleJump;
    public bool isRunning;
    public GameObject runNotification;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;
    public Animator anim;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        float yStore = moveInput.y;

        Vector3 verticalMovement = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalMovement + verticalMovement;
        moveInput.Normalize();
        if (Input.GetKey(KeyCode.LeftShift )|| Input.GetKey(KeyCode.LeftAlt) || Input.GetButtonDown("Sprint"))
        {
            moveInput = moveInput * runSpeed;
            isRunning = true;
            runNotification.SetActive(isRunning);
        }
        else
        {
            moveInput = moveInput * moveSpeed;
            isRunning = false;
            runNotification.SetActive(isRunning);
        }
        moveInput.y = yStore;

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (charCon.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckpoint.position, .25f, whatIsGround).Length > 0;

        if (canJump)
        {
            canDoubleJump = false;
        }


        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        }
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        charCon.Move(moveInput * Time.deltaTime);

        //Camera
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("MouseX"), Input.GetAxisRaw("MouseY")) * mouseInputSensetivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        //Camera Rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));



        //Headbob
        //anim.SetFloat("moveSpeed", moveInput.magnitude);
        //anim.SetBool("onGround", canJump);
    }

}
