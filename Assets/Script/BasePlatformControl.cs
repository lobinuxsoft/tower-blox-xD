using System;
using UnityEngine;
using UnityEngine.Events;

public class BasePlatformControl : MonoBehaviour
{
    private BoxCollider boxCollider;
    
    public UnityEvent<Vector3> onBuildingReachPlatform;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Vector3.Dot(collision.contacts[0].point, transform.up) <= 0 ) return;
        
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
            rigidbody.transform.SetParent(transform, true);

            Vector3 newPosition = rigidbody.transform.localPosition;
            newPosition.y += rigidbody.transform.localScale.y / 2;
            
            boxCollider.center = newPosition;

            onBuildingReachPlatform?.Invoke(transform.GetChild(transform.childCount-1).position);
        }
    }
}
