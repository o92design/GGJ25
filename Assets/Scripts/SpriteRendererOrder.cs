using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Adjust the sorting order based on the y position
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}