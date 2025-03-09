using UnityEngine;
using UnityEngine.XR;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState _currentState = CatState.Walking;

    private void Start()
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newState)
    {
        if(_currentState == newState) { return; }
        _currentState = newState;
    }

    public CatState GetCurrentState()
    {
        return _currentState;
    }
}
