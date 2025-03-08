using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        // Collider'ı kapat (Artık çarpışma algılanmaz)
        GetComponent<Collider>().enabled = false;

        // Yumurtayı GameManager'a bildir
        GameManager.Instance.OnEggCollected();

        // Yumurtayı yok et
        Destroy(gameObject);
    }
}