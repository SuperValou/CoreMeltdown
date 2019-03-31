using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class RadiationsHUD : BaseHUD
    {
        public Text radiationLevel;
        
        public void SetRadiationLevel(float radiation)
        {
            radiationLevel.text = radiation.ToString("0.0");
            
            if (radiation > dangerThreshold)
            {
                radiationLevel.color = danger;
            }
            else if (radiation > warningThreshold)
            {
                radiationLevel.color = warning;
            }
            else
            {
                radiationLevel.color = safe;
            }
        }
    }
}