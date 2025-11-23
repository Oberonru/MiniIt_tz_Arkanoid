using Core.Bricks;
using Core.Bricks.SO;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Core.Game
{
    public class LevelLoader : MonoBehaviour
    {
        [Inject] private DiContainer _di;
        [SerializeField] private Tilemap _levelTilemap;

        private void Start()
        {
            SpawnBricksFromTilemap();
        }

        private void SpawnBricksFromTilemap()
        {
            foreach (var pos in _levelTilemap.cellBounds.allPositionsWithin)
            {
                if (!_levelTilemap.HasTile(pos)) continue;

                TileBase tile = _levelTilemap.GetTile(pos);

                if (tile is BrickTile brickTile)
                {
                    var worldPos = _levelTilemap.CellToWorld(pos) + _levelTilemap.tileAnchor;

                    var brickObj = _di.InstantiatePrefab(brickTile.Prefab, worldPos, Quaternion.identity, transform);

                    var brick = brickObj.GetComponent<Brick>();
                    brick.Init(brickTile.MaxHealth, brickTile.DestroyedSprite);
                }
            }

            _levelTilemap.ClearAllTiles(); 
        }
    }
}