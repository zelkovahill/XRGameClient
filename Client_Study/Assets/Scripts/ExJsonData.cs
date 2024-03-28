using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;      // JSON 직렬화를 위한 패키지
using UnityEngine;

public class ExJsonData : MonoBehaviour
{
    string filePath;


    private void Start()
    {
        filePath = Application.persistentDataPath + "/PlayerData.json";
        print(filePath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerData playerData = new PlayerData();
            playerData.playerName = "플레이어 1";
            playerData.playerLevel = 1;
            playerData.items.Add("돌1");
            playerData.items.Add("바위1");
            SaveData(playerData);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();

            playerData = LoadData();

            print(playerData.playerName);
            print(playerData.playerLevel);
            for(int i = 0; i < playerData.items.Count; i++)
            {
                print(playerData.items[i]);
            }
            
            
        }
    }



    private void SaveData(PlayerData data)
    {
        // JSON 직렬화
       string jsonData = JsonConvert.SerializeObject(data);

        // 파일 저장
        File.WriteAllText(filePath,jsonData);
    }

    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            // 파일에서 데이터 읽기
            string jsonData = File.ReadAllText(filePath);

            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }

}
