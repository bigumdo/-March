using BGD.Agents;
using BGD.Animators;
using BGD.Cores;
using NUnit.Framework;
using System;
using UnityEngine;

namespace BGD.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field:SerializeField] public WeaponDataSo WeaponDataSO { get; private set; }
        [HideInInspector] public WeaponAnimationTrigger animTrigger;
        [HideInInspector] public bool CanShooting
        {
            get
            {
                return _currentBulletCnt > 0;
            }
        }

        [SerializeField] private WeaponStateListSO _states;

        [SerializeField] private Transform _firePos;
        [SerializeField] private GameObject _bulletPrefab;


        private WeaponRenderer _renderer;
        private AgentWeapon _weapon;
        private bool isEndTrigger;
        private int _currentBulletCnt;
        private int _maxBulletCnt;


        private WeaponStateMachine _stateMachine;

        private void Awake()
        {
            _weapon = transform.parent.GetComponent<AgentWeapon>();
            animTrigger = GetComponentInChildren<WeaponAnimationTrigger>();
            _renderer = GetComponentInChildren<WeaponRenderer>();

            _stateMachine = new WeaponStateMachine(this, _states);
            _currentBulletCnt = _maxBulletCnt = WeaponDataSO.bulletCnt;
        }

        private void Start()
        {
            _stateMachine.Initialize(WeaponStateEnum.Idle);

        }

        public void Reload()
        {
            _currentBulletCnt = _maxBulletCnt;
        }

        public void BulletReduction()
        {
            _currentBulletCnt--;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void ChangeState(WeaponStateEnum changeState)
        {
            _stateMachine.ChangeState(changeState);
        }

        public void Fire()
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.position = _firePos.position;
            bullet.transform.right = _firePos.right;
        }
    }
}
