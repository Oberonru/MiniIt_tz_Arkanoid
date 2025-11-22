using UnityEngine;
using UnityEngine.Tilemaps;

public class Brick : MonoBehaviour
{
    [SerializeField] private int _health = 2;
    public int Health => _health;
    [SerializeField] private Sprite _destroyedSprite;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_tilemap == null) _tilemap = FindObjectOfType<Tilemap>();
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Vector3Int cell = _tilemap.WorldToCell(transform.position);
            _tilemap.SetTile(cell, null);

            Destroy(gameObject);
        }
    }
}