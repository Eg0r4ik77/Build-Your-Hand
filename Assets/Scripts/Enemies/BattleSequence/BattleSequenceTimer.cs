using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Enemies.BattleSequence
{
    public class BattleSequenceTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private float _timeBetweenEnemyWavesInSeconds = 10f;

        private const string OriginText = "New wave will arrive in: ";

        public event Action Finished;
        
        public void StartTimer()
        {
            _tmpText.gameObject.SetActive(true);
            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            float timer = _timeBetweenEnemyWavesInSeconds;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateText((int)Math.Ceiling(timer));
                yield return null;
            }
            
            _tmpText.gameObject.SetActive(false);
            Finished?.Invoke();
        }

        private void UpdateText(int seconds)
        {
            _tmpText.text = OriginText + seconds;
        }
    }
}