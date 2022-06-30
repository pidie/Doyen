using System;
using Audio;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action onLoadNewScene;
    
    public void NewGame()
    {
        SceneManager.LoadScene("TestScene");
        onLoadNewScene.Invoke();
        
        AudioManager.onNewGame.Invoke();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}
