using UnityEngine;
using System.Collections;



namespace MyNameSpace
{
    public class PlayerResourceManagement
    {
        const float RegenInterval = 5f;

        PlayerController player;
        PlayerStats stats;
        UIManager ui;
        SfxManager sfx;

        float regenTimer;

        public PlayerResourceManagement(PlayerController player)
        {
            this.player = player;
            stats = player.data.stats;

            ui = UIManager.Instance;
            sfx = SfxManager.instance;

            UpdateUI_Money();
        }

        public void PassiveRegen()
        {
            if (regenTimer > 0f)
            {
                regenTimer -= Time.deltaTime;
            }
            else
            {
                regenTimer = RegenInterval;
                stats.HP += stats.HPRegen;
                stats.MP += stats.MPRegen;
                stats.AP += stats.APRegen;
                UpdateUI_HPBar();
                UpdateUI_ManaBar();
                UpdateUI_StaminaBar();
            }
        }

        public void DamagePlayer(int damage)
        {
            stats.HP -= damage;
            sfx.Spawn_PlayerHurt();
            ui.OnDamaged();

            if (stats.HP < 0)
            {
                stats.HP = 0;
                player.PlayerDead();
            }

            UpdateUI_HPBar();

            //status.lastEnemyPosition = enemyPos;
            //SwitchToNewState(MotorStates.Hurt);
        }

        public void FullyHealPlayer()
        {
            sfx.Spawn_PlayerRespawn();

            stats.HP = stats.HPMax;
            UpdateUI_HPBar();

            stats.MP = stats.MPMax;
            UpdateUI_ManaBar();

            stats.AP = stats.APMax;
            UpdateUI_StaminaBar();
        }

        public void HealPlayer(int amount)
        {
            stats.HP = Mathf.Clamp(stats.HP + amount, 0, stats.HPMax);
            UpdateUI_HPBar();
        }

        public void RestoreMana(int amount)
        {
            stats.MP = Mathf.Clamp(stats.MP + amount, 0, stats.MPMax);
            UpdateUI_ManaBar();
        }

        public void RestoreStamina(int amount)
        {
            stats.AP = Mathf.Clamp(stats.AP + amount, 0, stats.APMax);
            UpdateUI_StaminaBar();
        }

        public void UpdateUI_HPBar() => ui.UpdateHealth(stats.HP, stats.HPMax);
        public void UpdateUI_ManaBar() => ui.UpdateMana(stats.MP, stats.MPMax);
        public void UpdateUI_StaminaBar() => ui.UpdateAP(stats.AP, stats.APMax);

        #region Money
        public bool TrySpendMoney(int amount)
        {
            //Debug.Log("TrySpendMoney :" + amount);
            if (player.data.money - amount > 0)
            {
                player.data.money -= amount;
                UpdateUI_Money();
                return true;
            }
            return false;
        }

        public void AddMoney(int amount)
        {
            player.data.money += amount;
            UpdateUI_Money();
        }

        public void UpdateUI_Money() => ui.SetMoney(player.data.money);
        #endregion
    }
}