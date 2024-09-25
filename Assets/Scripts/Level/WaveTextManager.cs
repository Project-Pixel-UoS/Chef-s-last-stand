using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Level.WaveData
{
    public class WaveTextManager : MonoBehaviour
    {
        [SerializeField] private GameObject waveText;
        private Text text;

        private void Awake()
        {
            text = waveText.GetComponent<Text>();


        }


        public IEnumerator  DisplayLevelComplete()
        {
            yield return new WaitForSeconds(1);
            waveText.SetActive(true); 
            text.text = "Level Complete!";
        }

        public IEnumerator  DisplayFinishedWaveText()
        {
            yield return new WaitForSeconds(1);
            text.text = "Wave Finished";
            waveText.SetActive(true); 

            yield return new WaitForSeconds(3);
            waveText.SetActive(false); 

            yield return new WaitForSeconds(1);
        }

        public IEnumerator DisplayStartingWaveText(int currentWave)
        {
            text.text = "Wave " + (currentWave + 1);
            waveText.SetActive(true); 

            yield return new WaitForSeconds(3);
            waveText.SetActive(false); 

            yield return new WaitForSeconds(1);
        }


    }
}