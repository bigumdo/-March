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
        private AgentMover _mover;
        private List<GameObject> _effects = new List<GameObject>();
        private AgentRenderer _renderer;

        private Coroutine _spawnCoroutine;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _renderer = agent.GetCompo<AgentRenderer>(true);
            _mover = agent.GetCompo<AgentMover>(true);
        }
        public void AfterInit()
        {

        }

        private void Update()
        {
            if(_effects.Count > 0)
                for (int i = 0; i < _effects.Count; ++i)
                {
                    SpriteRenderer renderer = _effects[i].transform.GetComponent<SpriteRenderer>();
                    if (renderer != null && renderer.color.a <= 0)
                    {
                        GameObject.Destroy(_effects[i]);
                        _effects.Remove(_effects[i]);
                    }
                }
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
            GameObject obj = new GameObject();
            SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = _renderer.SpriteRenderer.sprite;
            renderer.flipX = _renderer.FacingDirection > 0 ? false : true ;
            renderer.color = new Vector4(1,1,1,0.75f);
            obj.transform.position = _player.transform.position;
            _effects.Add(obj);
            renderer.DOFade(0, _fadeTime);
        }

    }
}
