using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public enum SIDE { Left, Mid, Right }
public class Player : MonoBehaviour
{
    //OldCode
    public AudioSource crashThud;
    public AudioSource coinFX;
    public GameObject charModel, redpot_button, greenpot_button;
    public HealthBar healthbar;
    float damage = 50;

    //orginal code
    public SIDE mSide = SIDE.Mid;
    float newXPos = 0f;
    [HideInInspector]
    public bool swipeLeft, swipeRight, swipeUp, swipeDown, inJump, inRoll;
    private static CharacterController controller;
    private Animator animator;

    private float x;
    public float speedLeftRight;
    private float jumpPower = 15f;
    private float y;
    public float xValue;
    public static float playerSpeed = 7f;
    private float currentHight;
    private float currentCenterY;
    static public bool canMove = false;
    Vector3 moveVector;


    void Start()
    {
        
        Application.targetFrameRate = 60;
        controller = GetComponent<CharacterController>();
        currentHight = controller.height;
        currentCenterY = controller.center.y;
        transform.position = Vector3.zero;
        animator = GetComponent<Animator>();
        AudioManager.audioInstence.PlayMusic("Background");
        if (InventoryManager.instance.items.Count > 0)
        {
            redpot_button.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

        swipeLeft = DenemeController.swipeLeft;
        swipeRight = DenemeController.swipeRight;
        swipeUp = DenemeController.swipeUp;
        swipeDown = DenemeController.swipeDown;

        if (canMove == true)
        {
            if (swipeLeft)
            {
                if (mSide == SIDE.Mid)
                {
                    newXPos = -xValue;
                    mSide = SIDE.Left;
                    animator.Play("Jog Strafe Left");
                }
                else if (mSide == SIDE.Right)
                {
                    newXPos = 0;
                    mSide = SIDE.Mid;
                    animator.Play("Jog Strafe Left");
                }
            }
            else if (swipeRight)
            {
                if (mSide == SIDE.Mid)
                {
                    newXPos = xValue;
                    mSide = SIDE.Right;
                    animator.Play("Jog Strafe Right");

                }
                else if (mSide == SIDE.Left)
                {
                    newXPos = 0;
                    mSide = SIDE.Mid;
                    animator.Play("Jog Strafe Right");
                }
            }
            Roll();

        }
    
        controller.Move(moveVector);
        moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, playerSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, newXPos, Time.deltaTime * speedLeftRight);
        Jump();


    }
    public void Jump()
    {

        if (controller.isGrounded)
        {
            if (canMove == true)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
                {
                    animator.Play("JumpingDown");
                    inJump = false;
                }
                if (swipeUp)
                {
                    y = jumpPower;
                    animator.CrossFadeInFixedTime("Jumping", 0.1f);
                    inJump = true;
                }

            }

        }
        else
        {
            y -= jumpPower * 1.6f * Time.deltaTime;

        }
    }
    internal float rollCounter;

    public void Roll()
    {
        rollCounter -= Time.deltaTime;
        if (rollCounter <= 0f)
        {
            rollCounter = 0f;
            controller.center = new Vector3(0, currentCenterY, 0);
            controller.height = currentHight;
            inRoll = false;
        }
        if (swipeDown)
        {

            rollCounter = 0.95f;
            y -= 10f;
            controller.center = new Vector3(0, currentCenterY / 2f, 0);
            controller.height = currentHight / 2f;
            animator.Play("RunningSlide");
            inRoll = true;
            inJump = false;
        }


    }
    private void OnTriggerEnter(Collider other)
    {

        // Dead
        if (other.transform.tag == "Obstacle")
        {
            ObstacleCollision.isDead = true;
            AudioManager.audioInstence.PlaySfxMusic("Crash");



        }
        // Collect Coin
        if (other.transform.tag == "CollectCoin")
        {
            AudioManager.audioInstence.PlaySfxMusic("Coin");


        }
        if (other.transform.tag == "Red Potion")
        {
            InventoryManager.instance.items.Add(other.gameObject);
            other.gameObject.SetActive(false);
            if (redpot_button.activeSelf == false)
            {
                redpot_button.SetActive(true);
            }
            redpot_button.transform.GetChild(4).GetComponent<ButtonPotion>().CountIncrease();
        }
        if (other.transform.tag == "Green Potion")
        {
            InventoryManager.instance.items.Add(other.gameObject);
            other.gameObject.SetActive(false);
            if (greenpot_button.activeSelf == false)
            {
                greenpot_button.SetActive(true);
            }
            greenpot_button.transform.GetChild(4).GetComponent<ButtonPotion>().CountIncrease();
        }
        if (other.transform.tag == "Traps")
        {
            healthbar.TakeDamage(damage);
            if (HealthBar.hitPoint != 0)
            {
                animator.Play("InjuredRun");
            }

        }

    }

}
