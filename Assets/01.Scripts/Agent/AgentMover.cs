using BGD.Animators;
using BGD.Casters;
using BGD.FSM;
using BGD.StatSystem;
using System;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentMover : MonoBehaviour, IAgentComponent, IAfterInit
    {
        public Vector2 Velocity => _rbcompo.linearVelocity;
        public bool CanMove { get; set; } = true;
        public bool IsGrounded { get; set;}
        public float LimitYSpeed { get; set;}

        [Header("MoveStat")]
        [SerializeField] protected StatSO _moveSpeedStat;
        protected float _moveSpeed;


        [Header("AnimParams")]
        [SerializeField] protected AnimParamSO _ySpeedParam;

        protected Rigidbody2D _rbcompo;
        protected Agent _agent;
        protected AgentRenderer _renderer;
        protected AgentStat _agentStat;
        protected AgentCaster _caster;

        protected float _xMovement;

        protected Collider2D _collider;

        public virtual void Initialize(Agent agent)
        {
            _agent = agent;
            _rbcompo = agent.GetComponent<Rigidbody2D>();
            _renderer = agent.GetCompo<AgentRenderer>(true);
            _agentStat = agent.GetCompo<AgentStat>();
            _caster = agent.GetCompo<AgentCaster>();
            _collider = agent.GetComponent<Collider2D>();
        }

        public virtual void AfterInit()
        {
            _agentStat.MoveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _agentStat.MoveSpeedStat.Value;
            LimitYSpeed = 40f;
        }

        protected virtual void OnDestroy()
        {
            _agentStat.MoveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _moveSpeed = current;
        }

        public void AddForce(Vector2 force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
        {
            _rbcompo.AddForce(force, forceMode2D);
        }

        public void AddForceX(float force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
        {
            _rbcompo.AddForceX(force, forceMode2D);
        }

        public void AddForceY(float force, ForceMode2D forceMode2D = ForceMode2D.Impulse)
        {
            _rbcompo.AddForceY(force, forceMode2D);
        }

        public void StopImmediately(bool resetYAxis = false)
        {
            if(resetYAxis)
            {
                _rbcompo.linearVelocity = Vector2.zero;
            }
            else
            {
                _rbcompo.linearVelocityX = 0;
            }
        }

        public void SetMovement(float xMovement) => _xMovement = xMovement;

        public virtual void FixedUpdate()
        {
            CheckGround();
            MoveCharacter();

            _rbcompo.linearVelocityY = Math.Clamp(_rbcompo.linearVelocityY, -LimitYSpeed, LimitYSpeed);
            
        }

        protected virtual void MoveCharacter()
        {
            if(CanMove)
            {
                _rbcompo.linearVelocityX = _xMovement * _moveSpeed;
            }

            _renderer.FlipControl(_xMovement);
            _renderer.SetParam(_ySpeedParam, Velocity.y);
        }

        protected virtual void CheckGround()
        {
            IsGrounded = _caster.Cast(CastTypeEnum.Ground);
        }
        
        protected virtual void IsWallDetected()
        {

        }
    }
}
