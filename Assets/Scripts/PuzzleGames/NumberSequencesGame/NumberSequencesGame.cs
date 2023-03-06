using System.Collections.Generic;
using PuzzleGames;
using TMPro;
using UnityEngine;

namespace NumberSequencesGame
{
    public class NumberSequencesGame : PuzzleGame
    {
        [SerializeField] private List<NumberSequence> _numberSequences;
        [SerializeField] private List<TMP_InputField> _inputFields;
        
        public override void InitializeGame()
        {
            IsInitialized = true;
            
            foreach (var inputField in _inputFields)
            {
                inputField.onValueChanged.AddListener(_ => UpdateGame());
            }
            
            foreach (var numberSequence in _numberSequences)
            {
                numberSequence.Initialize();
            }
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
            int rightAnswersCount = 0;
            
            for (int i = 0; i < _inputFields.Count; i++)
            {
                string textAnswer = _inputFields[i].text;
                if (textAnswer.Length > 0 && _numberSequences[i].MissingNumber == int.Parse(textAnswer))
                {
                    rightAnswersCount++;
                }
            }
            
            return rightAnswersCount == _inputFields.Count;
        }
    }
}