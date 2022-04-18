using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class SceneChangeUI : MonoBehaviour
{
    [SerializeField] private string sceneToSwitchName;

    private Button button;
    
    void Start ()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadNext);
    }
    
    void OnDestroy ()
    {
        button.onClick.RemoveListener(LoadNext);
    }
    

    private void LoadNext ()
    {
        SceneManager.LoadScene(sceneToSwitchName);
    }
}
