using System.Collections.Generic;
using System.Linq;
using PuzzleGames;
using UnityEngine;

namespace ShulteTableGame
{
    public class ShulteTableGame : PuzzleGame
    {
        [SerializeField] private List<Cell> _cells; 
        
        [SerializeField] private Color _color;
        [SerializeField] private Color _defaultColor;
        
        private int _currentCellNumber;

        public override void InitializeGame()
        {
            IsInitialized = true;
            _currentCellNumber = 1;
            GenerateField();
        }

        public override void StartGame()
        {
            if (!IsInitialized)
            { 
                InitializeGame();
            }
        }

        public override void UpdateGame()
        {
            if (IsGameOver())
            {
                FinishGame();
            }
        }

        public override bool IsGameOver()
        {
            return _currentCellNumber > _cells.Count;
        }

        private void GenerateField()
        {
            var random = new System.Random();
            var idList = new List<int>();
        
            for (int i = 0; i < _cells.Count; i++)
            {
                idList.Add(i + 1);
            }
            
            idList = idList.OrderBy(_ => random.Next()).ToList();
            
            for(int i = 0; i<_cells.Count; i++)
            {
                _cells[i].SetColor(_defaultColor);
                _cells[i].SetId(idList[i]);
                
                _cells[i].CellClicked += OnCellClicked;
            }
        }

        private void OnCellClicked(Cell cell)
        {
            if (cell.Id == _currentCellNumber)
            {
                _currentCellNumber++;
                cell.SetColor(_color);
            }
            UpdateGame();
        }
    }
}

