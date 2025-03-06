using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
        Destroy(gameObject);
    }
} 