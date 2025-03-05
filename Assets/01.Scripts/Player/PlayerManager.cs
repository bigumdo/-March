using UnityEngine;

namespace BGD.Players
{
    [CreateAssetMenu(fileName = "PlayerManager", menuName = "SO/PlayerManager")]
    public class PlayerManager : ScriptableObject
    {
        private Player _player;
        public Player Player
        {
            get
            {
                if(_player == null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                    Debug.Assert(_player != null, "Player¾ø´Ù");
                }
                return _player;
            }

            set { Player = value; }
        }

        public Transform PlayerTransform => Player.transform;

    }
}
