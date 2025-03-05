using UnityEngine;

namespace BGD
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/Combat/AttackData")]
    public class AttackDataSO : ScriptableObject
    {
        [Header("BaseSetting")]
        public string dataName;
        public Vector2 attackMove;
        public float damage;
        public bool isknockBack;
        public Vector2 knockBackForce;
        public bool isCameraShake;
        public float cameraShakeForce;
        public float cameraShakeDuration;

        //[Space]
        //[Header("CompoAttack")]
    }
}
