using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DBSend : MonoBehaviour
{
    string savePath;
    string fileName;
    // Start is called before the first frame update
    void Start()
    {
        savePath = Application.persistentDataPath;
        fileName = "leaderboard" + ".csv";
        Debug.Log(Application.persistentDataPath);
        CreateIfNew();
    }


    void CreateIfNew()
    {
        if (!File.Exists(savePath + Path.DirectorySeparatorChar + fileName))
        {
            StreamWriter headerWriter = new StreamWriter(savePath + Path.DirectorySeparatorChar + fileName);
            headerWriter.WriteLine("Nombre,Correo,Puntaje"); // Encabezado de columnas
            headerWriter.Close();
            headerWriter.Close();
        }

    }
    public void WriteIntoDB(string name, string mail, float score)
    {
        if (File.Exists(savePath + Path.DirectorySeparatorChar + fileName))
            {
                Debug.Log("Si existe archivo en " + Application.persistentDataPath);


                StreamWriter writer = new StreamWriter(savePath + Path.DirectorySeparatorChar + fileName, true);

                writer.WriteLine(name + "," + mail + "," + score.ToString());

                writer.Flush();
                writer.Close();
            }
            else
            {
                Debug.LogError("No existe, error " + Application.persistentDataPath);
            }
    }
}