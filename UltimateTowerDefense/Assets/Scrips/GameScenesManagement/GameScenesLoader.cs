using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSceneManangement 
{
    public enum GameScenes
    {
        TestingScene ,MainMenu, SingleRoadGame, DoubleRoadGame,TripleRoadGame,QuadRoadGame
    }    

    public static class GameScenesLoader
    {
        private static GameScenes currentScene = GameScenes.MainMenu;
        public static void LoadGameScene(GameScenes sceneToLoad)
        {
            currentScene = sceneToLoad;
            switch (sceneToLoad)
            {
                case GameScenes.TestingScene:
                    SceneManager.LoadScene(0);
                    break;
                case GameScenes.MainMenu:
                    SceneManager.LoadScene(1);
                    break;
                case GameScenes.SingleRoadGame:
                    break;
                case GameScenes.DoubleRoadGame:
                    break;
                case GameScenes.TripleRoadGame:
                    break;
                case GameScenes.QuadRoadGame:
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

