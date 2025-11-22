using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.Bricks.SO
{
    [CreateAssetMenu(menuName = "Bricks/BrickTile", fileName = "BrickTile")]
    public class BrickTile : Tile
    {
        [SerializeField] private Sprite _destroyedSprite;
        [SerializeField] private int _health;
        [SerializeField] private Color _hitColor;
        [SerializeField] private GameObject _prefab;

        public Sprite DestroyedSprite => _destroyedSprite;
        public int Health => _health;
        public Color HitColor => _hitColor;
        public GameObject Prefab => _prefab;
    }
}