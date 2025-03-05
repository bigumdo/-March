using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Spawns
{
    [CreateAssetMenu(fileName = "SpawnDataSO", menuName = "Scriptable Objects/SpawnDataSO")]
    public class EnemySpawnDataSO : ScriptableObject
    {
        public EnemySpawnType spawnType;
        public List<GameObject> list = new List<GameObject>();
    }
}
