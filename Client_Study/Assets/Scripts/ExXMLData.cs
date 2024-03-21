using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;     // XML ����ϱ� ����


public class PlayerData             // �÷��̾� ������ ���
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
            playerData.playerName = "�÷��̾� 1";
            playerData.playerLevel = 1;
            playerData.items.Add("��1");
            playerData.items.Add("����1");
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
        FileStream steam = new FileStream(filePath, FileMode.Create);       // ���Ͻ�Ʈ�� �Լ��� ���� ����
        serializer.Serialize(steam, data);                                  // Ŭ���� -> XML ��ȯ �� ����
        steam.Close();
    }

    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
            FileStream steam = new FileStream(filePath, FileMode.Open); // ���� �б���� ���� ����
            PlayerData data = (PlayerData)serializer.Deserialize(steam); // XML -> Ŭ���� �о ��ȯ
            steam.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
