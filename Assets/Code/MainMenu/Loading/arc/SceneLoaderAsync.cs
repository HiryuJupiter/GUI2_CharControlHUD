﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace MyNameSpace
{
    public class SceneLoaderAsync : MonoBehaviour
    {
        // Loading Progress: setter, public getter
        float _loadingProgress;
        public float LoadingProgress { get { return _loadingProgress; } }

        public void LoadScene()
        {
            // kick-off the one co-routine to rule them all
            StartCoroutine(LoadScenesInOrder());
        }

        IEnumerator LoadScenesInOrder()
        {
            // LoadSceneAsync() returns an AsyncOperation, 
            // so will only continue past this point when the Operation has finished
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Loading");

            //What is the nature of Asyncoperation?

            // as soon as we've finished loading the loading screen, start loading the game scene
            yield return StartCoroutine(LoadScene("Game"));
        }

        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            // this value stops the scene from displaying when it's finished loading
            asyncScene.allowSceneActivation = false;

            while (!asyncScene.isDone)
            {
                // loading bar progress
                _loadingProgress = Mathf.Clamp01(asyncScene.progress / 0.9f) * 100;

                // scene has loaded as much as possible, the last 10% can't be multi-threaded
                if (asyncScene.progress >= 0.9f)
                {
                    // we finally show the scene
                    asyncScene.allowSceneActivation = true;
                }

                yield return null;
            }
        }

    }
}