using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RepeatDrawingGame
{
    public class RepeatDrawingGame : MonoBehaviour
    {
        [SerializeField] private List<Cell> _templateCells; 
        [SerializeField] private List<Cell> _playerCells; 
        
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _trueColor;

        [SerializeField] private int _trueCellsCount;
        
        private int _trueCellsOpened;
        private int _cellsOpened;

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
            GenerateField();
        }

        private void UpdateGame(int index)
        {
            _playerCells[index].Clicked = !_playerCells[index].Clicked;
            if (!_playerCells[index].Clicked)
            {
                _playerCells[index].SetColor(_defaultColor);
                _cellsOpened--;
                if (_templateCells[index].Id)
                {
                    _trueCellsOpened--;
                }
            }
            else
            {
                 _playerCells[index].SetColor(_trueColor);
                _cellsOpened++;
                if (_templateCells[index].Id)
                {
                    _trueCellsOpened++;
                }
            }
            
            if (_trueCellsOpened == _trueCellsCount && _cellsOpened == _trueCellsCount)
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
            StartCoroutine(CloseGame(.8f));
        }

        public IEnumerator CloseGame(float time)
        {
            yield return new WaitForSeconds(time);
            _trueCellsOpened = 0;
            gameObject.SetActive(false);
        }


        private void GenerateField()
        {
            var random = new System.Random();
            var _ids = new bool[_templateCells.Count];

            for (int i = 0; i < _ids.Length; i++)
            {
                if (i < _trueCellsCount)
                    _ids[i] = true;
                else
                    _ids[i] = false;
            }
            var idList = _ids.OrderBy(_ => random.Next()).ToList();
            
            for(int i = 0; i<_templateCells.Count; i++)
            {
                _templateCells[i].Id = idList[i];

                _templateCells[i].SetColor(idList[i] ? _trueColor : _defaultColor);

                _playerCells[i].CellClicked += UpdateGame;

                _playerCells[i].Index = i;
                _playerCells[i].SetColor(_defaultColor);
            }
        }
    }
}