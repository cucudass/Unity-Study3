using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashMaterial : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    
    private SpriteRenderer SpriteRenderer;
    private Material originMaterial;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        originMaterial = SpriteRenderer.material;
    }

    public IEnumerator HitEffect(float duration) {
        SpriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);
        SpriteRenderer.material = originMaterial;
    }
}
