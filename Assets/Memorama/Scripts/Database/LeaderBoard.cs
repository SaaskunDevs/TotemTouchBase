using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private GameObject userPrefabGO;
    private UsersFromDB userData;
    [SerializeField] private Transform parentT;
    [SerializeField] LoadData loadData;

    void Start()
    {

    }

    public void ReciveLeader()
    {
        UsersLoad[] usersLoad = loadData.LoadDataFile();
        ClearInstances();
        UpdateUserDisplay(usersLoad);
    }

    void ClearInstances()
    {
        foreach (Transform item in parentT)
        {
            Destroy(item.gameObject);
        }
    }
    // Este método se encargará de asignar datos a cada prefab de usuario
    public void UpdateUserDisplay(UsersLoad[] usersLoad)
    {
        // Ordenar los datos por puntaje de mayor a menor
        if (usersLoad == null)
            return;

        System.Array.Sort(usersLoad, (a, b) =>
        {
            if (a == null || b == null)
            {
                return 0;
            }
            else
            {
                return a.scorePlayer.CompareTo(b.scorePlayer);
            }
        });

        // Iterar a través de los prefabs y asignar datos
        for (int i = 0; i < usersLoad.Length; i++)
        {
            GameObject userD = Instantiate(userPrefabGO, parentT);
            userData = userD.GetComponent<UsersFromDB>();
            userData.SetData(usersLoad[i].name, usersLoad[i].email, usersLoad[i].scorePlayer.ToString());

        }
    }

    public void DeleteDB()
    {
        loadData.DeleteFile();
        SceneManager.LoadScene(0);
    }
}