using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioSource audioS;

    [SerializeField] AudioClip card;
    [SerializeField] AudioClip finish;
    public void CardDone()
    {
        audioS.PlayOneShot(card);
    }

    public void Finish()
    {
        audioS.PlayOneShot(finish);
    }
}
