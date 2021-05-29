using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabsChange : MonoBehaviour
{
    public GameObject description;
    public GameObject content;
    public GameObject soundButton;
    public GameObject focusFlowers;
    public GameObject baseFlowers;
    public GameObject fillFlowers;
    public GameObject detailFlowers;
    public GameObject green;
    public TextMeshProUGUI descriptionText;

    private int openTab = 0;

    public void ChangeTab(int id)
    {
        content.GetComponent<AudioSource>().Play();
        content.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        openTab = id;
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

    public void OpenDescription()
    {
        content.GetComponent<AudioSource>().Play();
        if (description.activeInHierarchy)
        {
            description.SetActive(false);
            return;
        }

        switch (openTab)
        {
            case 1:
                description.SetActive(true);
                descriptionText.text = "�������� ����� - ��� ������� �������� � �������, ��� �������, ��� ��������� ��������� �������� ��� �������� ������������ " +
                    "�� ���� �������� ������� ��� ������ ������.";
                break;
            case 2:
                description.SetActive(true);
                descriptionText.text = "������� ����� - �� ������ ������� � ������������� ������� ��������� ������, � �������� ������ ������. �� ���� ������ ����, ��� � �������� ������. " +
                    "���� � �� �� �������� ����� ��������� � ������ ��� �������� ��������, ��� � ��� �������, � ����������� �� ������ ������������. " +
                    "� ������� ������� �������� ����� ���������� �� ������ 3 - � ����� ��� ������ �������� � ������ �������� ������, ������������ �� �������� ��������.";
                break;
            case 3:
                description.SetActive(true);
                descriptionText.text = "�������� � ������ ������� ��� � �������� ���������� �������, ���������� � ��������. ��� ��������� ������������ " +
                    "����� �������� ��������, ��������� � ����� ����������������.";
                break;
            case 4:
                description.SetActive(true);
                descriptionText.text = "��� �������, ��� �������� � ��������� �������� ������, �� ������ ����������� ������� ������, �� ��� ���� �� ��������� ������ ��������� ��������� " +
                    "������������� �������� �������. ������ �� ����������� ��� ����� ������� ������ � ����� �������� ����������.";
                break;
            case 5:
                description.SetActive(true);
                descriptionText.text = "������ � ������ ����� ��������� ����������� ���� (�������� �������, �������� ������� �������) ��� ������������ (���������� ����� " +
                    "��� ����� �������, ������). ��� �������� ����������� ������� ����������� �������� ��������� ����� ������, ������������ �� �������, ����� ��� ������ �����.";
                break;
            default:
                break;
        }
    }

    public bool CheckDescriptionOpen()
    {
        if (description.activeInHierarchy) return true;
        else return false;
    }
}
