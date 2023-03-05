using System;
using System.Collections;
using UnityEngine;

namespace PuzzleGames
{
    public abstract class PuzzleGame : MonoBehaviour
    {
        protected const float ClosingDurationInSeconds = .8f;
        protected bool Initialized;

        public event Action Finished;
        
        public abstract void InitializeGame();
        public abstract void StartGame();
        public abstract void UpdateGame();
        public abstract bool IsGameOver();
        public abstract void FinishGame();
        

        protected IEnumerator CloseGame()
        {
            yield return new WaitForSeconds(ClosingDurationInSeconds);
            Finished?.Invoke();
            gameObject.SetActive(false);
        }
    }
}