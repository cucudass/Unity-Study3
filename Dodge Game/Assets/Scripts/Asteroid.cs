using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    Transform targetTransform;
    Vector2 direction;

    private void Awake() {
        targetTransform = GameObject.Find("Player").transform;
    }
    void Start()
    {
        direction = targetTransform.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        Move();
    }

    void Move() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("DZ")) {
            Destroy(gameObject);
        }
    }
}
