using System;
using UnityEngine;
using UnityEngine.UI;


namespace Chef.Upgrades
{
    /// <summary>
    /// class tracks how far along each upgrade path the chef is
    /// </summary>
    public class UpgradeTracker : MonoBehaviour
    {
        private int path1Status, path2Status;
        private int maxLevel1 = 4;
        private int maxLevel2 = 4;


        

        /// <summary>
        /// invoked upon Upgrade Range button on the bottom bar is clicked
        /// </summary>
        public void UpgradePath1()
        {
            if (path1Status == maxLevel1) return; // if path is upgraded to the max we can not upgrade it again
            path1Status++;
            GetComponent<Range>().Radius *= 1.1f;
            if (gameObject.CompareTag("Grillardin"))
            {
                var main =  GetComponentInChildren<ParticleSystem>().main;
                var emission = GetComponentInChildren<ParticleSystem>().emission;
                main.startSpeed = 1.2f * main.startSpeed.constant;
                emission.rateOverTime = 1.2f * emission.rateOverTime.constant;
            }
            RefreshHealthBar1();

        }
        
        public void UpgradePath2()
        {
            
        }

        public void RefreshHealthBar1()
        {
            var upgradeBar1 = GameObject.FindGameObjectWithTag("UpgradeBar1").GetComponent<Image>();
            upgradeBar1.fillAmount = (float) path1Status / maxLevel1;
        }
        
        
        

    }
}