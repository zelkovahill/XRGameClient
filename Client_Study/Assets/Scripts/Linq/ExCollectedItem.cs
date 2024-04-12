using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExItem
{
    public bool IsCollected;
}

public class ExCollectedItem : MonoBehaviour
{
    public List<ExItem> collectedItem = new List<ExItem>(); // 컬렉팅 할 리스트

    private void Start()
    {
        collectedItem.Add(new ExItem());
        collectedItem.Add(new ExItem());
        collectedItem[0].IsCollected = true;
        collectedItem[1].IsCollected = false;
        
    }


    private void CheckAllItemCollected()
    {
        if (collectedItem.All(item => item.IsCollected)) // 모든 아이템이 수집 되었는지 검사
        {
            print("All item collected");
        }
        else
        {
            print("Not all item collected");
        }
    }

}
