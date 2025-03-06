using BGD.Agents;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BGD.Casters
{
    public enum CastTypeEnum // Cast타입
    {
        Damge,
        AttackRnage,
        PlayerAttack,
        Ground,
        Wall
    }

    public class Caster : MonoBehaviour, IAgentComponent
    {
        private Agent _agent;
        private Dictionary<CastTypeEnum, BaseCaster> _casters; //Cast할 종류Dictionary
        private Collider2D[] _castTargets; // 감지한 오브젝트의 Collider를 담는 변수
        private AgentRenderer _agentRenderer;
        private Vector2 _agentDir;
        [SerializeField] private BaseCaster _currentCast;//현재 감자히는 Caster를 담아 놓는 변수

        public void Initialize(Agent agent)
        {
            _agent = agent;
            _agentRenderer = agent.GetCompo<AgentRenderer>(true);
            _casters = new Dictionary<CastTypeEnum, BaseCaster>();
            BaseCaster[] casts = GetComponentsInChildren<BaseCaster>();//감지할 캐스트 종류를 가져옴
            foreach (BaseCaster c in casts)
            {
                c.Initialize(agent);
                _casters.Add(c.castType, c); // Enum으로 cast구결하기 위해 추가
            }
        }

        public bool Cast(CastTypeEnum castType, bool multiCast = true)
        {
            _currentCast = _casters.GetValueOrDefault(castType);//타입에 맞는 Cast를 갖고 온다.
            Debug.Assert(_currentCast != null, $"{castType}cast없어 돌아가");// CurrentCast가 Null아니라면 실행

            switch (_currentCast.castMethodType)
            {
                case CastMethodType.Circle:
                    return CircleCast();
                case CastMethodType.Box:
                    return BoxCast();
                case CastMethodType.Ray:
                    return RayCast();
            }
            return false;
        }

        private bool RayCast()
        {
            switch (_currentCast.rayDirection)
            {
                case RayDirection.Right:
                    _agentDir = Vector2.right;
                    break;
                case RayDirection.Left:
                    _agentDir = Vector2.left;
                    break;
                case RayDirection.Up:
                    _agentDir = Vector2.up;
                    break;
                case RayDirection.Down:
                    _agentDir = Vector2.down;
                    break;
            }
            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, _agentDir, _currentCast.rayDistance);

            if (hit.collider != null)
            {
                if(_currentCast is IRaycastCaster rayCaster)
                {
                    rayCaster.RayCast(hit);
                }
            }
            return false;
        }

        private bool CircleCast()//원하는 캐스트 타입과 얼마나 체크할지를 받는다.
        {
            _agentDir = new Vector2(_currentCast.castOffset.x * _agentRenderer.FacingDirection, _currentCast.castOffset.y);
            _castTargets = Physics2D.OverlapCircleAll((Vector2)_currentCast.transform.position + _agentDir, _currentCast.castRange,
                 _currentCast.targetLayer, 0, _currentCast.castCnt);//cat설정에 맞게 OverapCircleAll체크
            if (_castTargets.Length > 0)
            {
                if (_currentCast is IColliderCaster colliderCaster)
                {
                    return colliderCaster.ColliderCast(_castTargets);
                }
            }
            return false;
        }

        private bool BoxCast()
        {
            _agentDir = new Vector2(_currentCast.castOffset.x * _agentRenderer.FacingDirection, _currentCast.castOffset.y);
            _castTargets = Physics2D.OverlapBoxAll((Vector2)_currentCast.transform.position + _agentDir, _currentCast.castSize,
                 0, _currentCast.targetLayer,0,_currentCast.castCnt);//cat설정에 맞게 OverapCircleAll체크
            if (_castTargets.Length > 0)
            {
                if (_currentCast is IColliderCaster colliderCaster)
                {
                    return colliderCaster.ColliderCast(_castTargets);
                }
            }
            return false;
        }
    }
}