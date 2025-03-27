using UnityEngine;

namespace BGD.Environments
{
    public abstract class BaseTrap : MonoBehaviour
    {
        [Header("BaseSetting")]
        public bool isOneTimeUse;
        public float _reSpawnTime;
        public abstract void ActivateTrap();
    }
}
