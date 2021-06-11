using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUiController : MonoBehaviour
{
    public InputField PlayerNameInput;

    public void Start()
    {
        if (!string.IsNullOrEmpty(SharedSceneDataSingleton.Instance.PlayerName))
        {
            PlayerNameInput.text = SharedSceneDataSingleton.Instance.PlayerName;
        }
    }

    public void StartNew()
    {
        var playerName = "NewPlayer";
        //Save the player in the SharedSceneDataSingleton
        if (!string.IsNullOrEmpty(PlayerNameInput.text))
        {
            playerName = PlayerNameInput.text;
        }
        SharedSceneDataSingleton.Instance.PlayerName = playerName;
        SharedSceneDataSingleton.Instance.SavePersitentData();
        SceneManager.LoadScene(1);        
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif        
    }

}
