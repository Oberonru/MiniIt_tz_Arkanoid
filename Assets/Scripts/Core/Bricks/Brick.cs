using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Sprite _damagedSprite;

    private SpriteRenderer _spriteRenderer;
    private int _health;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int health, Sprite damagedSprite)
    {
        _health = health;
        _damagedSprite = damagedSprite;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;

        if (_health == 1)
        {
            if (_damagedSprite != null)
            {
                _spriteRenderer.sprite = _damagedSprite;
            }
               
        }
        else if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}