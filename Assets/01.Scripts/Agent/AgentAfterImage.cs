using BGD.ObjPooling;
using BGD.Players;
using BGD.StatSystem;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BGD.Agents
{
    public class AgentAfterimage : MonoBehaviour, IAgentComponent, IAfterInit
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _fadeTime;

        private Player _player;
        private AgentRenderer _renderer;

        private Coroutine _spawnCoroutine;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _renderer = agent.GetCompo<AgentRenderer>(true);
        }
        public void AfterInit()
        {

        }
        
        public void Play()
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public void Stop()
        {
            Debug.Assert(_spawnCoroutine != null, "Play도 제대로 안하면서");
            StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator Spawn()
        {
            while(true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                AfterimageSpawn();
            }

        }

        private void AfterimageSpawn()
        {
            AfterImage obj = PoolingManager.Instance.Pop("AfterImage") as AfterImage;
            obj.renderer.sprite = _renderer.SpriteRenderer.sprite;
            obj.renderer.flipX = _renderer.FacingDirection > 0 ? false : true ;
            obj.renderer.color = new Vector4(1,1,1, 0.75f);
            obj.transform.position = _player.transform.position;
            obj.renderer.DOFade(0, _fadeTime);
        }

    }
}
