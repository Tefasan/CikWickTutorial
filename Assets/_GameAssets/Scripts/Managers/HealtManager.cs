using UnityEngine;

public class HealtManager : MonoBehaviour
{
 [SerializeField] private int _maxHealth =3;

 private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage (int damageAmount)
    {
        if(_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
            //TODO: UI ANİMATE DAMAGE

            if(_currentHealth <=0)
            {
                //TODO: PLAYER DEAD
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
