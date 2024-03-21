using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPlayerPrefabsData : MonoBehaviour
{
    public int scorePoint;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveData(scorePoint);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Score : " + LoadData());
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey("Score");
        }
    }

    void SaveData(int score)
    {
        PlayerPrefs.SetInt("Score",score);
        PlayerPrefs.Save();
    }


    int LoadData()
    {
        return PlayerPrefs.GetInt("Score");
    }
}
