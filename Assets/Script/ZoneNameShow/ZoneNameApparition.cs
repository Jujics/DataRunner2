using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ZoneNameApparition : MonoBehaviour
{
    [SerializeField] private string placeName;
    [SerializeField] private TMP_Text placeNameText;
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            hasEntered = true;
            StartCoroutine(ShowText());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasEntered = false;
        }
    }

    IEnumerator ShowText()
    {
        placeNameText.gameObject.SetActive(true);
        placeNameText.text = placeName;
        yield return new WaitForSeconds(4f);
        placeNameText.text = "";
    }
}
