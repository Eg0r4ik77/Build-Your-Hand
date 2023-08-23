using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScenario : MonoBehaviour
{
    [SerializeField] private GlobalBlackboard _enemyGlobalBlackboard;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        _player.Died += RestartGame;
    }

    private void OnDestroy()
    {
        _player.Died -= RestartGame;
    }

    private void RestartGame()
    {
        Destroy(_enemyGlobalBlackboard.gameObject);
        
        int activeSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneBuildIndex);
    }
}