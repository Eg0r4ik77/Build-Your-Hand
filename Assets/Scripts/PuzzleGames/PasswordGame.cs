using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PuzzleGames
{
    public class PasswordGame : PuzzleGame
    {
        [SerializeField] private List<TMP_InputField> _inputFields;
        [SerializeField] private List<int> _missedNumbers;

        protected override void InitializeGame()
        {
            IsInitialized = true;
            
            foreach (TMP_InputField inputField in _inputFields)
            {
                inputField.onValueChanged.AddListener(_ => UpdateGame());
                inputField.onValueChanged.AddListener(_ => TrySelectNextInputField(inputField));
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            TrySelectFirstFreeInputField();
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
            bool isRightPassword = true;
            
            for (int i = 0; i < _inputFields.Count; i++)
            {
                string textAnswer = _inputFields[i].text;
                
                if (textAnswer.Length == 0 || int.Parse(textAnswer) != _missedNumbers[i])
                {
                    isRightPassword = false;
                }
            }
            
            return isRightPassword;
        }

        private void TrySelectNextInputField(TMP_InputField inputField)
        {
            int index = _inputFields.IndexOf(inputField);

            if (index < _inputFields.Count - 1 && inputField.text.Length > 0)
            {
                _inputFields[index + 1].Select();
            }
        }

        private void TrySelectFirstFreeInputField()
        {
            foreach (TMP_InputField inputField in _inputFields)
            {
                if (inputField.text.Length == 0)
                {
                    inputField.Select();
                    return;
                }
            }
        }
    }
}