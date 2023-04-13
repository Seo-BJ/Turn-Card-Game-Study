using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [FormerlySerializedAs("_gameStartButton")] [SerializeField] Button gameStartButton;

    void Start()
    {
        gameStartButton.onClick.AddListener(GameStart);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Map");
    }
}
