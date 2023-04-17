using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;
public static class GameLoader
{
    public enum Scene
    {
        UIMenu,
        WorldScene
    }

    public static void Load(Scene targetScene)
    {
        SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);
    }

    public static void LoadNetworkGame(string sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        
    }


}
