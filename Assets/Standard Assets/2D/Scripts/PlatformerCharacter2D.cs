using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    
    {
        public Animator animator;
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
        [SerializeField] private float airSpeed = 0.25f;
        [SerializeField] float initialJump = 2f;
        public Rigidbody2D rigidBody;
        float speedLimiter = 0.01f;
        public float jumpTime;
        private float jumpTimer;
        private float health;

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        //bool haveJumped = false;
        //float isJumpingTimer;
        //public float jumpAnimStaller = 0.5f;


        private GameObject jumpObject;
        //private AudioSource jumpObjectSound;

        private void Start()
        {
            //jumpObject = GameObject.Find("jumpSound");
            //jumpObjectSound = jumpObject.GetComponent<AudioSource>();
        }

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            //isJumpingTimer = jumpAnimStaller;
        }

        private void Update()
        {
            //if (Input.GetButtonDown("Jump"))
            //{
            //    haveJumped = true;
            //}
            
            //if (haveJumped)
            //{
            //    animator.SetTrigger("IsJumping");
            //    animator.SetBool("DuringJump", true);
                
            //    isJumpingTimer -= Time.deltaTime;

            //    if (isJumpingTimer <= 0)
            //    {
            //        animator.SetBool("DuringJump", false);
            //        haveJumped = false;
            //        isJumpingTimer = jumpAnimStaller;
            //    }
            //}
            
        }

        private void FixedUpdate()
        {
            m_Grounded = false;

            if (!m_Grounded)
            {
                animator.SetBool("IsFalling", true);
            }
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
                animator.SetBool("IsFalling", false);
            }
            //m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            //Debug.Log("X velocity: " + rigidBody.velocity.x + ", Y velocity: " + rigidBody.velocity.y);
            //Debug.Log("Y velocity: " + rigidBody.velocity.y);
            //Debug.Log("Z velocity: " + rigidBody.velocity.x);

            if (!m_Grounded && m_MaxSpeed > 6f && rigidBody.velocity.y < 0)
            {
                //airSpeed -= Time.deltaTime * 2;
                m_MaxSpeed -= speedLimiter;
                speedLimiter += 0.05f;
            }

            if (m_Grounded)
            {
                //airSpeed = 1;
                m_MaxSpeed = 10;
                speedLimiter = 0.01f;
            }
            AnimationControl();


        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch) // && m_Anim.GetBool("Crouch")
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                animator.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed*airSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }

            // If the player should jump...
            if (m_Grounded && jump) //&& animator.SetTrigger("IsJumping");
            {
                jumpTimer = jumpTime;
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * initialJump)); // , ForceMode2D.Impulse

                animator.SetTrigger("IsJumping");
                //if (!jumpObjectSound.isPlaying)
                //{
                //    jumpObjectSound.Play();
                //}
                //else if (jumpObjectSound.isPlaying)
                //{
                //    {
                //        /*GameObject clone;
                //        AudioSource clonedSound;
                //        clone = Instantiate(jumpObject);
                //        clonedSound = clone.GetComponent<AudioSource>();
                //        clonedSound.Play();
                //        if (clonedSound.isPlaying)
                //        {
                //            GameObject clone1;
                //            AudioSource clonedSound1;
                //            clone1 = Instantiate(jumpObject);
                //            clonedSound1 = clone.GetComponent<AudioSource>();
                //            clonedSound1.Play();
                //        } */
                //    }
                //}
            }

            if (jump && jumpTimer > 0)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                jumpTimer -= Time.deltaTime;
            }

            
        }



        // Player Landing event
        /*public void OnLanding()
        {
            if (m_Grounded)
            {
                animator.SetTrigger("IsJumping");
            }
            Debug.Log("landed");
        }*/

        //PlayerGPMechanics.overheatActive

        

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void AnimationControl()
        {
            if (!Input.GetButton("Fire3"))
            {
                animator.SetLayerWeight(1, 0);
            }
            else
            {
                animator.SetLayerWeight(1, 1);
            }
        }

    }
   
}
