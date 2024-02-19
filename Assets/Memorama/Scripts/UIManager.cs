using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]  GameObject menuUI;
    [SerializeField]  GameObject gameUI;
    [SerializeField]  GameObject winUI;
    [SerializeField]  GameObject[] fireWorks;
    [SerializeField]  GameObject cards;

    [Header("Scripts")]
    [SerializeField]  Main main;

    public void GoMenu()
    {
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        winUI.SetActive(false);
    }
    public void GoGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        winUI.SetActive(false);

        cards.SetActive(true);
        main.StartMemorama();
    }
    public void GoWin()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        winUI.SetActive(true);
        ActivateWinFireWorks();
    }

    void ActivateWinFireWorks()
    {
        foreach (var fireWork in fireWorks)
        {
            fireWork.SetActive(true);
        }
    }
    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }
}
