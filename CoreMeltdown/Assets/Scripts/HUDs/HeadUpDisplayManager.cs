using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class HeadUpDisplayManager : MonoBehaviour
    {
        public Text healthLevel;
        public Text shieldLevel;
        public Text radiationLevel;

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
                
                healthLevel.text = healthRate.ToString("##0") + "%";
            }
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void SetRadiationLevel(float radiation)
        {
            radiationLevel.text = radiation.ToString("0.0");
        }
    }
}