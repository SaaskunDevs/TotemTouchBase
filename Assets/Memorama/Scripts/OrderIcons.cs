using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderIcons : MonoBehaviour
{
    [Header ("Prefabs")]
    public GameObject iconPrefab2D; // Prefab de icono 2D
    public Icon iconPrefab3D; // Prefab de icono 3D

    [Header ("Panel")]
    public RectTransform panel; // Panel para los iconos 2D
    public Transform panel3D; // GameObject para los iconos 3D

    [Header ("Scripts")]
    public Main main;
    
    [Header ("Variables")]
    public float padding = 10f;
    public float padding3D = 1.5f;

    void Start()
    {
        CreateIcons();
    }

    public void CreateIcons()
    {
        //Obtenemos el tamaño con respecto a la cantidad de imagenes del arreglo en Main
        int size = 2 * main.imgs.Length;
        Debug.Log(size);
        
        //Obtenemos el tamaño del panel
        float panelWidth = panel.rect.width;
        float panelHeight = panel.rect.height;

        //Calculamos cuántos iconos caben en una fila y en una columna con respecto al tamaño del arreglo de imagenes
        int iconsPerRow = Mathf.FloorToInt(Mathf.Sqrt(size)); // Raíz cuadrada del tamaño del arreglo
        int iconsPerColumn = Mathf.CeilToInt((float)size / iconsPerRow);

        //Ajustamos el ancho y el espacio entre los iconos
        float iconWidth = (panelWidth - padding * (iconsPerRow + 1)) / iconsPerRow;
        float iconHeight = (panelHeight - padding * (iconsPerColumn + 1)) / iconsPerColumn;

        //Inicializamos la posicion en x a la esquina izquierda del panel
        float startX = -panelWidth / 2 + iconWidth / 2 + padding;
        float startY = panelHeight / 2 - iconHeight / 2 - padding;
        float currentX = startX;
        float currentY = startY;
        
        float startX3D = -padding3D * (iconsPerRow - 1) / 2; // Posición x inicial para los prefabs 3D
        float startY3D = padding3D * (iconsPerColumn - 1) / 2; // Posición y inicial para los prefabs 3D
        float currentX3D = startX3D;
        float currentY3D = startY3D -.75f;

        Debug.Log("y inicial" + startY3D);
        
        for (int i = 0; i < size; i++)
        {
            // Crear icono 2D
            GameObject newIcon2D = Instantiate(iconPrefab2D, panel);
            RectTransform rect = newIcon2D.GetComponent<RectTransform>();

            //Ajustamos el tamaño del icono 2D
            rect.sizeDelta = new Vector2(iconWidth, iconHeight);

            rect.anchoredPosition = new Vector2(currentX, currentY);

            // Crear icono 3D
            Icon newIcon3D = Instantiate(iconPrefab3D, panel3D);

            //Ajustamos el tamaño del icono 3D
            newIcon3D.transform.localScale = new Vector3(1, 1, 1);

            // Ajustamos la posición del icono 3D para que esté en el mismo lugar que el icono 2D
            newIcon3D.transform.position = new Vector3(currentX3D, currentY3D, 0);

            main.icons[i] = newIcon3D; // Guardamos el icono 3D en el arreglo de iconos 3D

            currentX += iconWidth + padding;
            currentX3D += padding3D; // Aumentamos la posición x para los prefabs 3D

            if((i+1) % iconsPerRow == 0)
            {
                currentX = startX;
                currentY -= iconHeight + padding;
                currentX3D = startX3D; // Reiniciamos la posición x para los prefabs 3D
                currentY3D -= padding3D; // Aumentamos la posición y para los prefabs 3D
            }
        }
        main.CreateIDs(size);
    }
}