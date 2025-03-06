using UnityEngine;

namespace BGD.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private WeaponAnimationTrigger _animTrigger;


        private void Awake()
        {
            _animTrigger = GetComponentInChildren<WeaponAnimationTrigger>();
        }
    }
}
