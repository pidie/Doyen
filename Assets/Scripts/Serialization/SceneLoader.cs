using System;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Serialization
{
    public static class SceneLoader
    {
        public enum Scene
        {
            OpeningMenu,
            LoadingScreen,
            TestScene,
        }

        private static Action _onLoaderCallback;
        public static Action onAssignReferences;
        public static Action onSetUIElements;

        public static void Load(Scene scene)
        {
            // store the scene we want to load
            _onLoaderCallback = () => SceneManager.LoadScene(scene.ToString());

            // start loading the "Loading..." screen
            SceneManager.LoadScene(Scene.LoadingScreen.ToString());
        }

        // this method is called by the SceneLoaderCallback script
        public static void LoaderCallback()
        {
            if (_onLoaderCallback != null)
            {
                _onLoaderCallback.Invoke();
                _onLoaderCallback = null;
            }
        }
    }
}