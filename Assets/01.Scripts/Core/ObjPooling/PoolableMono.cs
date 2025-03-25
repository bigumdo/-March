using UnityEngine;

namespace BGD.ObjPooling
{
    public abstract class PoolableMono : MonoBehaviour
    {
        public string type;
        public abstract void ResetItem();
    }
}
