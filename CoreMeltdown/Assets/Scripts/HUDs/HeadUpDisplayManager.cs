using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class HeadUpDisplayManager : MonoBehaviour
    {
        public Text radiationLevel;
        public Text shieldLevel;

        public Color danger;

        private readonly IList<Player> _players = new List<Player>();

        void Update()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                Player player = _players[i];

                float shieldRate = 100f * player.Shield / Player.MaxShield;
                shieldLevel.text = shieldRate.ToString("##0") + "%";

                float healthRate = 100f * player.Health / Player.MaxHealth;
                
                //float radiationRate = 100f - 
                //if (radiationRate > 90)
                //{
                //    radiationLevel.color = danger;
                //}

                radiationLevel.text = healthRate.ToString("##0") + "%";

                
            }
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }
    }
}