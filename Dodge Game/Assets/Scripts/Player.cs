using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private FlashMaterial flashMaterial;

    [SerializeField] Vector2 direction;
    [SerializeField] float speed = 500.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        flashMaterial = GetComponent<FlashMaterial>();
    }

    void Update()
    {
        KeyBoard();
    }

    private void FixedUpdate() {
        Move();
        Reverse();
    }

    void KeyBoard() {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
    }

    void Move() {
        if(rigidbody2D.velocity == Vector2.zero) {
            animator.SetBool("Run", false);
        } else {
            animator.SetBool("Run", true);
        }
        rigidbody2D.velocity = new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime;
    }

    void Reverse() {
        spriteRenderer.flipX = direction.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            StartCoroutine(flashMaterial.HitEffect(0.25f));
        }
    }
}
