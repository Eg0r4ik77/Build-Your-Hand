﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PuzzleGames;
using UnityEngine;
using Random = System.Random;

namespace FindPairsGame
{
    public class FindPairsGame : PuzzleGame
    {
        [SerializeField] private List<Cell> _cells; 
        
        [SerializeField] private List<Color> _colors;
        [SerializeField] private Color _defaultColor;

        private const int OpeningPerMoveCount = 2;
        private const float CellHidingDurationInSeconds = 1.5f;
        
        private int _openedCellsCount;

        private List<Cell> _openedCellsPerMove;

        private void OnDisable()
        {
            if (_openedCellsPerMove.Count == OpeningPerMoveCount && !AreEqualOpenedCellsPerMove())
            {
                HideCells(_openedCellsPerMove);
                _openedCellsPerMove = new List<Cell>();
            }
        }

        protected override void InitializeGame()
        {
            IsInitialized = true;
            _openedCellsPerMove = new List<Cell>();
            GenerateField();
        }

        public override void UpdateGame()
        {
            if (_openedCellsPerMove.Count != OpeningPerMoveCount)
            { 
                return;         
            }

            if (AreEqualOpenedCellsPerMove())
            {
                _openedCellsCount += OpeningPerMoveCount;
            }
            else
            {
                StartCoroutine(HideCellsAfterMove(_openedCellsPerMove));
            }
            
            _openedCellsPerMove = new List<Cell>();

            if (IsGameOver())
            {
                FinishGame();
            }
        }

        public override bool IsGameOver()
        {
            return _openedCellsCount == _cells.Count;
        }

        private void OpenCell(Cell cell)
        {
            _openedCellsPerMove.Add(cell);
            cell.SetClickable(false);

            if (_openedCellsPerMove.Count == OpeningPerMoveCount)
            { 
                UpdateGame(); 
            }
        }

        private bool AreEqualOpenedCellsPerMove()
        {
            for(int i = 1; i < _openedCellsPerMove.Count; i++)
            {
                if (_openedCellsPerMove[i].Id != _openedCellsPerMove[i - 1].Id)
                {
                    return false;
                }
            }

            return true;
        }
        
        private void SetFieldClickable(bool clickable)
        {
            foreach (var cell in _cells)
            {
                cell.SetClickable(clickable);
            }
        }
        
        private IEnumerator HideCellsAfterMove(List<Cell> cells)
        {
            SetFieldClickable(false);
   
            yield return new WaitForSeconds(CellHidingDurationInSeconds);
            HideCells(cells);
            
            SetFieldClickable(true);
        }

        private void HideCells(List<Cell> cells)
        {
            foreach (Cell cell in cells)
            {
                cell.SetColor(_defaultColor);
            }
        }
        
        private void GenerateField()
        {
            var random = new Random();
            var idList = new List<int>();

            for (int i = 0; i < _colors.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    idList.Add(i);
                }
            }
            
            idList = idList.OrderBy(_ => random.Next()).ToList();
            
            for(int i = 0; i < _cells.Count; i++)
            {
                _cells[i].Initialize(idList[i], _colors[idList[i]]);
                _cells[i].SetColor(_defaultColor);
                _cells[i].Clicked += OpenCell;
            }
        }
    }
}