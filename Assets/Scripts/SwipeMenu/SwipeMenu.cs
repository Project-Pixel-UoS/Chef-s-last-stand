using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SwipeMenu
{
    public class SwipeMenu : MonoBehaviour
    {

        public GameObject scrollbar;
        private float scrollPos;
        private float[] posArray;
        private int currentLevel;
        private void Start()
        {
            posArray = new float[transform.childCount];
            float thresholdDistance = GetThresholdDistance();
            for (int i = 0; i < posArray.Length; i++)
            {
                posArray[i] = thresholdDistance * i * 2;
            }
        }

        private float GetThresholdDistance()
        {
            return (1f / (posArray.Length - 1)) / 2f;
        }

        private void Update()
        {
 
            float scrollBarValue = scrollbar.GetComponent<Scrollbar>().value;
            if (Input.GetMouseButton(0))
            {
                scrollPos = scrollBarValue;
            }
            else
            {
                foreach (var pos in posArray)
                {
                    if (CheckPosRelatesToCurrentSelection(pos))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBarValue, pos, 0.1f);
                    }
                }
            }
            
            for(int i = 0; i < posArray.Length; i++)
            {
                if (CheckPosRelatesToCurrentSelection(posArray[i]))
                {
                    currentLevel = i + 1;
                    var currentSelection = transform.GetChild(i);
                    currentSelection.localScale = Vector2.Lerp(currentSelection.localScale, new Vector2(1f, 1f), 0.1f);
                    for (int a = 0; a < posArray.Length; a++)
                    {
                        if (a != i)
                        {
                            transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }
        }
        
        private bool CheckPosRelatesToCurrentSelection(float pos)
        {
            float distance = GetThresholdDistance();
            return scrollPos < pos + (distance) && scrollPos > pos - (distance);
        }

        public void LoadSelectedLevel()
        {
            SceneManager.LoadScene("Level " + currentLevel);

        }
    }
}