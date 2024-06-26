using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    [Header("Scrips")]
    public UIManager uiManager;
    public Main main;


    [Header("Asignations")]
    string name;
    string email;
    string phone;

    [Header("InputFields")]
    public TMP_InputField nameInputField; 
    public TMP_InputField emailInputField;
    public TMP_InputField phoneInputField;

    [Header("Bool")]
    public bool registerExist;
    bool inputNull = true;

    [Header("Array")]
    TMP_InputField[] inputFields;

    
    void Start()
    {
        if(registerExist)
        {
            inputFields = new TMP_InputField[] {nameInputField, emailInputField, phoneInputField};

            foreach(TMP_InputField inputField in inputFields)
            {
                if(inputField != null)
                {
                    inputNull = false;
                    break;
                }
            }
        }
    }
    public void CheckRegister()
    {
        if(!registerExist)
        {
            uiManager.GoGame();
            return;
        }

        if(!inputNull)
        {
            //Si la variable esta declarada se le asigna el valor del inputField, si no se le asigna null
            name = nameInputField ? nameInputField.text : null;
            email = emailInputField ? emailInputField.text : null;
            phone = phoneInputField ? phoneInputField.text : null;

            Debug.Log("Checando...");

            if(nameInputField && string.IsNullOrEmpty(name))
                return;
            if(emailInputField && (string.IsNullOrEmpty(email) || !(email.Contains("@") && (email.Contains("gmail.com") || email.Contains("hotmail.com")))))
                return;
            if(phoneInputField && (string.IsNullOrEmpty(phone) || phone.Length != 10))
                return;

            //Siempre tener la instancia de la clase UsersLoad para poder guardar los datos
            Debug.Log("Datos correctos");
            UsersLoad user = main.UserDataInstance;
            user.name = name;
            user.email = email;
            user.phoneNumber = int.TryParse(phone, out int phoneNumber) ? phoneNumber : 0;
            
            uiManager.GoGame();
        }
    }
}