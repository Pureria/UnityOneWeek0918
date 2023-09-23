using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneChange : MonoBehaviour
{
    [SerializeField] private string SceneName = "";

    public void SceneChange()
    {
        SceneManager.LoadScene(SceneName);
    }
}
