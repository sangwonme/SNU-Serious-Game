using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody theRB;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public int jumpPower;
    public float moveSpeed;
    private Vector2 moveInput;
    private BoxCollider col;
    private bool IsJumping;


    // Start is called before the first frame update
    void Start()
    {
        IsJumping = false;
    }

    void Jump ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!IsJumping)
            {
                IsJumping = true;
                theRB.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        // move
        theRB.velocity = new Vector3(moveInput.x * moveSpeed, theRB.velocity.y, moveInput.y * moveSpeed);

        // jump
        Jump();

        // sprite direction
        if(moveInput.x > 0){
            spriteRenderer.flipX = true;
        }
        else if(moveInput.x < 0){
            spriteRenderer.flipX = false;
        }

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            animator.SetBool("isWalking", true);
        }
        else{
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            IsJumping = false;
        }
    }
}
