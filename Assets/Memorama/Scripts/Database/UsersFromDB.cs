using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class UsersFromDB : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameTxt;
    [SerializeField] private TextMeshProUGUI mailTxt;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    public void SetData(string username, string usermail, string score)
    {
        usernameTxt.text = username;
        mailTxt.text = usermail;
        scoreTxt.text = score;
    }
}
