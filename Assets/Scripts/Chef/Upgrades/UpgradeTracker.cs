using UnityEngine;
using UnityEngine.UI;


namespace Chef.Upgrades
{
    /// <summary>
    /// class tracks how far along each upgrade path the chef is
    /// </summary>
    public class UpgradeTracker
    {
        private int path1Status, path2Status;
        public int maxLevel1;
        public int maxLevel2;

        [SerializeField] private Image upgradeBar1;


        /// <summary>
        /// invoked upon Upgrade Range button on the bottom bar is clicked
        /// </summary>
        public void UpgradePath1()
        {
            if (path1Status == maxLevel1) return; // if path is upgraded to the max we can not upgrade it again
            path1Status++;
            RefreshHealthBar1();

        }
        
        public void UpgradePath2()
        {
            
        }

        private void RefreshHealthBar1()
        {
            upgradeBar1.fillAmount = (float) path1Status / maxLevel1;
        }
        
        
        

    }
}