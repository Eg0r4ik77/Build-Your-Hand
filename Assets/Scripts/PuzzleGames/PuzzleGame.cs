using System;
using UnityEngine;

namespace PuzzleGames
{
    public abstract class PuzzleGame : MonoBehaviour
    {
        protected bool IsInitialized { get; set; }
        public bool IsFinished { get; private set; }

        public event Action Finished;
        public event Action Interrupted;
        
        public abstract void InitializeGame();
        public abstract void StartGame();
        public abstract void UpdateGame();
        public abstract bool IsGameOver();

        public void InterruptGame()
        {
            Interrupted?.Invoke();
            gameObject.SetActive(false);
        }

        protected void FinishGame()
        {
            IsFinished = true;
            Finished?.Invoke();
            gameObject.SetActive(false);
        }
    }
}