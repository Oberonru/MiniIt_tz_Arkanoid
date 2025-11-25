using System;
using Core.Bricks;
using Core.Bricks.SO;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Game
{
    public class LevelLoader : MonoBehaviour
    {
        [Inject] private DiContainer _di;
        [SerializeField] private GameManager _gameManager;

        private const string PhoneLevelKey = "Level_phone";
        private const string IpadLevelKey = "Level_ipad";

        private Tilemap _currentTilemap;

        private async void Awake()
        {
            try
            {
                var key = SelectTilemapByDevice();

                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var levelPrefab = handle.Result;
                    var levelInstance =
                        _di.InstantiatePrefab(levelPrefab, Vector3.zero, Quaternion.identity, transform);

                    _currentTilemap = levelInstance.GetComponentInChildren<Tilemap>();
                    SpawnBricks();
                }
                else
                {
                    Debug.LogError($"Failed to load level with key: {key}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"LevelLoader exception: {e}");
            }
        }

        private string SelectTilemapByDevice()
        {
            // iPad simulation
            // var dpi = 160f; 
            // var width = 2048;
            // var height = 1536;

            var dpi = Screen.dpi;
            if (dpi == 0.0f)
            {
                Debug.LogWarning("DPI is unavailable, fallback to iPad level");
                return IpadLevelKey;
            }

            var width = Screen.width;
            var height = Screen.height;
            var diagonal = Mathf.Sqrt(width * width + height * height);
            var diagonalInches = diagonal / dpi;

            return diagonalInches >= 7.0f ? IpadLevelKey : PhoneLevelKey;
        }

        private void SpawnBricks()
        {
            foreach (var pos in _currentTilemap.cellBounds.allPositionsWithin)
            {
                if (!_currentTilemap.HasTile(pos)) continue;

                TileBase tile = _currentTilemap.GetTile(pos);

                if (tile is BrickTile brickTile)
                {
                    var worldPos = _currentTilemap.CellToWorld(pos) + _currentTilemap.tileAnchor;

                    var brickObj = _di.InstantiatePrefab(brickTile.Prefab, worldPos, Quaternion.identity, transform);

                    var brick = brickObj.GetComponent<Brick>();
                    brick.Init(brickTile.MaxHealth, brickTile.DestroyedSprite, brickTile.Reward, brickTile.BrickType);
                    _gameManager.RegisterBrick(brick);
                }
            }

            _currentTilemap.ClearAllTiles();
        }
    }
}