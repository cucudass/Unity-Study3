using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] GameObject asteroid;
    [SerializeField] int circleSize = 10;
    [SerializeField] Transform playerTransform;

    void Start()
    {
        StartCoroutine(CreateAsteroid());
    }

    IEnumerator CreateAsteroid() {
        while (true) {
            GameObject obj = Instantiate(asteroid, (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * circleSize, Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }
}
