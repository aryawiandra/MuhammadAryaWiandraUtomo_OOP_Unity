using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    
    // Buat mount offset bisa diedit di inspector dengan label yang jelas
    [Header("Mounting Configuration")]
    [Tooltip("X: Kiri/Kanan, Y: Atas/Bawah")]
    [SerializeField] private Vector2 mountOffset = Vector2.zero;
    
    // Tambahkan opsi rotasi jika diperlukan
    [Tooltip("Rotasi weapon dalam derajat")]
    [SerializeField] private float mountRotation = 0f;
    
    // Tambahkan opsi scale
    [Tooltip("Skala weapon")]
    [SerializeField] private Vector2 mountScale = Vector2.one;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (weaponPrefab == null || !other.CompareTag("Player")) return;

        GameObject weaponInstance = Instantiate(weaponPrefab, other.transform.position, Quaternion.identity);
        
        if (weaponInstance != null)
        {
            weaponInstance.transform.SetParent(other.transform);
            
            // Terapkan semua konfigurasi mounting
            weaponInstance.transform.localPosition = mountOffset;
            weaponInstance.transform.localRotation = Quaternion.Euler(0, 0, mountRotation);
            weaponInstance.transform.localScale = mountScale;
            
            Debug.Log($"Weapon mounted at: Position={mountOffset}, Rotation={mountRotation}");
        }
        
        Destroy(gameObject);
    }

    // Visual helper di editor untuk melihat posisi mounting
    private void OnDrawGizmosSelected()
    {
        // Gambar garis dari pickup ke posisi mounting
        Gizmos.color = Color.green;
        Vector3 mountPoint = transform.position + (Vector3)mountOffset;
        Gizmos.DrawLine(transform.position, mountPoint);
        Gizmos.DrawWireSphere(mountPoint, 0.1f);
        
        // Tampilkan arah rotasi
        if (mountRotation != 0)
        {
            Gizmos.color = Color.blue;
            Vector3 direction = Quaternion.Euler(0, 0, mountRotation) * Vector3.right * 0.5f;
            Gizmos.DrawRay(mountPoint, direction);
        }
    }
}