using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public CharacterController charCon;
    public GameManager gM;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;
    public Animator anim;

    Vector2 mouseInput;
    Vector3 moveInput, verticalMovement, horizontalMovement;

    public Transform camTrans;

    //Player Stats
    public GunController activeGun, secondaryGun;
    public List<GunController> allGuns = new List<GunController>();
    public int currentGun, swapGun;

    public Transform adsPoint, gunHolder;
    public Vector3 gunStartPOS;
    public float adsSpeed;

    public GameObject sprintUI;
    public float moveSpeed, runSpeed, gravityModifier, jumpPower;

    public int pID;

    public float mouseInputSensetivity, joyInputSensetivity = 1f;
    public bool invertX, invertY, useJoyStick, isRunning, haloGunSwap;
    private bool canJump, canDoubleJump, canMove;

    void Awake()
    {
        instance = this;
        canMove = true;
    }
    void Start() {
        gM = FindObjectOfType<GameManager>();
        activeGun = allGuns[currentGun];
        secondaryGun = allGuns[swapGun];
        activeGun.gameObject.SetActive(true);
    }
    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float yStore = moveInput.y;
        if (!useJoyStick) {
            verticalMovement = transform.forward * (Input.GetAxis("Vertical"));
            horizontalMovement = transform.right * (Input.GetAxis("Horizontal"));
        }

        else if (useJoyStick) {
            verticalMovement = transform.forward * (Input.GetAxis("Joy"+ pID +"V"));
            horizontalMovement = transform.right * (Input.GetAxis("Joy"+ pID +"H"));
            Debug.Log(verticalMovement);
            Debug.Log(horizontalMovement);
        }
       

        sprintUI.SetActive(isRunning);
        moveInput = horizontalMovement + verticalMovement;
        moveInput.Normalize();
        if (Input.GetKey(KeyCode.LeftShift ) || Input.GetButton("Sprint"))
        {
            moveInput = moveInput * runSpeed;
            isRunning = true;
            //sprintUI.SetActive(isRunning);
        }
        else
        {
            moveInput = moveInput * moveSpeed;
            isRunning = false;
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
        if (gM.playerFreeze)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        //Jumping
        if (Input.GetButtonDown("Jump") && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
        }

        charCon.Move(moveInput * Time.deltaTime);

        //Camera
        if (canMove)
        {
            if (!useJoyStick)
            {
                mouseInput = new Vector2(Input.GetAxisRaw("MouseX"), Input.GetAxisRaw("MouseY")) * mouseInputSensetivity;
            }
            else if (useJoyStick)
            {
                mouseInput = new Vector2(Input.GetAxisRaw("Joy" + pID + "X"), Input.GetAxisRaw("Joy" + pID + "Y")) * joyInputSensetivity;
            }
        }
        else if(!canMove)
        {
            mouseInput = new Vector2(0,0);
        }
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
        if (Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
                //GunSwap();
                GunSwitch();
            if (haloGunSwap)
            {
            }
            else if(!haloGunSwap)
            {
            }
        }
    }
    public void GunSwitch()
    {
        activeGun.gameObject.SetActive(false);

        currentGun++;
        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        if (activeGun.isHeld && !activeGun.isActive)
        {
            activeGun.gameObject.SetActive(true);
        }
        else
        {
            currentGun++;
        }
    }
    public void GunSwap()
    {
        if (!activeGun)
        {
            activeGun.gameObject.SetActive(false);
            secondaryGun.gameObject.SetActive(true);
        }
        else
        {
            activeGun.gameObject.SetActive(true);
            secondaryGun.gameObject.SetActive(false);
        }
    }
}
