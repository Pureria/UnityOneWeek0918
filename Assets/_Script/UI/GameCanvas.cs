using MorseGame.Player;
using MorseGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private PlayerCanvas _pCanvas;
    [SerializeField] private MenuPopUP _StartPopup;

    public PlayerCanvas PlayerCanvas { get { return _pCanvas; } }

    public void GameStart()
    {
        _StartPopup.Hide();
    }
}
