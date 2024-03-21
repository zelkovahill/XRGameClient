using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;     // XML 사용하기 위해


public class PlayerData             // 플레이어 데이터 사용
{
    public string playerName;
    public int playerLevel;
    public List<string> items = new List<string>();
     
   
}

public class ExXMLData : MonoBehaviour
{

    string filePath;


    // Start is called before the first frame update
    void Start()
    {

        filePath = Application.persistentDataPath + "/PlayerData.xml";
        print(filePath);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
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
            print(playerData.items[0]);
            print(playerData.items[1]);
        }
    }

    private void SaveData(PlayerData data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream steam = new FileStream(filePath, FileMode.Create);       // 파일스트림 함수로 파일 생성
        serializer.Serialize(steam, data);                                  // 클래스 -> XML 변환 후 저장
        steam.Close();
    }

    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            FileStream steam = new FileStream(filePath, FileMode.Open); // 파일 읽기모드로 파일 열기
            PlayerData data = (PlayerData)serializer.Deserialize(steam); // XML -> 클래스 읽어서 변환
            steam.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
