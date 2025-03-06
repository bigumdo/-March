using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace BGD.Agents
{
    public class Agent : MonoBehaviour
    {
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeadEvent;

        public bool IsDead { get; set; }

        protected Dictionary<Type, IAgentComponent> _components;

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IAgentComponent>();
            //(true)-> 비활성화 되어있는 오브젝트도 포함
            GetComponentsInChildren<IAgentComponent>(true).ToList()
                .ForEach(component => _components.Add(component.GetType(), component));

            InitComponenet();
            AfterInitComponenets();
        }

        protected virtual void InitComponenet()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        protected virtual void AfterInitComponenets()
        {
            _components.Values.ToList().ForEach(component =>
            {
                if (component is IAfterInit afterInit)
                {
                    afterInit.AfterInit();
                }
            });

            OnHitEvent.AddListener(HandleHitEvent);
            OnDeadEvent.AddListener(HandleDeadEvent);
        }

        public T GetCompo<T>(bool isDerived = false) where T : IAgentComponent
        {
            if(_components.TryGetValue(typeof(T), out IAgentComponent compo))
            {
                return (T)compo;
            }

            if (!isDerived)
                return default;

            Type findType = _components.Keys.FirstOrDefault(t => t.IsSubclassOf(typeof(T)));

            if(findType != null)
                return (T)_components[findType];

            return default;
        }

        private void OnDestroy()
        {
            OnHitEvent.RemoveListener(HandleHitEvent);
            OnDeadEvent.RemoveListener(HandleDeadEvent);
        }

        public virtual void HandleHitEvent()
        {
            
        }

        public virtual void HandleDeadEvent()
        {
            
        }
    }
}
