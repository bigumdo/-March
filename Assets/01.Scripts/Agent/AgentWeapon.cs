using BGD.Players;
using BGD.Weapons;
using UnityEngine;

namespace BGD.Agents
{
    public class AgentWeapon : MonoBehaviour, IAgentComponent
    {
        private Player _player;
        private PlayerRenderer _renderer;
        private Weapon _currentWeapon;
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _renderer = agent.GetCompo<PlayerRenderer>();
            _currentWeapon = GetComponentInChildren<Weapon>();
        }

        private void Update()
        {
            if(_renderer.FacingDirection > 0)
                _currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, _renderer.MouseAngle);
            else
                _currentWeapon.transform.localRotation = Quaternion.Euler(180, 180, -_renderer.MouseAngle);
        }
    }
}

