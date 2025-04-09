using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] private int destroyedObjects;
    [SerializeField] private GameObject[] keys;
    [SerializeField] public bool isLevelComplete = false;

    private void Start()
    {
        foreach (GameObject item in keys)
        {
            item.SetActive(false);
        }
    }

    private void Update()
    {
        UnlockKeys();
    }

    public void ScoreCount()
    {
        destroyedObjects++;
    }

    void UnlockKeys()
    {
        switch (destroyedObjects)
        {
            case 5:
                keys[0].SetActive(true);
                break;
            case 10:
                keys[1].SetActive(true);
                break;
            case 15:
                keys[2].SetActive(true);
                break;
            case 20:
                keys[3].SetActive(true);
                break;
            case 25:
                keys[4].SetActive(true);
                break;
            case 30:
                keys[5].SetActive(true);
                break;
            case 35:
                keys[6].SetActive(true);
                break;
            case 40:
                keys[7].SetActive(true);
                break;
            case 45:
                isLevelComplete = true;
                keys[8].SetActive(true);
                break;
        }
    }
}

