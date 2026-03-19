using UnityEngine;

// Requires component to exist (in this case without SpriteRenderer code would not work
[RequireComponent (typeof(SpriteRenderer))]

public class SpriteRandomiserBehaviour : MonoBehaviour
{
    // SerializeField tells Unity that you want this to be editable in the editor
    // Allows it to show up without making it public
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    // When spriteRandomiserBehaviour is instantiated and attached to the gameObject, this is the first thing called
    // Gets a reference to the spriteRenderer
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
