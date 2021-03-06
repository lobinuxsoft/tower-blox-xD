using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    [SerializeField] private IntVariable scoreVariable;
    [SerializeField] private IntVariable highScoreVariable;
    [SerializeField] private Transform pivot, buildingsContainer;
    [SerializeField] private TextMeshPro meterLabel;

    private LineRenderer lineRenderer;
    private int childAmount;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        meterLabel.text = "";
    }

    private void LateUpdate()
    {
        if (childAmount != buildingsContainer.childCount)
        {
            childAmount = buildingsContainer.childCount;

            lineRenderer.SetPosition(0, transform.position);
            

            UpdatePositionTask(1, buildingsContainer.GetChild(childAmount - 1).transform.position);
        }
        
        if(scoreVariable.GetValue() > highScoreVariable.GetValue()) highScoreVariable.SetValue(scoreVariable.GetValue());
    }
    
    private async void UpdatePositionTask(float duration, Vector3 endValue)
    {
        float time = 0;
        
        Vector3 startValue = pivot.position;
        
        endValue.x = startValue.x;
        endValue.z = startValue.z;

        while (time < duration)
        {
            pivot.position = Vector3.Lerp(startValue, endValue, time / duration);
            lineRenderer.SetPosition(1, pivot.position);
            scoreVariable.SetValue(Mathf.RoundToInt(Mathf.Lerp(scoreVariable.GetValue(), endValue.y * 100, time / duration)));
            meterLabel.text = $"{scoreVariable.GetValue():0}mts";
            time += Time.deltaTime;
            await Task.Yield();
        }
        pivot.position = endValue;
        lineRenderer.SetPosition(1, endValue);
    }
}
