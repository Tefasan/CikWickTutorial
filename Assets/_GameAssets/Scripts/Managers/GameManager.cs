using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealtUI;
    
    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay;

    private GameState _currentGameState;

    private int _currentEggCount;
    private bool _isCatCatched;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HealtManager.Instance.OnPlayerDeath += HealtManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void CatController_OnCatCatched()
    {
        if(!_isCatCatched)
        {
             _playerHealtUI.AnimateDamageForAll();
        StartCoroutine(OnGameOver(true));
         CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f);
         _isCatCatched = true;
    }
        }

    private void HealtManager_OnPlayerDeath()
    {
         StartCoroutine(OnGameOver(false));
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
        BackgroundMusic.Instance.PlayBackgroundMusic(true);
    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);
    
        if(_currentEggCount == _maxEggCount)
        {
            //WIM
            _eggCounterUI.SetEggComleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }

      private IEnumerator OnGameOver(bool _isCatCatched)
        {
            yield return new WaitForSeconds(_delay);
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameLose();
            if(_isCatCatched)
            {
                AudioManager.Instance.Play(SoundType.CatSound);
            }
        }


    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
