using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Bricks.SO
{
    [CreateAssetMenu(menuName = "Bricks/BrickTile", fileName = "BrickTile")]
    public class BrickTile : Tile
    {
        [SerializeField] private Sprite _destroyedSprite;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private Color _hitColor;
        [SerializeField] private GameObject _prefab;

        public Sprite DestroyedSprite => _destroyedSprite;
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public Color HitColor => _hitColor;
        public GameObject Prefab => _prefab;
    }
}