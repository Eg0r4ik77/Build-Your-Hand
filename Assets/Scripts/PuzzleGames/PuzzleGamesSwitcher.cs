using System.Collections.Generic;
using System.Linq;
using Doors;
using UnityEngine;

namespace PuzzleGames
{
    public class PuzzleGamesSwitcher : MonoBehaviour
    {
        [SerializeField] private List<PuzzleGame> _puzzleGames;
        
        private List<HackableDoor> _hackableDoors;
        private PuzzleGame _currentGame;

        private void Awake()
        {
            _hackableDoors = FindObjectsOfType<HackableDoor>().ToList();
        }

        private void Start()
        {
            foreach (PuzzleGame puzzleGame in _puzzleGames)
            {
                puzzleGame.Finished += DeactivateCurrentGame;
                puzzleGame.Interrupted += DeactivateCurrentGame;
            }
            
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.GameStarted += ActivateCurrentGame;
            }
        }

        private void OnDestroy()
        {
            foreach (PuzzleGame puzzleGame in _puzzleGames)
            {
                puzzleGame.Finished -= DeactivateCurrentGame;
                puzzleGame.Interrupted -= DeactivateCurrentGame;
            }
            
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.GameStarted -= ActivateCurrentGame;
            }
        }

        private void ActivateCurrentGame(PuzzleGame game)
        {
            _currentGame = game;
            _currentGame.gameObject.SetActive(true);
            _currentGame.StartGame();
        }

        private void DeactivateCurrentGame()
        {
            _currentGame.gameObject.SetActive(false);
            _currentGame = null;
        }
    }
}