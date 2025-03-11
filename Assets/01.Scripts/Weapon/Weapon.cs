using BGD.Agents;
using BGD.Animators;
using BGD.Cores;
using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public bool CanShoot => _currentBulletCnt > 0;

        [HideInInspector] public WeaponAnimationTrigger animTrigger;

        [SerializeField] private Transform _firePos;
        [SerializeField] private AnimParamSO _shootParam;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private WeaponDataSo _weaponDataSO;

        private WeaponRenderer _renderer;
        private AgentWeapon _weapon;
        private bool isEndTrigger;
        private float _listShootingTime;
        private int _currentBulletCnt;
        private float _shootDealyTime;

        private void Awake()
        {
            _weapon = transform.parent.GetComponent<AgentWeapon>();
            animTrigger = GetComponentInChildren<WeaponAnimationTrigger>();
            _renderer = GetComponentInChildren<WeaponRenderer>();
            _shootDealyTime = _weaponDataSO.dealyTime;
            Reload();
        }

        public void Reload()
        {
            _currentBulletCnt = _weaponDataSO.bulletCnt;
        }

        private void Start()
        {
            animTrigger.OnAnimationEndTrigger += HandleAttackEndTrigger;
            animTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void OnDestroy()
        {
            animTrigger.OnAnimationEndTrigger -= HandleAttackEndTrigger;
            animTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void HandleAttackTrigger()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePos.position, Quaternion.identity);
            bullet.transform.right = _firePos.right;
        }

        private void HandleAttackEndTrigger()
        {
            isEndTrigger = true;
            _renderer.SetParam(_shootParam, false);
            _listShootingTime = Time.time;
            _currentBulletCnt--;
        }

        public void Shooting()
        {
            if (_listShootingTime + _shootDealyTime < Time.time)
            {
                isEndTrigger = false;
                _renderer.SetParam(_shootParam, true);
            }
        }

        private void Update()
        {
        }
    }
}
