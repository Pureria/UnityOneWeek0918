using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace MorseGame.UI
{
    public class ChangeSoundVolume : MonoBehaviour
    {
        private static string BGMKEY = "BGMVolume - ";
        private static string SEKEY = "SEVolume - ";

        [SerializeField] AudioMixer audioMixer;
        [SerializeField] Slider bgmSlider;
        [SerializeField] Slider seSlider;

        void Start()
        {
            //�X���C�_�[�𓮂��������̏�����o�^
            bgmSlider.onValueChanged.AddListener(SetAudioMixerBGM);
            seSlider.onValueChanged.AddListener(SetAudioMixerSE);

            if (PlayerPrefs.HasKey(BGMKEY)) bgmSlider.value = PlayerPrefs.GetFloat(BGMKEY);
            else bgmSlider.value = bgmSlider.maxValue;

            if (PlayerPrefs.HasKey(SEKEY)) seSlider.value = PlayerPrefs.GetFloat(SEKEY);
            else seSlider.value = seSlider.maxValue;
        }

        //BGM
        public void SetAudioMixerBGM(float value)
        {
            PlayerPrefs.SetFloat(BGMKEY, value);

            //5�i�K�␳
            value /= 5;
            //-80~0�ɕϊ�
            float volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            //audioMixer�ɑ��
            audioMixer.SetFloat("BGM", volume);
            Debug.Log($"BGM:{volume}");
        }

        //SE
        public void SetAudioMixerSE(float value)
        {
            PlayerPrefs.SetFloat(SEKEY, value);

            //5�i�K�␳
            value /= 5;
            //-80~0�ɕϊ�
            float volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
            //audioMixer�ɑ��
            audioMixer.SetFloat("SE", volume);
            Debug.Log($"SE:{volume}");
        }
    }
}

