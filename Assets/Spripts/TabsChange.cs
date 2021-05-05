using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsChange : MonoBehaviour
{
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject detailFlowers;

    public void ChangeTab(int id)
    {
        switch (id)
        {
            case 1:
                focusFlowers.SetActive(true);
                baseFlowers.SetActive(false);
                detailFlowers.SetActive(false);
                break;

            case 2:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(true);
                detailFlowers.SetActive(false);
                break;

            case 3:
                focusFlowers.SetActive(false);
                baseFlowers.SetActive(false);
                detailFlowers.SetActive(true);
                break;

            default:
                break;
        }
    }
}
