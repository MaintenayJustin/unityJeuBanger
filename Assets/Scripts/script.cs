using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class script : MonoBehaviour
{

    public Animator Animator;
    public SpriteRenderer spriteRenderer;
    public float JumpForce = 120;
    public float Speed = 15f;
    private Rigidbody2D Rigidbody;
    public Transform RaycastOrigin;

    public LayerMask GroundMask;
    public Transform RaycastOriginLeft;
    public Transform RaycastOriginLeftDown;
    public Transform RaycastOriginLeftUp;

    public Transform RaycastOriginRight;
    public Transform RaycastOriginRightDown;
    public Transform RaycastOriginRightUp;

    public Transform RaycastOriginDown;
    public Transform RaycastOriginDownLeft;
    public Transform RaycastOriginDownRight;
    private bool _isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(RaycastOriginDown.position, Vector2.down, 0.1f, GroundMask);
        RaycastHit2D hitL = Physics2D.Raycast(RaycastOriginDownLeft.position, Vector2.down, 0.1f, GroundMask);
        RaycastHit2D hitR = Physics2D.Raycast(RaycastOriginDownRight.position, Vector2.down, 0.1f, GroundMask);
        _isGrounded = hit.collider != null || hitL.collider != null || hitR.collider != null;

        // Si le joueur appuie sur la flèche droite
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RaycastHit2D hit1 = Physics2D.Raycast(RaycastOriginRight.position, Vector2.right, 0.1f, GroundMask);
            RaycastHit2D hit2 = Physics2D.Raycast(RaycastOriginRightDown.position, Vector2.right, 0.1f, GroundMask);
            RaycastHit2D hit3 = Physics2D.Raycast(RaycastOriginRightUp.position, Vector2.right, 0.1f, GroundMask);
            if (hit1.collider == null && hit2.collider == null && hit3.collider == null)
            {
                spriteRenderer.flipX = false;
                Rigidbody.velocity = new Vector2(Speed, Rigidbody.velocity.y);
            }

        }

        // Si le joueur appuie sur la flèche gauche
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RaycastHit2D hit1 = Physics2D.Raycast(RaycastOriginLeft.position, Vector2.left, 0.1f, GroundMask);
            RaycastHit2D hit2 = Physics2D.Raycast(RaycastOriginLeftDown.position, Vector2.left, 0.1f, GroundMask);
            RaycastHit2D hit3 = Physics2D.Raycast(RaycastOriginLeftUp.position, Vector2.left, 0.1f, GroundMask);
            if (hit1.collider == null && hit2.collider == null && hit3.collider == null)
            {
                spriteRenderer.flipX = true;
                Rigidbody.velocity = new Vector2(-Speed, Rigidbody.velocity.y);
            }

        }
        else
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);

        // Si le joueur appuie sur la touche saut
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Rigidbody.AddForce(Vector2.up * JumpForce);
            Animator.SetTrigger("Jump");
        }

        // On donne à l'animation les données de mouvement du joueur
        Animator.SetFloat("velocityX", Mathf.Abs(Rigidbody.velocity.x));
        Animator.SetFloat("velocityY", Rigidbody.velocity.y);
        Animator.SetBool("isGrounded", _isGrounded);
    }
}
