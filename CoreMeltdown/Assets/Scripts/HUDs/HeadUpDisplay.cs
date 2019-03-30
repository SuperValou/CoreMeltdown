using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class HeadUpDisplay : MonoBehaviour
    {
        public Player player;
        public Text radiationLabel;
        public Text shieldLabel;

        public Color danger;

        void Update()
        {
            float radiationRate = 100 - player.Health / player.maxHealth;

            if (radiationRate < 10)
            {
                radiationLabel.color = Color.red;
            }

            radiationLabel.text = radiationRate.ToString("##0.00") + "%";

            float shieldRate = player.Shield / player.maxShield;
            shieldLabel.text = shieldRate.ToString("##0.00") + "%";
        }
    }
}