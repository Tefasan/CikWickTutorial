using System;
using UnityEngine;

public class HealtManager : MonoBehaviour
{
     public static HealtManager Instance { get; private set;}
    public event Action OnPlayerDeath;
    [Header("References")]
    [SerializeField] private PlayerHealthUI _playerHealtUI;

    [Header("Settings")]
    [SerializeField] private int _maxHealth =3;
    private int _currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage (int damageAmount)
    {
        if(_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            _playerHealtUI.AnimateDamage();

            if(_currentHealth <=0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    
    }
        public void Heal(int heaLAmount)
        {
            if(_currentHealth < _maxHealth)
            {
                _currentHealth = Mathf.Min(_currentHealth + heaLAmount, _maxHealth);
            }
        }

        
    

}
