using System.Collections.Generic;
using System.Linq;
using PuzzleGames;
using UnityEngine;

namespace RepeatDrawingGame
{
    public class RepeatDrawingGame : PuzzleGame
    {
        [SerializeField] private List<TemplateCell> _templateCells; 
        [SerializeField] private List<PlayerCell> _playerCells; 
        
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _rightClickColor;

        [SerializeField] private int _rightCellsCount;
        
        private int _rightCellsOpened;
        private int _cellsOpened;

        private int _currentCellIndex;

        protected override void InitializeGame()
        {
            IsInitialized = true;
            GenerateField();
        }

        public override void UpdateGame()
        {
            if (_templateCells[_currentCellIndex].Id)
            {
                if (_playerCells[_currentCellIndex].Clicked)
                {
                    _playerCells[_currentCellIndex].SetColor(_rightClickColor);
                    _cellsOpened++;
                    _rightCellsOpened++;
                }
                else
                {
                    _playerCells[_currentCellIndex].SetColor(_defaultColor);
                    _cellsOpened--;
                    _rightCellsOpened--;
                }
            }
            
            if (IsGameOver())
            {
                FinishGame();
            } 
        }

        public override bool IsGameOver()
        {
            return _rightCellsOpened == _rightCellsCount && _cellsOpened == _rightCellsCount;
        }

        private void GenerateField()
        {
            var random = new System.Random();
            var idList = new List<bool>();

            for (int i = 0; i < _templateCells.Count; i++)
            {
                idList.Add(i < _rightCellsCount);
            }
            
            idList = idList.OrderBy(_ => random.Next()).ToList();
            
            for(int i = 0; i<_templateCells.Count; i++)
            {
                _templateCells[i].Id = idList[i];
                _templateCells[i].SetColor(idList[i] ? _rightClickColor : _defaultColor);

                _playerCells[i].CellClicked += OnCellClicked;

                _playerCells[i].Index = i;
                _playerCells[i].SetColor(_defaultColor);
            }
        }

        private void OnCellClicked(int cellIndex)
        {
            _currentCellIndex = cellIndex;
            UpdateGame();
        }
    }
}