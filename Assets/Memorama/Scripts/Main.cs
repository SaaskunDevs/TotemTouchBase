using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] public Texture2D[] imgs;

    [SerializeField] UIManager uiManager;
    [SerializeField] SoundsManager sounds;

    private bool started;
    private bool waitForCheck;

    #region Timer
    [SerializeField] private int timeToPlay = 60; // Tiempo en segundos
    [SerializeField] private TextMeshProUGUI timeText; // Texto para mostrar el tiempo restante
    private float timeLeft; // Tiempo restante en segundos
    #endregion

    #region Score
    [SerializeField] private TextMeshProUGUI scoreTxt; // Texto para mostrar el score
    [SerializeField] TextMeshProUGUI Finalscore;
    private int score, sizeBegin;
    #endregion

    public int selected;

    private Icon item1, item2;
    public bool differentImgs;

    private void Update()
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

    void CheckTouches()
    {
        if (waitForCheck)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Realizar una prueba de colisi�n en la posici�n del clic
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity);

            if (hit.collider != null)
            {
 //               Debug.Log("Collider " + hit.collider.name + " ha sido tocado");

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

    public void CheckItems()
    {
        if (waitForCheck)
            return;
       // canSelect = true;
        if (selected != 2)
            return;
        waitForCheck = true;
        Debug.Log("Check items");
        StartCoroutine(DelayToCheck());
    }

    IEnumerator DelayToCheck()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Check");
        if (item1.GetID() == item2.GetID())
        {
 //           Debug.Log("tabien");
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
                    // Finalscore.text = score.ToString();
                    started = false;
                    uiManager.GoWin(Mathf.RoundToInt(timeToPlay-timeLeft).ToString() + " s.", timeToPlay - timeLeft);
                }
            }
            if (!differentImgs)
            {
                if (sizeBegin == imgs.Length)
                {
                  //  Finalscore.text = score.ToString();
                    started = false;
                    uiManager.GoWin(Mathf.RoundToInt(timeToPlay - timeLeft).ToString() + " s.", timeToPlay - timeLeft);
                }
            }
        }
        else
        {
  //          Debug.Log("Tamal");
            item1.ReturnAnim();
            item2.ReturnAnim();
        }

        yield return new WaitForSeconds(0.21f);
        selected = 0;
        waitForCheck = false;
    }

    void Score()
    {
        score += 100;
        scoreTxt.text = "Pares: " + sizeBegin + " / " + imgs.Length /2;
    }
    private void UpdateTimeText()
    {
        timeLeft -= Time.deltaTime;


        if (timeLeft <= 0)
        {
            Debug.Log("Tiempo agotado.");
            Finalscore.text = score.ToString();
            started = false;
            uiManager.Loose();
        }

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
