using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigationController : MonoBehaviour
{
    [SerializeField]
    SoundHandler soundHandler; // I wanna play sounds and you can't stop me

    enum SceneMapping : int
    {
        MainMenuScene = 0,
        GameScene
    }

    static SceneMapping ActiveSceneMapping { get; set; }
    public static int ActiveStage { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void NavigateToMainMenu()
    {
        if (ActiveSceneMapping != SceneMapping.MainMenuScene)
        {
            // TODO stop music/sounds if going

            ActiveSceneMapping = SceneMapping.MainMenuScene;
            ActiveStage = 0;

            SceneManager.LoadScene((int)SceneMapping.MainMenuScene, LoadSceneMode.Single);
        }
    }

    public void NavigateToLevel(int stage)
    {
        // TODO if stage number is same as current, it's a restart of the current level

        // TODO stop/blend music/sounds if going
        soundHandler.PlayJukebox(SoundHandler.JukeboxSong.ConfirmMenu);

        ActiveSceneMapping = SceneMapping.GameScene;
        ActiveStage = stage;

        SceneManager.LoadScene((int)SceneMapping.GameScene, LoadSceneMode.Single);
    }

    public void PlayEpicDoot()
    {
        // Coming soon
        // use soundHandler
        soundHandler.PlayJukebox(SoundHandler.JukeboxSong.DootDoot);
    }

    public void ExitGame()
    {
        soundHandler.PlayJukebox(SoundHandler.JukeboxSong.Exit);
        Application.Quit();
    }
}
