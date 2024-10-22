using System;
using System.Collections;
using Color;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Level.WaveData
{
    public class WaveTextManager : MonoBehaviour
    {
        [SerializeField] public GameObject waveTextBorder;
        private Text text;

        private void Awake()
        {
            text = waveTextBorder.GetComponentInChildren<Text>();
            SetUpWaveText();
        }
        
        public IEnumerator  DisplayLevelComplete()
        {
            yield return new WaitForSeconds(1);
            waveTextBorder.SetActive(true); 
            text.text = "Level Complete!";
        }

        public IEnumerator DisplayStartingWaveText(int currentWave)
        {
            text.text = "Wave " + (currentWave + 1);
            waveTextBorder.SetActive(true);
            yield return new WaitForSeconds(2);
            waveTextBorder.SetActive(false); 

            yield return new WaitForSeconds(1);
        }

        
        private void SetUpWaveText()
        {
            Vector2 newPos = text.rectTransform.anchoredPosition;
            newPos.x = 105;
            newPos.y = -14;
            text.rectTransform.anchoredPosition = newPos;
            
            Vector2 newSize = text.rectTransform.sizeDelta;
            newSize.x = 597.5f;
            text.rectTransform.sizeDelta = newSize;
            
            text.fontSize = 150;
        }

        // private void SetUpLevelCompleteText()
        // {
        //     Vector2 newPos = text.rectTransform.anchoredPosition;
        //     newPos.x = 13;
        //     newPos.y = -28;
        //     text.rectTransform.anchoredPosition = newPos;
        //     
        //     Vector2 newSize = text.rectTransform.sizeDelta;
        //     newSize.x = 664;
        //     text.rectTransform.sizeDelta = newSize;
        //     
        //     text.fontSize = 105;
        // }


    }
}