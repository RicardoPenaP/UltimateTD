using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSceneManangement 
{
    public enum GameScenes
    {
        MainMenu, GameScene
    }    

    public static class GameScenesLoader
    {
        private static GameScenes currentScene = GameScenes.MainMenu;
        public static GameScenes CurrenScene { get { return currentScene; } }
        public static void LoadGameScene(GameScenes sceneToLoad)
        {
            currentScene = sceneToLoad;
            switch (sceneToLoad)
            {               
                case GameScenes.MainMenu:
                    SceneManager.LoadScene(0);
                    break;
                case GameScenes.GameScene:
                    SceneManager.LoadScene(1);
                    break;
               
                default:
                    Debug.Log("Scene Not found");
                    break;
            }
        }

        public static void ReloadCurrentScene()
        {
            LoadGameScene(currentScene);
        }
    }

}

