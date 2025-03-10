using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set;}
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;


private void Awake() 
{
    Instance = this;
    _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
}
private IEnumerator CameraShakeCoroutine(float intensity, float time, float deLay)
{
    yield return new WaitForSeconds(deLay);
    _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
    _shakeTimer = time;
    _shakeTimerTotal = time;
    _startingIntensity = intensity;
}

    public void ShakeCamera(float intensity, float time, float deLay = 0f)
    {
        StartCoroutine(CameraShakeCoroutine(intensity, time, deLay));
    }

    private void Update()
    {
        if(_shakeTimer > 0f)
            {
                _shakeTimer -= Time.deltaTime;
                
                if(_shakeTimer<= 0f)
                {
                    _cinemachineBasicMultiChannelPerlin.AmplitudeGain
                    = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
                }
            }   
    }
}
