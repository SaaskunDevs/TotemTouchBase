using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]  GameObject menuUI;
    [SerializeField]  GameObject instructionsUI;
    [SerializeField]  GameObject gameUI;
    [SerializeField]  GameObject winUI;
    [SerializeField]  GameObject[] fireWorks;
    [SerializeField]  GameObject cards;

    [Header("Scripts")]
    [SerializeField]  OrderIcons _orderIcons;

    [SerializeField] DBSend dbSend;
    [SerializeField] LeaderBoard leaderBoard;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] SoundsManager sounds;
    public void GoMenu()
    {
        menuUI.SetActive(true);
        instructionsUI.SetActive(false);
        gameUI.SetActive(false);
        winUI.SetActive(false);
    }

    public void GoInstructions()
    {
        menuUI.SetActive(false);
        instructionsUI.SetActive(true);
        gameUI.SetActive(false);
        winUI.SetActive(false);
    }

    public void GoGame()
    {
        menuUI.SetActive(false);
        instructionsUI.SetActive(false);
        gameUI.SetActive(true);
        winUI.SetActive(false);

        cards.SetActive(true);
        _orderIcons.StartMemorama();
    }

    public void GoWin(string timeToWin, float time)
    {
        scoreText.text = timeToWin;
        UserDataInstance.scorePlayer = time;
        Debug.Log("win");
        StartCoroutine(DelayWin());
    }

    IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(1);
        Win();
    }

    public void Win()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        winUI.SetActive(true);
        StoreData();
        sounds.Finish();

    }

    public void Loose()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        winUI.SetActive(true);

        cards.SetActive(false);
    }


    public void ShowInfoCanvas()
    {

    }

    public UsersLoad UserDataInstance = new UsersLoad();
    public void StoreData()
    {
        //UserData userData = checkResult.UserDataInstance;
        string name = UserDataInstance.name;
        string email = UserDataInstance.email;
        float scoreFF = UserDataInstance.scorePlayer;

        Debug.Log("Nombre: " + name + " Email: " + email + " Score: " + scoreFF);
        //nameText.text = name;
        //scoreText.text = scoreFF.ToString();
        dbSend.WriteIntoDB(name, email, scoreFF);
    }

    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }
}
