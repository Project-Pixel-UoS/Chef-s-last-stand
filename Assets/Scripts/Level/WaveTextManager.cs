using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Level.WaveData
{
    public class WaveTextManager : MonoBehaviour
    {
        [SerializeField] private Text waveText;

        private void Start()
        {
            this.waveText.enabled = false;
        }


        public IEnumerator  DisplayLevelComplete()
        {
            yield return new WaitForSeconds(1);
            waveText.enabled = true;
            waveText.text = "Level Complete!";
        }

        public IEnumerator  DisplayFinishedWaveText()
        {
            yield return new WaitForSeconds(1);
            waveText.text = "Wave Finished";
            waveText.enabled = true;
            yield return new WaitForSeconds(3);
            waveText.enabled = false;
            yield return new WaitForSeconds(1);
        }

        public IEnumerator DisplayStartingWaveText(int currentWave)
        {
            waveText.text = "Wave " + (currentWave + 1) + " Starting";
            waveText.enabled = true;
            yield return new WaitForSeconds(3);
            waveText.enabled = false;
            yield return new WaitForSeconds(1);
        }


    }
}