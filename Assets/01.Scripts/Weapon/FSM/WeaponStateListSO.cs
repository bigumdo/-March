using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Weapons
{
    public enum WeaonStateEnum
    {
        Idle,
        Shooting,
        Reloading,
        Empty
    }

    [CreateAssetMenu(fileName = "WeaponStateListSO", menuName = "SO/Weapon/FSM/StateListSO")]
    public class WeaponStateListSO : ScriptableObject
    {
        public List<WeaponStateSO> states = new List<WeaponStateSO>();
    }
}
