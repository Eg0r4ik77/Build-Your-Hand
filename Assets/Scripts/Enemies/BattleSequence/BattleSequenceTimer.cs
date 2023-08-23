using System;
using TMPro;
using UnityEngine;

namespace Enemies.BattleSequence
{
    public class BattleSequenceTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private float _timeBetweenEnemyWavesInSeconds = 10f;

        private const string OriginText = "New wave will arrive in: ";
        
        private bool _paused = true;
        private bool _started = false;
        
        private float _timeLeft;
        
        public event Action Finished;

        private void OnEnable()
        {
            Pause.Instance.OnPaused += SetPaused;
        }

        private void OnDisable()
        {
            Pause.Instance.OnPaused -= SetPaused;
        }

        private void Update()
        {
            if (!_paused && _started)
            {
                Tick();
            }
        }

        public void StartTimer()
        {
            _tmpText.gameObject.SetActive(true);
            _timeLeft = _timeBetweenEnemyWavesInSeconds;
            _started = true;
            _paused = false;
        }

        private void Tick()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateText((int)Math.Ceiling(_timeLeft));
            }
            else
            {
                _tmpText.gameObject.SetActive(false);
                Finished?.Invoke();
                _paused = true;
                _started = false;
            }
        }
        
        private void UpdateText(int seconds)
        {
            _tmpText.text = OriginText + seconds;
        }
        
        private void SetPaused(bool paused)
        {
            _paused = paused;
        }
    }
}