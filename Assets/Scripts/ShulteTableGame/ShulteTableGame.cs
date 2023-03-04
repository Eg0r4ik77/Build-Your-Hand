using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace ShulteTableGame
{
    public class ShulteTableGame : MonoBehaviour
    {
        [SerializeField] private List<Cell> _cells; 
        
        [SerializeField] private Color _color;
        [SerializeField] private Color _defaultColor;
        
        private int _currentCell = 1;
        
        public bool IsPlaying { get; private set; }

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Initialize()
        {
            IsPlaying = true;
            GenerateField();
        }

        private void UpdateGame(Cell cell)
        {
            if (cell.Id == _currentCell)
            {
                cell.ShowColor(_color);
                _currentCell++;
            }

            if (_currentCell == _cells.Count)
            {
                GameOver();
            }
        }

        public void FinishGame()
        {
            StartCoroutine(CloseGame(.1f));
        }
        
        private void GameOver()
        {
            StartCoroutine(CloseGame(1.5f));
        }

        private IEnumerator CloseGame(float time)
        {
            yield return new WaitForSeconds(time);
            IsPlaying = false;
            _currentCell = 0;
            gameObject.SetActive(false);
        }

        private void GenerateField()
        {
            var random = new System.Random();
            var _ids = new int[_cells.Count()];
        
            for (int i = 0; i < _cells.Count; i++)
            {
                _ids[i] = i + 1;
            }
            var idList = _ids.OrderBy(_ => random.Next()).ToList();
            
            for(int i = 0; i<_cells.Count; i++)
            {
                _cells[i].Id = idList[i];
                _cells[i].ShowColor(_defaultColor);
                _cells[i].SetId(idList[i]);
                _cells[i].CellClicked += UpdateGame;
            }
        }
    }
}

