using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

namespace BGD.Spawns
{
    public enum EnemySpawnType
    {
        melee,
        ranged
    }


    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawnDataSO> spawnType = new List<EnemySpawnDataSO>();
        private Dictionary<EnemySpawnType, EnemySpawnDataSO> spawnDictionary;

        private void Awake()
        {
            spawnDictionary = new Dictionary<EnemySpawnType, EnemySpawnDataSO>();
            spawnType.ForEach(x => spawnDictionary.Add(x.spawnType, x));
        }
    }
}
