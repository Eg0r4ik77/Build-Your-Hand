using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace NumberSequencesGame
{
    [Serializable]
    public class NumberSequence
    {
        [SerializeField] private List<TMP_Text> _texts;
        [SerializeField] private List<int> _numbers;
        [SerializeField] private int _missingNumber;

        public int MissingNumber => _missingNumber;

        public void Initialize()
        {
            int missingNumberIndex = _numbers.IndexOf(_missingNumber);
            int textIndex = 0;
            
            for (int i = 0; i < _numbers.Count; i++)
            {
                if (i != missingNumberIndex)
                {
                    _texts[textIndex++].text = _numbers[i].ToString();
                }
            }
        }
    }
}