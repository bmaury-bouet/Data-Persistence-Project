#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEditor.UI;

#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Button UserNameOkButton;
    public Button UserNameCancelButton;
    public TMP_InputField UserNameTextField;

    public void StartNewGame_Click()
    {        
        EnableUserNameInputMenu(true);
    }

    public void UserNameCancelButton_Click()
    {
        EnableUserNameInputMenu(false);
    }

    public void UserNameOkButton_Click()
    {
        string userName = UserNameTextField.text;
        if (string.IsNullOrEmpty(userName))
        {
            //TODO:report error
            return;
        }
        //TODO: Validate if the user exists and if it does load previous save, if not report error
        //TODO: Create user game save
        GlobalManager.Instance.SaveSettings(userName,0);
        SceneManager.LoadScene(1);
    }

    private void EnableUserNameInputMenu(bool enable)
    {
        UserNameOkButton.gameObject.SetActive(enable);
        UserNameCancelButton.gameObject.SetActive(enable);
        UserNameTextField.gameObject.SetActive(enable);
    }
    
    public void ExitGame()
    {
        GlobalManager.Instance.SaveSettings(GlobalManager.Instance.PlayerName, GlobalManager.Instance.PlayerScore);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif        
    }
}
