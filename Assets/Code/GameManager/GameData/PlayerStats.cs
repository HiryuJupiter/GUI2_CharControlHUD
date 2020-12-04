using UnityEngine;
using System.Collections;

public enum StatTypes
{
    HP, //Health points
    HP_Regen,
    MP, //Mana points
    MP_Regen,
    AP, //Action points (i.e. stamina)
    AP_Regen,

    AttackDamage, //Physical attack
    AbilityPower, //Magical attack
    Defence, //Physical and magical defence
    EvasionRate,
    AttackSpeed, //Decreased delay between attacks,
    CritChance, //Rate of landing critical hits
    MoveSpeed, //Move speed
}

[System.Serializable]
public class PlayerStats
{
    public int HP,  HPMax,  HPRegen;
    public int MP,  MPMax,  MPRegen;
    public int AP,  APMax,  APRegen;

    public int AttackDamage;
    public int AbilityPower;
    public int Defence;

    public int EvasionRate;
    public int AttackSpeed;
    public int CritChance;
    public int MoveSpeed;

    public PlayerStats (PlayerAttribute a)
    {
        InitializeStats(a);
    }

    public void InitializeStats (PlayerAttribute a)
    {
        HPMax   = 100 + a.Endurance * 2;
        HPRegen = (int)(1 + a.Endurance * 0.2f);
        HP      = HPMax;

        MPMax   = 100 + a.Willpower * 2;
        MPRegen = (int)(1 + a.Agility * 1f);
        MP      = MPMax;

        APMax   = 100 + a.Endurance * 2;
        APRegen = (int)(1 + a.Willpower * 1f);
        AP      = APMax;

        AttackDamage    = (int)(a.Strength * 2f);
        AbilityPower    = (int)(a.Intelligence * 2.5f);
        Defence         = (int)(a.Endurance * 1.5f);
        EvasionRate     = (int)(a.Luck * 0.5f);
        AttackSpeed     = (int)(a.Agility * 0.8f);
        CritChance      = (int)(a.Luck * 1f);
        MoveSpeed       = (int)(a.Agility * 2f);
    }
}