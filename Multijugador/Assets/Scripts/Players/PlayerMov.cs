using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    private PhotonView view;
    [SerializeField] private float speed;
    [SerializeField] private GameObject hitbox;

    [Header("Salto")]
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask isFloor;
    private bool onFloor;
    private bool jump;

    [Header("Movimiento")]
    private bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        onFloor = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, isFloor);
        if (onFloor)
        {
            jump = true;
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (canMove)
            {
                if (Input.GetButton("Jump") && jump)
                {
                    Jump();
                }

                float InputX = Input.GetAxis("Horizontal");
                player.velocity = new Vector2(InputX * speed, player.velocity.y);
                if(InputX > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                } else if(InputX < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                if(player.velocity.y < 0)
                {
                    fall();
                }

                if (Input.GetButton("Fire1") && canMove)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private void Jump()
    {
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetBool("Jumping", true);
        onFloor = false;
        jump = false;
    }

    private void fall()
    {
        if(player.velocity.y < 0)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
    }

    public void ActivateHitbox()
    {
        Debug.Log("Se ha activado la hitbox");
        hitbox.SetActive(true);
    }

    public void DesactivateHitbox()
    {
        hitbox.SetActive(false);
    }

    private IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        canMove = false;
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }
}
