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
        
        private enum State
        {
            Visible,
            FadeIn,
            Invisible,
            FadeOut
        }
        
        private SpriteRenderer spriteRenderer;
        public bool Visible { get; private set; }

        // private IEnumerator fadeOutCoroutine;
        // private IEnumerator fadeInCoroutine;
        private IEnumerator fadeCoroutine;

        private State state = State.FadeOut;


        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            // fadeInCoroutine = null;
            fadeCoroutine = StartCoroutineWithDelay(2, FadeOut());
            StartCoroutine(fadeCoroutine);
        }

        private void Update()
        {
            if (IsInRangeOfHeadChef() && state != State.FadeIn && state != State.Visible) // check fade in not already happening
            {
                // print("Pre Show Mouse: " + GetAlpha());
                ShowMouse();
            }
            else if (!IsInRangeOfHeadChef() && state != State.FadeOut && state != State.Invisible) // check fade out is not already happening
            {
                // print("Pre hide");
                HideMouse();
                // print("post hide alpha: " + GetAlpha());
            }
        }

        private void HideMouse()
        {
            print("hide mouse");
            if (state == State.FadeIn) //check currently fading
            {
                // interrupt fade in
                StopCoroutine(fadeCoroutine);
                // fadeInCoroutine = null;
            }

            fadeCoroutine = FadeOut();
            StartCoroutine(fadeCoroutine);
        }

        private void ShowMouse()
        {
            print("show mouse");
            // print("alpha 1: " + GetAlpha());
            if (state == State.FadeOut) 
            {
                StopCoroutine(fadeCoroutine);
                // fadeOutCoroutine = null;
            }
            // print("alpha 2: " + GetAlpha());
            fadeCoroutine = FadeIn();
            StartCoroutine(fadeCoroutine);
        }

        private IEnumerator FadeIn()
        {
            
            print("Fading in!");
            // print("alpha 3: " + GetAlpha());
            state = State.FadeIn;
            // Visible = true;
            while (GetAlpha() < 1)
            {
                float n = (GetAlpha() + 0.01f);
                ChangeAlpha(GetAlpha() + 0.01f);
                // print("alpha 4: " + GetAlpha());

                yield return new WaitForSeconds(0.01f);
            }

            fadeCoroutine = null;
            state = State.Visible;
            // print("complete");
        }

        private IEnumerator FadeOut()
        {
            print("Fading out!");

            state = State.FadeOut;
            while (GetAlpha() >= 0.1)
            {
                ChangeAlpha(GetAlpha() - 0.01f);
                yield return new WaitForSeconds(0.01f);
            }

            // Visible = false;
            fadeCoroutine = null;
            state = State.Invisible;
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
            print("changing alpha: " + alpha);
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