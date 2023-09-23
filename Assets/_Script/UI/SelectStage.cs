using MorseGame.Map;
using MorseGame.StartUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MorseGame.UI
{
    public class SelectStage : MonoBehaviour
    {
        [SerializeField] StageSelecter _StageSelecter;
        [SerializeField] StartCanvas _StartCanvas;
        public void Selectmap(StageData data)
        {
            if(data == null)
            {
                Debug.LogError("マップデータが入っていません");
                return;
            }

            _StageSelecter.SaveStageFile(data);
            _StartCanvas.HideUIAndMoveGame();
            //_StartCanvas.DebugChangeScene();
            //SceneManager.LoadScene("DebugGameScene");
        }
    }
}
