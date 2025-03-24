using BGD.Agents;
using BGD.Animators;
using BGD.FSM;
using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.Players
{
    public class PlayerDashState : AgentState
    {
        private Coroutine _spawnCoroutine;
        private Player _player;
        private AgentMover _mover;
        private List<GameObject> _effects = new List<GameObject>();
        private AgentRenderer _renderer;

        public PlayerDashState(Agent agent, AnimParamSO animParam) : base(agent, animParam)
        {
            _player = agent as Player;
            _mover = agent.GetCompo<AgentMover>(true);
            _renderer = agent.GetCompo<AgentRenderer>(true);
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);
            _mover.AddForceX(10);
            _effects.Clear();
            _spawnCoroutine = _player.StartCoroutine(DashEffect());
        }

        private IEnumerator DashEffect()
        {
            while(true)
            {
                yield return new WaitForSeconds(0.2f);
                Spawn();
            }
        }

        public override void Update()
        {
            base.Update();
            if (_isEndTrigger)
                _player.ChangeState(FSMState.IDLE);
        }

        public override void Exit()
        {
            _player.StopCoroutine(_spawnCoroutine);

            Debug.Log(_effects.Count);
            for (int i = 0; i < _effects.Count; ++i)
            {
                Debug.Log(i);
                SpriteRenderer renderer = _effects[i].transform.GetComponent<SpriteRenderer>();
                if (renderer != null && renderer.color.a <= 0)
                {
                    GameObject.Destroy(_effects[i]);
                }
            }
            _effects.Clear();
            base.Exit();
        }

        private void Spawn()
        {
            GameObject obj = new GameObject();
            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = _renderer.SpriteRenderer.sprite;
            obj.transform.position = _player.transform.position;
            _effects.Add(obj);
            renderer.DOFade(0, 0.1f);
        }

    }
}
