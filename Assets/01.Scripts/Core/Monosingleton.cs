using UnityEngine;

namespace BGD.Cores
{
    public class Monosingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;
        private static bool IsDestroyed = false;

        public static T Instance
        {
            get
            {
                if (IsDestroyed)
                {
                    _instance = null;
                }
                if (_instance == null)
                {
                    _instance = GameObject.FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError($"{typeof(T).Name} singletone is not exist");
                    }
                    else
                    {
                        IsDestroyed = false;
                    }
                }
                return _instance;
            }
        }

        private void OnDestroy()
        {
            IsDestroyed = true;
        }
    }
}
