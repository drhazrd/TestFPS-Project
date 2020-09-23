using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Vector3 moveInput; 
    Vector3 verticalMovement, horizontalMovement;
    public GameObject walkSFX, sprintSFX;
    public Transform camTrans;

    //Player Weapons/Guns
    public GunController activeGun, secondaryGun;
    public List<GunController> allGuns = new List<GunController>();
    public List<GunController> allUnlockableGuns = new List<GunController>();
    public int currentGun, swapGun;

    //Player info
    public Text playerInfo;
    public int pID;
    //Player Ability List
    //Player Ability Cooldown

    public Transform adsPoint, gunHolder;
    private Vector3 gunStartPOS;
    public float adsSpeed;

    //Player Stats
    public float moveSpeed, runSpeed, gravityModifier, jumpPower;
    private float bounceAmt;
    private bool isBounced;

    //Player Event Info
    public Quest quest;
    public Text missionText;
    public bool isQuesting;
    public Text scoreText;
    public Text grenadesText;
    public int questCurrentAmount;
    public int questRequiredAmount;

    //Gameplay Options Control
    public float mouseInputSensetivity, joyInputSensetivity = 1f;
    public bool invertX, invertY, useJoyStick;
    public bool isRunning, isHit, haloGunSwap;
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
        gunStartPOS = gunHolder.localPosition;
        walkSFX.SetActive(false);
        sprintSFX.SetActive(false);
    }
    void Update()
    {
        //Player Info Update
        playerInfo.text = "Enemies Left: " + GameManager.instance.currentEnemies.Count.ToString();

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
        }
       
        UIController.instance.sprintUI.SetActive(isRunning);
        //UIController.instance.hitUI.gameObject.SetActive(isHit);
        moveInput = horizontalMovement + verticalMovement;
        moveInput.Normalize();
        if (moveInput.x == 0f || moveInput.z == 0f)
        {
            walkSFX.gameObject.SetActive(false);
        }
        else
        {
            if (charCon.isGrounded)
                walkSFX.gameObject.SetActive(true);
            Debug.Log("Walking....");
        }
        if (Input.GetKey(KeyCode.LeftShift ) || Input.GetButton("Sprint"+pID))
        {
            moveInput = moveInput * runSpeed;
            isRunning = true;
            walkSFX.gameObject.SetActive(false);
            if (charCon.isGrounded)
                sprintSFX.gameObject.SetActive(true);
            Debug.Log("Running....");
            //sprintUI.SetActive(isRunning);
        }
        else
        {
            moveInput = moveInput * moveSpeed;
            isRunning = false;
            sprintSFX.gameObject.SetActive(false);
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
        //Bounce
        //Jumping
        if (Input.GetButtonDown("Jump"+pID) && canJump)
        {
            moveInput.y = jumpPower;
            canDoubleJump = true;
            AudioManager.instance.PlaySFX(8);
            walkSFX.gameObject.SetActive(false);
            sprintSFX.gameObject.SetActive(false);

        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"+pID))
        {
            moveInput.y = jumpPower;
            canDoubleJump = false;
            AudioManager.instance.PlaySFX(8);
            walkSFX.gameObject.SetActive(false);
            sprintSFX.gameObject.SetActive(false);
        }

        if (isBounced)
        {
            isBounced = false;
            moveInput.y = bounceAmt;
            canDoubleJump = true;
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
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPOS, adsSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("GunSwitch1"))
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
    public void AddGun(string gunToAdd)
    {
        Debug.Log("Picked up the " + gunToAdd);
        bool gunUnlocked = false;
        if (allUnlockableGuns.Count > 0)
        {
            for (int i = 0; i < allUnlockableGuns.Count; i++)
            {
                if(allUnlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;
                    allGuns.Add(allUnlockableGuns[i]);
                    allUnlockableGuns.RemoveAt(i);
                    i = allUnlockableGuns.Count;
                }
            }
        }
        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            GunSwitch();
        }
    }
    public void InversionToggleX()
    {
        invertX = !invertX;
    }
    public void InversionToggleY()
    {
        invertY = !invertY;
    }
    public void ControllerToggle()
    {
        useJoyStick = !useJoyStick;
    }
    public void Bounce(float bounceForce)
    {
        bounceAmt = bounceForce;
        isBounced = true;
    }
}
