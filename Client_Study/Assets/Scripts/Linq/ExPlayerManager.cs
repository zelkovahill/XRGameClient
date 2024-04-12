using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class ExPlayerManager : MonoBehaviour
{
   public List<PlayerData> playerDatas = new List<PlayerData>();

   private void Start()
   {
      for (int index = 0; index < 100; index++)
      {
         PlayerData playerData = new PlayerData()
         {
            playerName = "플레이어 " + index.ToString(),
            playerLevel = Random.Range(0, 20)
         };
         playerDatas.Add(playerData);
      }

      // 플레이어 레벨이 10 이상인 플레이어만 출력
      var highLevelPlayers = playerDatas.Where(PlayerData => PlayerData.playerLevel >= 10);

      foreach (var Player in highLevelPlayers)
      {
         print("High Level Player : " + Player.playerName + " Level : " + Player.playerLevel);
      }
   }
}
