using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameData gameData;

    private void Start()
    {
        // ���۽� GameData �� ������ Debug.Log�� �����ش�.

        print("game Name : " + gameData.gameName);
        print("game Name : " + gameData.gameScore);
        print("game Name : " + gameData.isGameActive);
    }

}
