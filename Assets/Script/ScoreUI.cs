using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private string prevText = "Score: ";
    [SerializeField] private IntVariable intVariable;
    private TextMeshProUGUI scoreLabel;

    private void Awake()
    {
        scoreLabel = GetComponent<TextMeshProUGUI>();
        scoreLabel.text = $"{prevText}{intVariable.GetValue()}";
        intVariable.onValueChange += OnValueChnage;
    }

    private void OnDestroy()
    {
        intVariable.onValueChange -= OnValueChnage;
    }

    private void OnValueChnage(int value)
    {
        scoreLabel.text = $"{prevText}{value}";
    }
}
