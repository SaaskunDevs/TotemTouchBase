using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LoadData : MonoBehaviour
{

    public UsersLoad[] usersLoad;
    string savePath;
    string fileName;
    private string date;


    // Start is called before the first frame update
    void Start()
    {
        savePath = Application.persistentDataPath;

        date = System.DateTime.Today.ToString("dd-MM-yyyy");
        fileName = "leaderboard" + ".csv";
        Debug.Log("Archivo a cargar: " + fileName);
    }


    public UsersLoad[] LoadDataFile()
    {
        if (!File.Exists(savePath + Path.DirectorySeparatorChar + fileName))
        {
            Debug.Log("No existe archivo en " + Application.persistentDataPath);
        }
        else
        {
            string data = File.ReadAllText(savePath + Path.DirectorySeparatorChar + fileName);
            string[] lines = data.Split('\n');

            Debug.Log("Numero de lineas " + lines.Length);

            usersLoad = new UsersLoad[lines.Length - 2];

            if(usersLoad.Length == 0)
            {
                Debug.Log("Aun no hay archivos");

                return null;
            }

                for (int i = 1; i < lines.Length -1 ; i++)
                {       
                    string[] lineData = lines[i].Split(',');
                    if (float.TryParse(lineData[2], out float score))
                    {
                        usersLoad[i-1] = new UsersLoad {name = lineData[0], email = lineData[1], scorePlayer =  score};
                    }
                    else
                    {
                        Debug.Log($"Failed to parse score from line {i}: {lineData[2]}");
                    }
                }
            
        }
        return usersLoad;
    }

    public void DeleteFile()
    {
        if (!File.Exists(savePath + Path.DirectorySeparatorChar + fileName))
        {
            Debug.Log("No existe archivo en " + Application.persistentDataPath);
        }
        else
        {
            File.Delete(savePath + Path.DirectorySeparatorChar + fileName);

        }
    }


}