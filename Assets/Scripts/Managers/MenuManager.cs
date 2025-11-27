using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public GameObject[] gameObjects;
    public void StartGame(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void Open(int number)
    {
        gameObjects[number].SetActive(true);
    }
    public void Close(int number)
    {
        gameObjects[number].SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
