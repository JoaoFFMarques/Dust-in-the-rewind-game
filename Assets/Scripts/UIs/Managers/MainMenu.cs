using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("level");
        ScreenManager.Instance.LoadLevelLoading("Gameplay");
    }

    public void Continue()
    {
        ScreenManager.Instance.LoadLevelLoading("Gameplay");
    }

    public void Credits()
    {
        ScreenManager.Instance.LoadLevel("Credits");
    }

    public void Settings()
    {
        ScreenManager.Instance.LoadLevel("Settings");
    }

    public void Quit()
    {
        ScreenManager.Instance.QuitGame();
    }


}
