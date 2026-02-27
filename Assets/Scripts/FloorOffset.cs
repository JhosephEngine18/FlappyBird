using System;
using UnityEngine;

public class FloorOffset : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] private float offsetSpeed;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _spriteRenderer.size = new Vector2((_spriteRenderer.size.x) + Time.deltaTime * offsetSpeed,  _spriteRenderer.size.y);
    }
}
