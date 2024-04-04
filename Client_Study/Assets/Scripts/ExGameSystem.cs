using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int index;
    private string name;
    private EItemType type;
    private Sprite image;

    // ������Ƽ
    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public EItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    public Item(int index, string name, EItemType type)
    {
        this.index = index;
        this.name = name;
        this.type = type;
    }
}


public enum EItemType
{
    WEAPON,
    ARMOR,
    POTION,
    QUEST_ITEM
        // �پ��� ������ ���
}


public class Inventory
{
    private Item[] items = new Item[16];

    // ������ �ε���(indexer)
    public Item this[int index]
    {
        get { return items[index]; }
        set { items[index] = value; }
    }

    // ���� �κ��丮�� �ִ� ������ ��
    public int ItemCount
    {
        get
        {
            int count = 0;
            foreach (Item item in items)
            {
                if(item != null)
                    count++;
            }
            return count;
        }
    }

    public int InventoryCount
    {
        get { return items.Length; }
    }

    // ������ �߰�
    public bool AddItem(Item item)
    {
        for(int i =0; i < items.Length; i++)
        {
            if (items[i]==null)
            {
                items[i] = item;
                return true;
            }
        }
        return false; // �κ��丮�� ��ĭ�� ���� ���
    }


    // ������ ����
    public void RemoveItem(Item item)
    {
        for(int i =0; i<items.Length;i++)
        {
            if (items[i]==item)
            {
                items[i]= null;
                break;
            }
        }
    }
}

public class ExGameSystem : MonoBehaviour
{
    private Inventory inventory = new Inventory();

    Item sword = new Item(0, "Sword", EItemType.WEAPON);
    Item shield = new Item(0, "Shield", EItemType.ARMOR);


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            inventory.AddItem(sword);
            Debug.Log("inventory : " + GetInventoryAsString());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            inventory.AddItem(shield);
            Debug.Log("inventory : " + GetInventoryAsString());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.RemoveItem(sword);
            Debug.Log("inventory : " + GetInventoryAsString());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.RemoveItem(shield);
            Debug.Log("inventory : " + GetInventoryAsString());
        }
        
    }

    private string GetInventoryAsString()
    {
        string result = "";
        for (int i = 0; i < inventory.InventoryCount; i++) 
        {
            if (inventory[i]!=null)
            {
                result += inventory[i].Name + ",";
                
            }
        }
        return result.TrimEnd(',');
    }
}
