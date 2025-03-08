 using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playerVisualTransform;

    [Header("Settings")]

    [SerializeField] private float _rotationSpeed;


    private void Update()
    {
        if(GameManager.Instance.GetCurrentGameState() != GameState.Play
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
            {
                return;
            }
        Vector3 viewDirection
            = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientationTransform.forward = viewDirection.normalized;

        float _horizontalInput = Input.GetAxisRaw("Horizontal");
        float _verticalTnput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = _orientationTransform.forward * _verticalTnput + _orientationTransform.right * _horizontalInput;

        if(inputDirection != Vector3.zero)
        {
            _playerVisualTransform.forward
            =Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
       
     }
}
