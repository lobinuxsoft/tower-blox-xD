using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private string tagID;
    [SerializeField] private IntVariable lives;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagID))
        {
            lives.AddValue(-1);
        }
    }
}
