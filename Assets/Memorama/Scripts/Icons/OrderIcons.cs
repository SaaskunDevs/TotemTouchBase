using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class OrderIcons : MonoBehaviour
{
    [Header ("Panel")]
    public GameObject panel3D; // RectTransform para los iconos 3D
    
    [Header ("VariablesIcons")]
    public bool differentImgs = false; // Variable para saber si las imagenes son diferentes o iguales
    public float paddingX = 10f; // Espaciado horizontal entre los iconos
    public float paddingY = 10f; // Espaciado vertical entre los iconos
    public float iconWidth = 1.5f; // Ancho de los iconos
    public float iconHeight = 1.5f; // Alto de los iconos
    int size; // Tamaño de la lista de imagenes para asiganlas a los iconos

    //Arrays
    public Texture2D[] imgs; // Arreglo de texturas para los iconos
    int[] randomArray; // Arreglo de enteros aleatorios
    public List<Icon> iconosList = new List<Icon>(); // Lista de iconos
    public List<Vector3> positions = new List<Vector3>(); // Lista de posiciones de los iconos
    //Arrays

    [Header ("PanelConfiguration")]
    public float rows; // Filas del panel
    public float columns; // Columnas del panel
    public Vector3 panelPosition; // Posición del panel en el mundo
    int i=0; // Contador para el foreach de los iconos

    [Header ("Game")]
    private bool started; // Variable para saber si el juego ha comenzado
    private bool waitForCheck; // Variable para saber si se está esperando a comprobar los ítems seleccionados
    public int selected; // Variable para saber cuántos ítems se han seleccionado
    private Icon item1, item2; // Variables para guardar los ítems seleccionados

    [Header ("Sounds")]
    public SoundsManager sounds;
    
    [Header ("Scripts")]
    public UIManager uiManager;

    [Header ("Score")]
    public int timeToPlay = 60; // Tiempo en segundos
    public TextMeshProUGUI timeText; // Texto para mostrar el tiempo restante
    private float timeLeft; // Tiempo restante en segundos
    public TextMeshProUGUI scoreTxt; // Texto para mostrar el score
    public TextMeshProUGUI Finalscore;
    private int score, sizeBegin;

    void Start()
    {
       StructureMemory();
       uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if (started)
        {
            CheckTouches();
            UpdateTimeText();
        }
    }
    public void StartMemorama()
    {
        timeLeft = timeToPlay;
        started = true;
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

    /// <summary>
    /// Método para ajustar la posición de los iconos en el panel
    /// </summary>
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

    /// <summary>
    /// Método para crear los IDs de los iconos
    /// </summary>
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

    /// <summary>
    /// Método para reorganizar los iconos
    /// </summary>
    public void StructureMemory()
    {
        panel3D.transform.position = panelPosition;
        ConfiguratonGame();
    }

                                                    //////////////////////// Estructura del Juego ////////////////////////

    /// <summary>
    /// Método para verificar los toques a los iconos
    /// </summary>
    void CheckTouches()
    {
        if (waitForCheck)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity);

            if (hit.collider != null)
            {
                Icon icn = hit.collider.GetComponent<Icon>();

                if (icn.CanInteract())
                {

                    if (selected == 0)
                    {
                        item1 = icn;
                        icn.UserClick();
                        selected++;
                    }
                    else if (selected == 1)
                    {
                        item2 = icn;
                        icn.UserClick();
                        selected++;
                    }
                }
                
            }
        }
    }

    /// <summary>
    /// Método para verificar los ítems seleccionados
    /// </summary>
    public void CheckItems()
    {
        if (waitForCheck)
            return;

        if (selected != 2)
            return;
        waitForCheck = true;
        Debug.Log("Check items");
        StartCoroutine(DelayToCheck());
    }

    /// <summary>
    /// Método donde comprobamos si los ítems seleccionados son iguales
    /// </summary>
    IEnumerator DelayToCheck()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Check");
        if (item1.GetID() == item2.GetID())
        {
            item1.ParticlesAndDisable();
            item2.ParticlesAndDisable();
            
            sizeBegin++;
            Score();
            sounds.CardDone();
            Debug.Log(sizeBegin + " / " + imgs.Length);

            if (differentImgs)
            {
                if (sizeBegin == imgs.Length / 2)
                {
                    started = false;
                    uiManager.GoWin(Mathf.RoundToInt(timeToPlay-timeLeft).ToString() + " s.", timeToPlay - timeLeft);
                }
            }
            if (!differentImgs)
            {
                if (sizeBegin == imgs.Length)
                {
                    started = false;
                    uiManager.GoWin(Mathf.RoundToInt(timeToPlay - timeLeft).ToString() + " s.", timeToPlay - timeLeft);
                }
            }
        }
        else
        {
            item1.ReturnAnim();
            item2.ReturnAnim();
        }

        yield return new WaitForSeconds(0.21f);
        selected = 0;
        waitForCheck = false;
    }

    /// <summary>
    /// Método para obtener el score
    /// </summary>
    void Score()
    {
        score += 100;
        scoreTxt.text = "Pares: " + sizeBegin + " / " + imgs.Length /2;
    }
    
    /// <summary>
    /// Método mostrar el tiempo restante de juego
    /// </summary>
    private void UpdateTimeText()
    {
        timeLeft -= Time.deltaTime;


        if (timeLeft <= 0)
        {
            // Debug.Log("Tiempo agotado.");
            Finalscore.text = score.ToString();
            started = false;
            uiManager.Loose();
        }

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
                                                    /////////////////////// Estructura del Juego ////////////////////////
    
}