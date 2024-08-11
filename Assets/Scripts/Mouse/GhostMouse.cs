using System.Collections;
using Chef;
using Range;
using UnityEngine;

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
        private IEnumerator fadeCoroutine;
        private State currentState = State.FadeOut;


        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            fadeCoroutine = StartCoroutineWithDelay(2, FadeOut());
            StartCoroutine(fadeCoroutine);
        }

        private void Update()
        {
            if (IsInRangeOfHeadChef() && currentState != State.FadeIn && currentState != State.Visible)
            {
                ShowMouse();
            }
            else if (!IsInRangeOfHeadChef() && currentState != State.FadeOut && currentState != State.Invisible) 
            {
                HideMouse();
            }
        }

        private void HideMouse()
        {
            if (currentState == State.FadeIn) //check currently fading
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = FadeOut();
            StartCoroutine(fadeCoroutine);
        }

        private void ShowMouse()
        {
            if (currentState == State.FadeOut) 
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = FadeIn();
            StartCoroutine(fadeCoroutine);
        }

        private IEnumerator FadeIn()
        {
            currentState = State.FadeIn;
            while (GetAlpha() < 1)
            {
                ChangeAlpha(GetAlpha() + 0.01f);
                yield return new WaitForSeconds(0.01f);
            }
            fadeCoroutine = null;
            currentState = State.Visible;
        }

        private IEnumerator FadeOut()
        {
            currentState = State.FadeOut;
            while (GetAlpha() >= 0.1)
            {
                ChangeAlpha(GetAlpha() - 0.01f);
                yield return new WaitForSeconds(0.01f);
            }
            fadeCoroutine = null;
            currentState = State.Invisible;
        }

        private bool IsInRangeOfHeadChef()
        {
            foreach (var headChef in GameObject.FindGameObjectsWithTag("HeadChef"))
            {
                if (headChef.GetComponent<ChefRange>().IsMouseInRange(gameObject))
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

        public bool IsVisible()
        {
            return currentState is State.Visible or State.FadeIn;
        }
    }
}