using System.Collections.Generic;
using UnityEngine;

namespace BitloginClient.Floor
{
    /// <summary>
    /// In charge of update the floor
    /// </summary>
    [RequireComponent(typeof(MapDrawer))]
    public class Map : MonoBehaviour
    {
        MapDrawer _mapDrawer;
        Dictionary<Vector2Int, GameObject> _mapBlocks;

        void Awake()
        {
            Initialize();
            InstantiateMap();
        }

        void Initialize()
        {
            _mapDrawer = GetComponent<MapDrawer>();
        }

        void InstantiateMap()
        {
            
            _mapBlocks = _mapDrawer.GetInstantiatedMap();
        }
    }

}