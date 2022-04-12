using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HookControl : MonoBehaviour
{
    [SerializeField, Range(.01f, 1)] private float lineWidth = .25f;
    [SerializeField] private Rigidbody pivot;
    [SerializeField] private Rigidbody hook;
    [SerializeField] private float countDownIntancer = 1;
    [SerializeField] private Rigidbody building;

    private Rigidbody buildingBody;
    private LineRenderer line;
    private float lastInstance = 0;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    private void LateUpdate()
    {
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        
        line.SetPosition(0, pivot.position);
        line.SetPosition(1, hook.position);

        if (Input.GetButtonDown("Jump"))
        {
            DetachBuilding();
        }
        
        InstanceBuilding();
    }

    private void DetachBuilding()
    {
        if (buildingBody != null)
        {
            buildingBody.isKinematic = false;
            buildingBody.transform.SetParent(null);
            buildingBody = null;
            lastInstance = Time.time;
        }
    }
    
    private void InstanceBuilding()
    {
        if (!buildingBody && (Time.time - lastInstance) > countDownIntancer)
        {
            buildingBody = Instantiate<Rigidbody>(building, hook.transform, true);
            buildingBody.isKinematic = true;
            buildingBody.transform.localPosition = Vector3.zero;
        }
    }
}
