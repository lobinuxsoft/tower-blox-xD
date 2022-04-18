using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextColorPingPong : MonoBehaviour
{
    [SerializeField] Gradient gradient;
    [SerializeField] private float pingpongSpeed = 1f;
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMeshProUGUI.color = gradient.Evaluate(Mathf.PingPong(Time.time * pingpongSpeed, 1));
    }
}
