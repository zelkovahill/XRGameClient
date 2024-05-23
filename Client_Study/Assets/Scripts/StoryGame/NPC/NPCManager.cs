using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public Entity_Dialog entity_Dialog;

    public void Start()
    {
        
    }

    public Entity_Dialog.Param GetParamData(int npc, int gamestate) // npc 번호와 게임 상태 값으로 다이얼로그 클래스를 받아옴
    {
        foreach(Entity_Dialog.Param param in entity_Dialog.sheets[0].list)
        {
            if(param.npc == npc && param.gamestate == gamestate)
            {
                return param;
            }
        }
        // 해당 데이터가 없을 경우 null 반환
        return null;
    }


}
