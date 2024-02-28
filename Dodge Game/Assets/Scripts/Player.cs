using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Vector2 direction;
    [SerializeField] float speed = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    void Move() {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        spriteRenderer.flipX = direction.x > 0;

        if(direction.x != 0 || direction.y != 0)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);

        Vector2 movement = new Vector2(direction.x, direction.y);

        transform.Translate(movement * speed * Time.deltaTime);
    }
}
