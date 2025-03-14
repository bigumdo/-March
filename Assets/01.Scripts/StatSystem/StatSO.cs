using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGD.StatSystem
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSystem/Stat")]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, float current, float previous);
        public event ValueChangeHandler OnValueChange;

        public string statName;
        public string description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _displayName;
        [SerializeField] private float _baseValue, _minValue, _maxValue;
        private Dictionary<string, float> _modifyValueByKey = new Dictionary<string, float>();
        
        [field:SerializeField] public bool IsPercent { get; private set; }
        
        private float _modifiedValue = 0;
        public Sprite Icon => _icon;
        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public float MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }
        
        public float Value => Mathf.Clamp(_baseValue + _modifiedValue, _minValue, _maxValue);
        public bool IsMax => Mathf.Approximately(Value, _maxValue);
        public bool IsMin => Mathf.Approximately(Value, _minValue);

        public float BaseValue
        {
            get => _baseValue;
            set
            {
                float prevValue = Value;
                _baseValue = Mathf.Clamp(value, _minValue, _maxValue);
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }

        public void AddModifier(string key, float value)
        {
            //키가 있다면 return
            if (_modifyValueByKey.ContainsKey(key)) return;
            
            float prevValue = Value;
            _modifiedValue += value;
            _modifyValueByKey.Add(key, value);
            
            TryInvokeValueChangedEvent(Value, prevValue);
        }

        public void RemoveModifier(string key)
        {
            //만약에 키가 있다면 실행된다.
            if (_modifyValueByKey.TryGetValue(key, out float value))
            {
                float prevValue = Value;
                _modifiedValue -= value;
                _modifyValueByKey.Remove(key);
                
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }

        private void TryInvokeValueChangedEvent(float value, float prevValue)
        {
            //아주 작은 값들에 비교에서 사용되는 함수 거의 차이가 없다면 같은 것으로 간주한다.
            if(Mathf.Approximately(prevValue, value) == false) 
                OnValueChange?.Invoke(this, value, prevValue);
        }

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
