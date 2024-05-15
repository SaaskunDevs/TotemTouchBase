using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [Header("Scripts")]
    public DBSend dbSend;
    public LeaderBoard leaderBoard;
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
