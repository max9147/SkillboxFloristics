using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogSystem : MonoBehaviour
{
    public GameObject flowerLog;
    public GameObject log;
    public GameObject content;

    private GameObject[] logs = new GameObject[30];
    private int logAmount = 0;
    private float logItemHeight;

    private void Start()
    {
        logItemHeight = flowerLog.GetComponent<RectTransform>().rect.height;
    }    

    public void AddFlowerToLog(Flower flower)
    {
        GameObject curLog = Instantiate(flowerLog, log.transform);
        logs[logAmount] = curLog;
        curLog.transform.localPosition = new Vector3(0, (-logItemHeight * logAmount) + (logItemHeight / 2 * logAmount), 0);
        curLog.transform.Find("FlowerImage").GetComponent<Image>().sprite = flower.imageC;
        curLog.transform.Find("FlowerName").GetComponent<TextMeshProUGUI>().text = flower.flowerName;
        switch (flower.size)
        {
            case "Focus":
                curLog.transform.Find("FlowerSize").GetComponent<TextMeshProUGUI>().text = "��������";
                break;
            case "Base":
                curLog.transform.Find("FlowerSize").GetComponent<TextMeshProUGUI>().text = "�������";
                break;
            case "Fill":
                curLog.transform.Find("FlowerSize").GetComponent<TextMeshProUGUI>().text = "�����������";
                break;
            case "Details":
                curLog.transform.Find("FlowerSize").GetComponent<TextMeshProUGUI>().text = "������";
                break;
            case "Green":
                curLog.transform.Find("FlowerSize").GetComponent<TextMeshProUGUI>().text = "������";
                break;
            default:
                break;
        }
        logAmount++;
        for (int i = 0; i < logAmount - 1; i++)
        {
            logs[i].transform.localPosition += new Vector3(0, Mathf.Clamp01(logAmount - 1) * logItemHeight / 2, 0);
        }
    }

    public void ClearLog()
    {
        for (int i = 0; i < logAmount; i++)
        {
            Destroy(logs[i]);
            logs[i] = null;
        }
        logAmount = 0;
    }

    public void RemoveFromLog()
    {
        Destroy(logs[logAmount - 1]);
        logs[logAmount - 1] = null;
        logAmount--;
        for (int i = 0; i < logAmount; i++)
        {
            logs[i].transform.localPosition += new Vector3(0, -logItemHeight / 2, 0);
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, logAmount * logItemHeight);
    }

    public void RemoveFromLog(int id)
    {
        Destroy(logs[id - 1]);
        logs[id - 1] = null;
        for (int i = id - 1; i < logAmount - 1; i++)
        {
            logs[i] = logs[i + 1];
        }
        logs[logAmount - 1] = null;
        logAmount--;
        for (int i = 0; i < id - 1; i++)
        {
            logs[i].transform.localPosition += new Vector3(0, -logItemHeight / 2, 0);
        }
        for (int i = id - 1; i < logAmount; i++)
        {
            logs[i].transform.localPosition += new Vector3(0, logItemHeight / 2, 0);
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, logAmount * logItemHeight);
    }

    public void LayerUpLog(int id)
    {
        logs[id - 1].transform.localPosition -= new Vector3(0, logItemHeight, 0);
        logs[id].transform.localPosition += new Vector3(0, logItemHeight, 0);

        GameObject temp = logs[id];
        logs[id] = logs[id - 1];
        logs[id - 1] = temp;
    }

    public void LayerDownLog(int id)
    {
        logs[id - 1].transform.localPosition += new Vector3(0, logItemHeight, 0);
        logs[id - 2].transform.localPosition -= new Vector3(0, logItemHeight, 0);

        GameObject temp = logs[id - 2];
        logs[id - 2] = logs[id - 1];
        logs[id - 1] = temp;
    }

    public float GetItemHeight()
    {
        return logItemHeight;
    }
}