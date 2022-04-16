using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HookControl : MonoBehaviour
{
    [SerializeField, Range(.01f, 1)] private float lineWidth = .25f;
    [SerializeField] private Rigidbody pivot;
    [SerializeField] private Rigidbody hook;
    [SerializeField] private float countDownIntancer = 1;
    [SerializeField] private Rigidbody[] buildings;
    [SerializeField] private Vector3 pivotOffset =  5 * Vector3.up;
    [SerializeField] private float rePositionDuration = 1;

    private Rigidbody buildingBody;
    private LineRenderer line;
    private float lastInstance = 0;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;

        transform.position = pivotOffset;
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
            buildingBody.constraints = RigidbodyConstraints.None;
            buildingBody.transform.SetParent(null);
            buildingBody = null;
            lastInstance = Time.time;
        }
    }
    
    private void InstanceBuilding()
    {
        Random.InitState((int)Time.time);
        
        if (!buildingBody && (Time.time - lastInstance) > countDownIntancer)
        {
            buildingBody = Instantiate<Rigidbody>(buildings[Random.Range(0, buildings.Length)], hook.transform, true);
            buildingBody.isKinematic = true;
            buildingBody.transform.localPosition = Vector3.zero;
        }
    }

    public void UpdatePivotPosition(Vector3 pos)
    {
        UpdatePositionTask(rePositionDuration, pos + pivotOffset);
    }

    private async void UpdatePositionTask(float duration, Vector3 endValue)
    {
        float time = 0;
        Vector3 startValue = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            await Task.Yield();
        }
        transform.position = endValue;
    }
}
