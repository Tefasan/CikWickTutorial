using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        // Collider'ı kapat (Artık çarpışma algılanmaz)
        GetComponent<Collider>().enabled = false;
         CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        // Yumurtayı GameManager'a bildir
        GameManager.Instance.OnEggCollected();
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        // Yumurtayı yok et
        Destroy(gameObject);
    }
}