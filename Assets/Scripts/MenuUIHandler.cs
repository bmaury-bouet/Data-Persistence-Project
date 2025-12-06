#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        GlobalManager.Instance.SaveSettings();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif        
    }
}
