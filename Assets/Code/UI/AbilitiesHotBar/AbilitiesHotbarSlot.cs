using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AbilitiesHotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image CooldownMask;
    [SerializeField] Image icon;
    [SerializeField] GameObject descriptionGameObject;
    [SerializeField] Text nameText;
    [SerializeField] Text descriptionText;
    [SerializeField] int index;

    float cooldownMax;
    float cooldownCurrent;

    void Update()
    {
        if (cooldownCurrent > 0)
        {
            cooldownCurrent -= Time.deltaTime;
            CooldownMask.fillAmount = cooldownCurrent / cooldownMax;
        }
    }

    public void DoCooldownMask(float cooldown)
    {
        cooldownMax = cooldown;
        cooldownCurrent = cooldown;
    }

    public void UpdateSlotInfo(GameData player)
    {
        AbilityTypes type = player.abilityLoadout.abilities[index];
        Ability a = PlayerAbilityDirectory.Instance.lookup[type];

        //Set icon
        icon.sprite = a.Icon;

        //Set text
        descriptionText.text = a.Description;
        nameText.text = a.name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionGameObject.SetActive(false);
    }
}