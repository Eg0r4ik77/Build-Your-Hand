using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NumberSequencesGame
{
    [Serializable]
    public class NumberSequence
    {
        [SerializeField] private List<TMP_Text> _texts;
        [SerializeField] private List<int> _numbers;
        [SerializeField] private int _missingNumberIndex;

        public int MissingNumber => _numbers[_missingNumberIndex];

        public void Initialize()
        {
            int index = 0;
            for (int i = 0; i < _numbers.Count; i++)
            {
                if (i != _missingNumberIndex)
                    _texts[index++].text = _numbers[i].ToString();
            }
        }
    }
}