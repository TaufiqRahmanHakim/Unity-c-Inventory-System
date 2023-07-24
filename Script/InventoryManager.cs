using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotHolders;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    public List<SlotClass> items = new List<SlotClass>();

    private List<GameObject[]> slots;

    public void Start()
    {
        slots = new List<GameObject[]>();

        foreach (var holder in slotHolders)
        {
            GameObject[] holderSlots = new GameObject[holder.transform.childCount];
            for (int i = 0; i < holder.transform.childCount; i++)
            {
                holderSlots[i] = holder.transform.GetChild(i).gameObject;
            }
            slots.Add(holderSlots);
        }

        RefreshUI();

        Add(itemToAdd);
        Remove(itemToRemove);
    }

    public void RefreshUI()
    {
        for (int holderIndex = 0; holderIndex < slots.Count; holderIndex++)
        {
            GameObject[] currentHolderSlots = slots[holderIndex];

            for (int i = 0; i < currentHolderSlots.Length; i++)
            {
                if (i < items.Count && items[i] != null)
                {
                    currentHolderSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    currentHolderSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                    if (items[i].GetItem().isStackable)
                        currentHolderSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity() + "";
                    else
                        currentHolderSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
                else
                {
                    currentHolderSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    currentHolderSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    currentHolderSlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
    }

    public bool Add(ItemClass item)
    {
        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
            slot.AddQuantity(1);
        else
        {
            items.Add(new SlotClass(item, 1));
            if (items.Count < slots.Count)
                return false;
        }
        RefreshUI();
        return true;
    }

    public bool Remove(ItemClass item)
    {
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if(temp.GetQuantity() > 1)
            temp.SubQuantity(1);
            else
            {
                SlotClass slotToRemove = new SlotClass();
                foreach (SlotClass slot in items)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }
                items.Remove(slotToRemove);
            }
        }
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }
    public SlotClass Contains(ItemClass item)
    {

        foreach(SlotClass slot in items)
        {
            if (slot.GetItem() == item)
                return slot;
        }
        return null;
    }


}
