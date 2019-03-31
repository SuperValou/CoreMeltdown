using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class TimerHUD : BaseHUD
    {
        public Text remainingTime;
        
        public void DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < dangerThreshold)
            {
                remainingTime.text = timeToDisplay.ToString("0.0") + "s";
                remainingTime.color = danger;
            }
            else if (timeToDisplay < warningThreshold)
            {
                remainingTime.text = timeToDisplay.ToString("0.0") + "s";
                remainingTime.color = warning;
            }
            else
            {
                remainingTime.text = timeToDisplay.ToString("00") + "s";
                remainingTime.color = safe;
            }
        }
    }
}