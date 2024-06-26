using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene,
    }

    private static Scene targetScene;
    public static void LoadScene(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallbac()
    {
        //Loads on the first frame of loading scene
        SceneManager.LoadScene(targetScene.ToString());
    }
}
