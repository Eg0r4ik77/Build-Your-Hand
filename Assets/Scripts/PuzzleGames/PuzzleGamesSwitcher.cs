using System;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGames
{
    public class PuzzleGamesSwitcher : MonoBehaviour
    {
        [SerializeField] private List<PuzzleGame> _puzzleGames;
        [SerializeField] private GameObject _puzzleGamePanel;
        
        private PuzzleGame _currentGame;

        public Action<PuzzleGame> Opened;
        public Action Closed;

        private void Start()
        {
            foreach (PuzzleGame puzzleGame in _puzzleGames)
            {
                puzzleGame.Started += Open;
                puzzleGame.Finished += Close;
                puzzleGame.Interrupted += Close;
            }
        }

        private void OnDestroy()
        {
            foreach (PuzzleGame puzzleGame in _puzzleGames)
            {
                puzzleGame.Started -= Open;
                puzzleGame.Finished -= Close;
                puzzleGame.Interrupted -= Close;
            }
        }

        private void Open(PuzzleGame game)
        {
            Opened?.Invoke(game);
            
            _puzzleGamePanel.SetActive(true);
            _currentGame = game;
            _currentGame.gameObject.SetActive(true);
        }

        private void Close()
        {
            Closed?.Invoke();

            _puzzleGamePanel.SetActive(false);
            _currentGame.gameObject.SetActive(false);
            _currentGame = null;
        }
    }
}