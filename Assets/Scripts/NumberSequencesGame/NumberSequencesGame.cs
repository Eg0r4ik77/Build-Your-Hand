using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NumberSequencesGame
{
    public class NumberSequencesGame : MonoBehaviour
    {
        [SerializeField] private List<NumberSequence> _numberSequences;
        [SerializeField] private List<TMP_InputField> _inputFields;

        private void Start()
        {
            foreach (var inputField in _inputFields)
            {
                inputField.onValueChanged.AddListener((string _) => UpdateGame());
            }

            foreach (var numberSequence in _numberSequences)
            {
                numberSequence.Initialize();
            }
        }
        
        private void UpdateGame()
        {
            int count = 0;
            for (int i = 0; i < _inputFields.Count; i++)
            {
                string text = _inputFields[i].text;
                if (text.Length > 0 && _numberSequences[i].MissingNumber == int.Parse(text))
                {
                    count++;
                }
            }

            if (count == _inputFields.Count)
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

        private IEnumerator CloseGame(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}