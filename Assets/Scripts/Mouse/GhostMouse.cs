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
        private IEnumerator fadeInCoroutine;


        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            fadeInCoroutine = null;
            fadeOutCoroutine = StartCoroutineWithDelay(1, FadeOut());
            StartCoroutine(fadeOutCoroutine);
        }

        private void Update()
        {
            if (IsInRangeOfHeadChef() && fadeInCoroutine == null && !Visible) // check fade in not already happening
            {
                print("Pre Show Mouse: " + GetAlpha());
                // ShowMouse();
            }
            else if (!IsInRangeOfHeadChef() && fadeOutCoroutine == null && Visible) // check fade out is not already happening
            {
                print("Pre hide");
                HideMouse();
                print("post hide alpha: " + GetAlpha());
            }
        }

        private void HideMouse()
        {
            if (fadeInCoroutine != null) //check currently fading
            {
                // interrupt fade in
                StopCoroutine(fadeInCoroutine);
                fadeInCoroutine = null;
            }

            fadeOutCoroutine = FadeOut();
            StartCoroutine(fadeOutCoroutine);
        }

        private void ShowMouse()
        {
            print("alpha 1: " + GetAlpha());
            if (fadeOutCoroutine != null) //check currently fading
            {
                print("HERE");
                // interrupt fade out
                StopCoroutine(fadeOutCoroutine);
                fadeOutCoroutine = null;
            }

            print("alpha 2: " + GetAlpha());

            fadeInCoroutine = FadeIn();
            StartCoroutine(fadeInCoroutine);
        }

        private IEnumerator FadeIn()
        {
            print("alpha 3: " + GetAlpha());

            Visible = true;
            while (GetAlpha() < 1)
            {
                float n = (GetAlpha() + 0.01f);
                ChangeAlpha(GetAlpha() + 0.01f);
                print("alpha 4: " + GetAlpha());

                yield return new WaitForSeconds(0.01f);
            }

            fadeInCoroutine = null;
            print("complete");
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