using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float lives = 3.0f;

    private bool hasPlayedDead = false;
    public bool isDead = false;

    public float sfxVolume = 0.7f;
    public AudioClip jumpSFX;
    public AudioClip deathSFX;
    private  AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        controller.minMoveDistance = 0;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0) {
            animator.SetBool("isDead", true);
            isDead = true;
            if (!hasPlayedDead) {
                audio.PlayOneShot(deathSFX, 2.0f);
                hasPlayedDead = true;
            }
        } else {
            groundedPlayer = controller.isGrounded;
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));      // Gets vector of movement from keyboard input
            moveDirection = transform.TransformDirection(moveDirection);                                         // Converts user input to something we can use
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);                                       // Move the controller
            
            // Gravity continously decreases player velocity, so much reset to 0 anytime it goes negative
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isMoving", false);
                animator.SetBool("isSplodge", true);
            }
            else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isMoving", true);
                animator.SetBool("isSplodge", false);
            }
            else
            {
                animator.SetBool("isIdle", true);
                animator.SetBool("isMoving", false);
                animator.SetBool("isSplodge", false);
            }
            
            // Changes height poistion of bean based on gravity and jump height
            if (Input.GetAxis("Jump") > 0 && groundedPlayer)
            {
                audio.PlayOneShot(jumpSFX, sfxVolume);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);      // If jump, increase velocity in the y direction
            }

            playerVelocity.y += gravityValue * Time.deltaTime;          // Calculate gravity's current impact on bean
            controller.Move(playerVelocity * Time.deltaTime);           // Move bean
        }
    }
}
