using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsChange : MonoBehaviour
{
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject fillFlowers;
    public GameObject detailFlowers;
    public GameObject green;

    public void ChangeTab(int id)
    {
        switch (id)
        {
            case 1:
                focusFlowers.SetActive(true);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                break;

            case 2:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(true);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                break;

            case 3:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(true);
                detailFlowers.SetActive(false);
                green.SetActive(false);
                break;

            case 4:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(true);
                green.SetActive(false);
                break;

            case 5:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                fillFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                green.SetActive(true);
                break;

            default:
                break;
        }
    }
}
