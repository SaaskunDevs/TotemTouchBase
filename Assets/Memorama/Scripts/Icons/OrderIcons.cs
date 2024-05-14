using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class OrderIcons : MonoBehaviour
{
    [Header ("Panel")]
    public GameObject panel3D; // RectTransform para los iconos 3D
    
    [Header ("VariablesIcons")]
    public bool differentImgs = false;
    public float paddingX = 10f;
    public float paddingY = 10f;
    public float iconWidth = 1.5f;
    public float iconHeight = 1.5f;
    int size;

    //Arrays
    public Texture2D[] imgs;
    int[] randomArray; // Arreglo de enteros aleatorios
    public List<Icon> iconosList = new List<Icon>();
    public List<Vector3> positions = new List<Vector3>();
    //Arrays

    [Header ("PanelConfiguration")]
    public float rows;
    public float columns;
    public Vector3 panelPosition;
    int i=0;

    void Start()
    {
       StructureMemory();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConfiguratonGame();
        }
    }

    public void ConfiguratonGame()
    {
        switch (differentImgs)
        {
            case true:
                AreaCheck(imgs.Length);
                break;
            case false:
                AreaCheck(imgs.Length * 2);
                break;
        }
    }

    void AreaCheck(int size)
    {
        positions.Clear();
        
        this.size = size;
        Debug.Log("Tamaño: " + size);

        // Obtenemos el tamaño del panel
        float panelWidth = panel3D.GetComponent<Renderer>().bounds.size.x;
        float panelHeight = panel3D.GetComponent<Renderer>().bounds.size.y;

        // Inicializamos la posición en la esquina inferior izquierda del panel
        float startX = -panelWidth / 2 + iconWidth / 2;
        float startY = -panelHeight / 2 + iconHeight / 2;
        float currentX = startX;
        float currentY = startY;
        
        foreach (Icon icon in iconosList)
        {
            // Ajustamos la escala y la posición del icono
            icon.transform.localScale = new Vector3(iconWidth, iconHeight, 1);
            icon.transform.localPosition = new Vector3(currentX, currentY, 0);

            positions.Add(icon.transform.localPosition);

            // Actualizamos las coordenadas para el próximo icono
            currentX += iconWidth + paddingX;

            // Si hemos alcanzado el límite de iconos por fila, restablecemos las coordenadas para comenzar una nueva fila
            if ((i + 1) % columns == 0)
            {
                currentX = startX;
                currentY += iconHeight + paddingY;
            }
            i++;
        }

        // Calculamos el punto medio
        Vector3 midpoint = Vector3.zero;
        foreach (Vector3 pos in positions)
        {
            midpoint += pos;
        }
        midpoint /= positions.Count;

        // Centramos los objetos restando el punto medio de sus posiciones
        foreach (Transform child in panel3D.transform)
        {
            child.localPosition -= midpoint;
        }
        CreateIDs(size);
    }

    public void CreateIDs(int size)
    {
        if (differentImgs) 
            randomArray = CreateDiferentMemory(size);
        else 
            randomArray = CreateSimilarMemory(size);


        if (!differentImgs)
        {
            for (int i = 0; i < randomArray.Length; i++)
            {
                iconosList[i].SetImage(randomArray[i], imgs[randomArray[i] - 1]);
            }
        }
        else
        {
            for (int i = 0; i < randomArray.Length; i++)
            {

                if (randomArray[i] >= imgs.Length / 2)
                {

                    Debug.Log("index: " + randomArray[i] + " id final: " + (randomArray[i] - imgs.Length / 2));


                    iconosList[i].SetImage(randomArray[i] - imgs.Length / 2, imgs[randomArray[i]]);
                }
                else
                {
                    Debug.Log("index: " + randomArray[i] + " id final: " + randomArray[i]);
                    iconosList[i].SetImage(randomArray[i], imgs[randomArray[i]]);
                }
            }
        }
    }

    /// <summary>
    /// Array para imagenes pares diferentes
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    private int[] CreateDiferentMemory(int size)
    {
        int[] array = new int[size];
        List<int> availableNumbers = new List<int>();

        // Llenar la lista con los índices disponibles
        for (int i = 0; i < imgs.Length; i++)
        {
            availableNumbers.Add(i);
        }

        // Asignar un índice aleatorio a cada posición del array
        for (int i = 0; i < size; i++)
        {
            // Verificar si quedan índices disponibles
            if (availableNumbers.Count == 0)
            {
                Debug.LogError("No hay suficientes íconos disponibles para el tamaño especificado.");
                break;
            }

            int randomIndex = Random.Range(0, availableNumbers.Count);
            array[i] = availableNumbers[randomIndex];
            availableNumbers.RemoveAt(randomIndex);
        }

        return array;
    }

    /// <summary>
    /// Array para imagenes pares iguales
    /// </summary>
    private int[] CreateSimilarMemory(int size)
    {
        int[] array = new int[size];
        List<int> availableNumbers = new List<int>();

        for (int i = 1; i <= size / 2; i++)
        {
            availableNumbers.Add(i);
            availableNumbers.Add(i);
        }

        for (int i = 0; i < size; i++)
        {
            int randomIndex = Random.Range(0, availableNumbers.Count);
            array[i] = availableNumbers[randomIndex];
            availableNumbers.RemoveAt(randomIndex);
        }

        return array;
    }

    public void StructureMemory()
    {
        panel3D.transform.position = panelPosition;
        ConfiguratonGame();
    }
}