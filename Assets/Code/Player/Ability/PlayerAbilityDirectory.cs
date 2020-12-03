using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DefaultExecutionOrder(-100000000)] //This has to run before the managers
public class PlayerAbilityDirectory : MonoBehaviour
{
    public static PlayerAbilityDirectory Instance;

    [Header("Scriptable objects")]
    [SerializeField] Ability[] Abilities;

    public Dictionary<AbilityTypes, Ability> lookup { get; private set; } = new Dictionary<AbilityTypes, Ability>();

    void Awake ()
    {
        Instance = this;

        foreach (var a in Abilities)
        {
            lookup.Add(a.AbilityType, a);
            a.Initialize();
        }
    }

    public bool Contains (AbilityTypes abilityType)
    {
        return lookup.ContainsKey(abilityType);
    }

    public bool TryGetAbility(AbilityTypes abilityType, out Ability ability)
    {
        ability = null;
        if (Contains(abilityType))
        {
            ability = lookup[abilityType];
            return true;
        }
        return false;
    }
}