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
        [SerializeField] private string _StageSceneName = "";
        public void Selectmap(StageData data)
        {
            if(data == null)
            {
                Debug.LogError("マップデータが入っていません");
                return;
            }

            _StageSelecter.SaveStageFile(data.GetStageBinaryData());
            _StartCanvas.HideUI(ChangeGameScene);
        }

        private void ChangeGameScene()
        {
            SceneManager.LoadScene(_StageSceneName);
        }
    }
}
