using System;
using Audio;
using Serialization;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action onLoadNewScene;
    
    public void NewGame()
    {
        SceneLoader.Load(SceneLoader.Scene.TestScene);
        onLoadNewScene.Invoke();
        AudioManager.onNewGame.Invoke();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
