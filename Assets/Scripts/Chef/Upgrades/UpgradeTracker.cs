using System;
using Unity.VisualScripting;
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
            RefreshRangeBar();
        }
        
        /// <summary>
        /// invoked upon Upgrade Special button on the bottom bar is clicked
        /// </summary>
        public void UpgradePath2()
        {
            if (path2Status == maxLevel2) return;
            path2Status++;
            var chef = GetChefUpgrades();
            //instantiate new chef
            var chefParent = GameObject.FindGameObjectWithTag("ChefContainer");
            GameObject newChef = Instantiate(chef[path2Status], transform.position, transform.rotation, chefParent.transform);
            var range = newChef.GetComponent<Range>();
            range.Radius = GetComponent<Range>().Radius; //copy range upgrade status over
            range.EnableRangeRenderer(); //keep range active
            DuplicateUpgradeStats(newChef); //change new chef stats
            RefreshSpecialBar();
            ChefTracker.Instance.CurrentChef = newChef;
            Destroy(gameObject);
        }

        /// <summary>
        /// updates visual of range upgrade bar.
        /// </summary>
        public void RefreshRangeBar()
        {
            var upgradeBar1 = GameObject.FindGameObjectWithTag("UpgradeBar1").GetComponent<Image>();
            upgradeBar1.fillAmount = (float) path1Status / maxLevel1;
        }

        /// <summary>
        /// updates visual of special upgrade bar.
        /// </summary>
        public void RefreshSpecialBar()
        {
            var upgradeBar2 = GameObject.FindGameObjectWithTag("UpgradeBar2").GetComponent<Image>();
            upgradeBar2.fillAmount = (float) path2Status / maxLevel2;
        }

       
        /// <returns> an array of selected chef's upgrade prefabs</returns>
        public GameObject[] GetChefUpgrades()
        {
            if (transform.tag.Equals("PrepCook"))
            {
                return ChefTracker.Instance.GetPrepCooks();
            }
            return null;
        }

        /// <summary>
        /// copies the current chef's upgrade statuses to the upgraded version.
        /// </summary>
        /// <param name="newChef">the upgraded chef to paste the statuses to.</param>
        public void DuplicateUpgradeStats(GameObject newChef)
        {
            var upgradeTracker = newChef.GetComponent<UpgradeTracker>();
            upgradeTracker.SetPath1Status(path1Status);
            upgradeTracker.SetPath2Status(path2Status); 
        }

        public void SetPath1Status(int i) { path1Status = i; }
        public void SetPath2Status(int i) { path2Status = i; }
        public int getPath2Status() {  return path2Status; }
    }
}