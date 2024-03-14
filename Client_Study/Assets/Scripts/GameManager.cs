using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameData gameData;

    private void Start()
    {
        // 시작시 GameData 의 내역을 Debug.Log로 보여준다.

        print("game Name : " + gameData.gameName);
        print("game Name : " + gameData.gameScore);
        print("game Name : " + gameData.isGameActive);
    }

}
