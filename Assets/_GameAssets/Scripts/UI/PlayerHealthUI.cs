using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image[] _playerHealthImages;
    [Header ("Sprites")]
    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnhealthySprite;
    [Header("Settings")]
    [SerializeField] private float _scaleDuration;
    private RectTransform[] _playerHealthTransorms;

    private void Awake()
    {
        _playerHealthTransorms = new RectTransform[_playerHealthImages.Length];
        for(int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransorms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }
    // FOR TESTING

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        
        }
        if(Input.GetKeyDown(KeyCode.P))

        AnimateDamageForAll();

    }

    public void AnimateDamage()
    {
        for(int i = 0; i < _playerHealthImages.Length; i++)
        {
            if(_playerHealthImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransorms[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for(int i = 0; i < _playerHealthImages.Length; i++)
        AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransorms[i]);
    }

    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealthySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }

}


