using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilitiesHotbar : MonoBehaviour
{
    [SerializeField] AbilitiesHotbarSlot[] slots;


    public void DisplayCooldown (int slotNumber, float duration)
    {
        if (slotNumber < slots.Length)
        {
            slots[slotNumber].DoCooldownMask(duration);
        }
    }

    public void UpdateSlotIcons (GameData player)
    {
        foreach (var slot in slots)
        {
            slot.UpdateSlotInfo(player);
        }
    }
}