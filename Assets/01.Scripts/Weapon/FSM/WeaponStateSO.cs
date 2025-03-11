using BGD.Animators;
using UnityEngine;

namespace BGD.Weapons
{
    [CreateAssetMenu(fileName = "WeaponStateSO", menuName = "SO/Weapon/FSM/StateSO")]
    public class WeaponStateSO : ScriptableObject
    {
        public WeaonStateEnum stateName;
        public string className;
        public AnimParamSO animParam;
    }
}
