using System;
using GameManagement;
using UnityEngine;


namespace Cheese
{
    public class Cheese : MonoBehaviour
    {
        [SerializeField] private Sprite[] cheeseStage;
        private int currentCheeseStageIndex;

        private PlayerHealthManager healthManager;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            healthManager = GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHealthManager>();
            spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            currentCheeseStageIndex = 0;
            GameManager.gameManager.onGameOver += DisableCheese;
        }

        private void DisableCheese()
        {
            gameObject.SetActive(false);
        }

        public void UpdateSpriteIfNecessary()
        {
            var newCheeseStageIndex = CalculateNewCheeseStageIndex();
            if (currentCheeseStageIndex != newCheeseStageIndex)
            {
                spriteRenderer.sprite = cheeseStage[newCheeseStageIndex];
                currentCheeseStageIndex = newCheeseStageIndex;
            }
  
        }

   

        private int CalculateNewCheeseStageIndex()
        {
            double healthPercentage = healthManager.GetCurrentHealthAsPercentage();
            double indicesPerCheeseSprite = 100f / cheeseStage.Length;
            return cheeseStage.Length - (int)Math.Ceiling(healthPercentage / indicesPerCheeseSprite);
        }
        
        
    }
}