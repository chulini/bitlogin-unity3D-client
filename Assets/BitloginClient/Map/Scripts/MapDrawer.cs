using System.Collections.Generic;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace BitloginClient.Floor
{
    /// <summary>
    /// In charge of map instantiation
    /// </summary>
    public class MapDrawer : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] Vector2IntVariable _mapSize;
        [SerializeField] GameObject _floorPrefab;
#pragma warning restore 0649
        
        public Dictionary<Vector2Int, GameObject> GetInstantiatedMap()
        {
            Dictionary<Vector2Int, GameObject> mapBlocks = new Dictionary<Vector2Int, GameObject>();
            for (int x = 0; x < _mapSize.Value.x; x++)
            {
                for (int y = 0; y < _mapSize.Value.y; y++)
                {
                    Vector2Int coord = new Vector2Int(x, y);
                    GameObject blockInstance = GetBlockInstanceAt(coord);
                    if(blockInstance != null)
                    {
                        mapBlocks.Add(coord, blockInstance);
                    }
                }
            }

            return mapBlocks;
        }

        GameObject GetBlockInstanceAt(Vector2Int coord)
        {
            return Instantiate(_floorPrefab, new Vector3(coord.x, -1.0f, coord.y), Quaternion.identity);
        }
    }
}