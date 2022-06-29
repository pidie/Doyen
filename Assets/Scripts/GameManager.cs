using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }
}
