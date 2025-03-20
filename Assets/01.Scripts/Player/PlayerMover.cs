using BGD.Agents;
using BGD.StatSystem;
using System;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerMover : AgentMover
    {
        [Header("StatSetting")]
        public StatSO jumpCntStat;

        private float _currentJumpCnt;
        private float _maxJumpCnt;
        public bool CanJump => _currentJumpCnt > 0;

        public override void AfterInit()
        {
            base.AfterInit();
            jumpCntStat = _agentStat.GetStat(jumpCntStat);
            _maxJumpCnt = _currentJumpCnt = jumpCntStat.Value;
            jumpCntStat.OnValueChange += HandleJumpCntChange;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            jumpCntStat.OnValueChange -= HandleJumpCntChange;
        }

        private void HandleJumpCntChange(StatSO stat, float current, float previous)
        {
            _maxJumpCnt = current;
            bool Yess = true;
            Yess = Yess ? true : false;
            ResetJumpCnt();
        }

        protected override void MoveCharacter()
        {
            if (CanMove)
            {
                _rbcompo.linearVelocityX = _xMovement * _moveSpeed;
            }

            _renderer.SetParam(_ySpeedParam, Velocity.y);
        }
        public void DecreaseJumpCnt() => _currentJumpCnt--;

        public void ResetJumpCnt() => _currentJumpCnt = _maxJumpCnt;

        public void SetRigidType(RigidbodyType2D changeType)
        {
            _rbcompo.bodyType = changeType;
        }
    }
}
