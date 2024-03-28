using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;      // JSON ����ȭ�� ���� ��Ű��
using UnityEngine;
using System.Text;
using System;
using System.Linq;
using System.Security.Cryptography;

public class ExEncrypt : MonoBehaviour
{
    string filePath;
    string Key = "ThisIsASecretKey";    // ��ȣȭŰ


    private void Start()
    {
        filePath = Application.persistentDataPath + "/EncryptPlayerData.json";
        print(filePath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
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
            for (int i = 0; i < playerData.items.Count; i++)
            {
                print(playerData.items[i]);
            }


        }
    }



    private void SaveData(PlayerData data)
    {
        // JSON ����ȭ
        string jsonData = JsonConvert.SerializeObject(data);

        // �����͸� ����Ʈ �迭�� ��ȯ
        byte[] bytesToEncrypt =Encoding.UTF8.GetBytes(jsonData);

        // ��ȣȭ
        byte[] encryptedBytes = Encrypt(bytesToEncrypt);

        // ��ȣȭ�� �����͸� Base64 ���ڿ��� ��ȯ
        string encrytedData = Convert.ToBase64String(encryptedBytes);

        // ���� ����
        File.WriteAllText(filePath, encrytedData);
    }

    PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
           
            // ���Ͽ��� ��ȣȭ�� ������ �б�
            string encryptedData = File.ReadAllText(filePath);

            // Base64 ���ڿ��� ����Ʈ �迭�� ��ȯ
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            // ��ȣȭ
            byte[] decryptedBytes = Decrypt(encryptedBytes);

            // ����Ʈ �迭�� ���ڿ��� ��ȯ
            string jsonData = Encoding.UTF8.GetString(decryptedBytes);

            // JSON ������ȭ
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }

    byte[] Encrypt(byte[] plainBytes)
    {
        using (Aes aesAlg = Aes.Create()) 
        {
            aesAlg.Key =Encoding.UTF8.GetBytes(Key);
            aesAlg.IV = new byte[16];   // IV (intialization Vector) �������� ����ϰų� �������� ����

            // ��ȣȭ ��ȯ�� ����
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key,aesAlg.IV);

            // ��Ʈ�� ����
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                // ��Ʈ���� ��ȣȭ ��ȯ�⸦ �����Ͽ� ��ȣȭ ��Ʈ���� ����
                using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) 
                {
                    // ��ȣȭ ��Ʈ���� ������ ����
                    csEncrypt.Write(plainBytes,0,plainBytes.Length);
                    csEncrypt.FlushFinalBlock();

                    // ��ȣȭ�� ������ ����Ʈ�� �迭�� ��ȯ
                    return msEncrypt.ToArray();
                }

            }
        }
    }

    byte[] Decrypt(byte[] encryptedBytes)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(Key);
            aesAlg.IV = new byte[16];   // IV (intialization Vector) �������� ����ϰų� �������� ����

            // ��ȣȭ ��ȯ�� ����
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key,aesAlg.IV);

            // ��Ʈ�� ����
            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                // ��Ʈ���� ��ȣȭ ��ȯ�⸦ �����Ͽ� ��ȣȭ ��Ʈ�� ����
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    // ��ȣȭ�� �����͸� ���� ����Ʈ �迭 ����
                    byte[] decryptedBytes = new byte[encryptedBytes.Length];

                    // ��ȣȭ ��Ʈ������ �����͸� �б�
                    int decryptedByteCount = csDecrypt.Read(decryptedBytes, 0, decryptedBytes.Length);

                    // ������ ���� ũ�⸸ŭ�� ����Ʈ �迭�� ��ȯ
                    return decryptedBytes.Take(decryptedByteCount).ToArray();
                }
            }
        }


    }


}
