using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private float _force = 10f;

    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisuaLTransform)
    {
        HealtManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisuaLTransform.forward * _force, ForceMode.Impulse);
        AudioManager.Instance.Play(SoundType.ChickSound);
        Destroy(gameObject);
    }
}
