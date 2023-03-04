using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace FindPairsGame
{
    public class FindPairsGame : MonoBehaviour
    {
        [SerializeField] private List<Cell> _cells; 
        
        [SerializeField] private List<Color> _colors;
        [SerializeField] private Color _defaultColor;

        private int _moves;
        private int _cellsOpened;

        private Cell _firstCell;
        private Cell _secondCell;
        
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
            
            //если корутина уже запущена, надо прервать, но перевернуть ячейки
            StopCoroutine(HideCells());
        }

        private void Initialize()
        {
            IsPlaying = true;
            GenerateField();
        }

        private void UpdateGame(Cell cell)
        {
            _moves++;
            if (_moves == 1)
                _firstCell = cell;
            if (_moves == 2)
            {
                _secondCell = cell;

                if (_firstCell.Id == _secondCell.Id)
                {
                    _cellsOpened += 2;
                    if (_cellsOpened == _cells.Count)
                    {
                        GameOver();
                    } 
                }
                else
                {
                    StartCoroutine(HideCells());
                }

                _moves = 0;
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

        public IEnumerator CloseGame(float time)
        {
            yield return new WaitForSeconds(time);
            IsPlaying = false;
            _cellsOpened = 0;
            gameObject.SetActive(false);
        }

        private void SetField(bool state)
        {
            foreach (var cell in _cells)
            {
                cell.SetClickable(state);
            }
        }
        
        private IEnumerator HideCells()
        {
            SetField(false);
            yield return new WaitForSeconds(1.5f);
            _firstCell.ShowColor(_defaultColor);
            _secondCell.ShowColor(_defaultColor);
            SetField(true);
        }
        
        
        private void GenerateField()
        {
            var random = new Random();
            var _ids = new int[_cells.Count()];

            for (int i = 0; i < _colors.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    _ids[i * 2 + j] = i;
                }
            }
            var idList = _ids.OrderBy(_ => random.Next()).ToList();
            
            for(int i = 0; i<_cells.Count; i++)
            {
                _cells[i].Id = idList[i];
                _cells[i].CellColor = _colors[idList[i]];
                _cells[i].ShowColor(_defaultColor);

                _cells[i].CellClicked += UpdateGame;
            }
        }
    }
}