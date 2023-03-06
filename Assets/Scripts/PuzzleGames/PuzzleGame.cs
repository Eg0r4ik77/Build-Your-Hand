using System;
using System.Collections;
using UnityEngine;

namespace PuzzleGames
{
    public abstract class PuzzleGame : MonoBehaviour
    {
        private const float ClosingDurationInSeconds = .8f;

        protected bool IsInitialized { get; set; }
        public bool IsFinished { get; private set; }

        public event Action Finished;
        
        public abstract void InitializeGame();
        public abstract void StartGame();
        public abstract void UpdateGame();
        public abstract bool IsGameOver();

        public void InterruptGame()
        {
            StartCoroutine(CloseGame());
        }

        protected void FinishGame()
        {
            IsFinished = true;
            Finished?.Invoke();
            StartCoroutine(CloseGame());
        }
        
        private IEnumerator CloseGame()
        {
            yield return new WaitForSeconds(ClosingDurationInSeconds);
            gameObject.SetActive(false);
        }
    }
}