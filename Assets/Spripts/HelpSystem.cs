using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpSystem : MonoBehaviour
{
    private float timePassed = 0;
    private bool isInside = false;

    private void OnMouseEnter()
    {
        isInside = true;
    }

    private void OnMouseExit()
    {
        isInside = false;
        timePassed = 0;
        CloseHelp();
    }

    private void Update()
    {
        if (isInside)
        {
            timePassed += Time.deltaTime;

            if (timePassed >= 1f)
            {
                OpenHelp();
            }
        }
    }

    private void OpenHelp()
    {
        transform.Find("Help").gameObject.SetActive(true);
    }

    private void CloseHelp()
    {
        transform.Find("Help").gameObject.SetActive(false);
    }
}
