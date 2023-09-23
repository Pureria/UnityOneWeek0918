using MorseGame.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataContainer : MonoBehaviour
{
    public static StageDataContainer Instance;

    public StageData CurrentStageData = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
