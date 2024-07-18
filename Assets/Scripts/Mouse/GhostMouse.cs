using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Range = Chef.Range;

namespace Mouse
{
    
    /// <summary>
    /// Responsible for showing and hiding the ghost mouse
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class GhostMouse : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        public bool Visible { get; private set; }

        private IEnumerator fadeOutCoroutine;
 
        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            fadeOutCoroutine = StartCoroutineWithDelay(1, FadeOut());
            StartCoroutine(fadeOutCoroutine);
        }

        private void Update()
        {
            if (IsInRangeOfHeadChef())
            {
                ShowMouse();
            }
            else if (fadeOutCoroutine == null) // check fade out is not already happening
            {
                HideMouse();
            }
        }

        private void HideMouse()
        {
            print("HIDING MOUSE");
            fadeOutCoroutine = FadeOut();
            StartCoroutine(fadeOutCoroutine);
        }

        private void ShowMouse()
        {
            if (fadeOutCoroutine != null) //check currently fading
            {
                // interrupt fade out
                StopCoroutine(fadeOutCoroutine);
                fadeOutCoroutine = null;
            }
            //instantly show mouse
            Visible = true;
            ChangeAlpha(1);
        }

        private IEnumerator FadeOut()
        {
            while (GetAlpha() >= 0.1)
            {
                ChangeAlpha(GetAlpha() - 0.01f);
                yield return new WaitForSeconds(0.02f);
            }
            Visible = false;
            fadeOutCoroutine = null;
        }

        private bool IsInRangeOfHeadChef()
        {
            foreach (var headChef in GameObject.FindGameObjectsWithTag("HeadChef"))
            {
                if (headChef.GetComponent<Range>().IsMouseInRange(gameObject))
                {
                    return true;
                }
            }
            return false;
        }
        public void ChangeAlpha(float alpha)
        {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        private float GetAlpha()
        {
            return spriteRenderer.color.a;
        }
        
        private IEnumerator StartCoroutineWithDelay(float delay, IEnumerator targetCoroutine)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(targetCoroutine);
        }



    }
}