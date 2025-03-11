using UnityEngine;

namespace BGD.Weapons
{
    [CreateAssetMenu(fileName = "WeaponDataSo", menuName = "SO/Weapon/WeaponDataSo")]
    public class WeaponDataSo : ScriptableObject
    {
        public int bulletCnt;
        public float dealyTime;
    }
}
