using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    [SerializeField] private Main main;
    [SerializeField] private int itemID;

    [SerializeField] private Material originalMat;
    private Material mat;
    public bool isEnabled;
    public bool done;
    public bool animationRunning;

    [SerializeField] private AnimationCurve rotateAnim;
    private float actualAnimTime;
    private float animTime = 0.2f;

    public bool isOpen;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (animationRunning)
            Animation();
    }

    public void SetImage(int itemID, Texture2D img)
    {
        mat = new Material(originalMat);
        GetComponent<Renderer>().material = mat;

        this.itemID = itemID;
        mat.SetTexture("_BackImg", img);

        isEnabled = true;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public bool CanInteract()
    {
        if (!isEnabled || isOpen || animationRunning)
            return false;
        else return true;
    }

    public int GetID()
    {
        return itemID;
    }

    public void UserClick()
    {

        animationRunning = true;

    }

    public void ReturnAnim()
    {
        animationRunning = true;

    }

    void Animation()
    {
        actualAnimTime += Time.deltaTime;

        if (!isOpen)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 180, actualAnimTime / animTime), 0);

            if (actualAnimTime >= animTime)
            {
                actualAnimTime = 0;
                animationRunning = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isOpen = true;
                main.CheckItems();
                return;
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Lerp(180, 0, actualAnimTime / animTime), 0);

            if (actualAnimTime >= animTime)
            {
                actualAnimTime = 0;
                animationRunning = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isOpen = false;
                return;
            }
        }
    }

    public void ParticlesAndDisable()
    {
        isEnabled = false;
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        float t = 0.00f;
        float timeToDisolve = 0.20f;

        while(t<= timeToDisolve)
        {
            t += Time.deltaTime;
            mat.SetFloat("_Dissolve", Mathf.Lerp(0,1.1f, t / timeToDisolve));
            yield return new WaitForEndOfFrame();
        }

    }
}
