using UnityEngine;
using System;
using System.Collections;



namespace MyNameSpace
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] string abilityName;
        [SerializeField] string description;
        [SerializeField] Sprite icon;
        [SerializeField] AudioSource sfx;
        [SerializeField] GameObject spawnBullet;

        [SerializeField] AbilityTypes abilityType;

        [SerializeField] float animationLength; //Time before nautrally returning to idle pose. It's longer than the actual animation so that the player can queue and play another attack immediately.
        [SerializeField] float channelingDuration; //Minimum time before the player can do anything else
        [SerializeField] float cooldown;
        [SerializeField] float manaCost;
        [SerializeField] float apCost;

        public string AbilityName => abilityName;
        public string Description => description;
        public Sprite Icon => icon;
        public AudioSource Sfx => sfx;
        public GameObject SpawnBullet => spawnBullet;
        public AbilityTypes AbilityType => abilityType;
        public float AnimationLength => animationLength;
        public float ChannelingDuration => channelingDuration;
        public float Cooldown => cooldown;
        public bool IsCooldownReady { get; private set; } = true;
        public float ManaCost => manaCost;
        public float ApCost => apCost;

        public IEnumerator GoOnCooldown()
        {
            IsCooldownReady = false;
            yield return new WaitForSeconds(cooldown);
            IsCooldownReady = true;
        }

        public void Initialize()
        {
            //Because this is a ScriptableObject, we want to make sure certain
            //... variable values are not overridden by the settings in inspector
            IsCooldownReady = true;
        }
    }
}