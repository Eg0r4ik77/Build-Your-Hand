using System;
using System.Collections;
using UnityEngine;

namespace PuzzleGames
{
    public abstract class PuzzleGame : MonoBehaviour
    {
        protected bool IsInitialized { get; set; }
        public bool IsFinished { get; private set; }
        
        public event Action Interrupted;
        public event Action Finished;
        
        public void StartGame()
        {
            if (!IsInitialized)
            { 
                InitializeGame();
            }
        }

        protected abstract void InitializeGame();
        public abstract void UpdateGame();
        public abstract bool IsGameOver();

        public void InterruptGame()
        {
            Interrupted?.Invoke();
        }

        protected void FinishGame()
        {
            StartCoroutine(FinishGameCoroutine());
        }

        private IEnumerator FinishGameCoroutine()
        {
            const float deactivateTimeInSeconds = .3f;
            yield return new WaitForSeconds(deactivateTimeInSeconds);
            
            IsFinished = true;
            Finished?.Invoke();
        }
    }
}