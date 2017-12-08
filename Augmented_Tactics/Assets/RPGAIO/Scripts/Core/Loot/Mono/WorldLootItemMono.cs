﻿using System.Linq;
using LogicSpawn.RPGMaker;
using LogicSpawn.RPGMaker.API;
using LogicSpawn.RPGMaker.Core;
using LogicSpawn.RPGMaker.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Testing
{
    public class WorldLootItemMono : MonoBehaviour  
    {
        [SerializeField]
        public WorldLootItem LootItem;
        public GameObject Canvas;
        public LootItemModel Model;
        public Text ItemText;

        void OnEnable()
        {
            if(Canvas == null)
            {
                Canvas = GetComponentInChildren<Canvas>().gameObject;
            }
            Canvas.GetComponent<Canvas>().worldCamera = GetObject.RPGCamera.GetComponent<Camera>();
            Model = Canvas.GetComponent<LootItemModel>();
            Canvas.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            Canvas.GetComponentInChildren<Button>().onClick.AddListener(Loot);
        }

        void OnDisable()
        {
            Canvas.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }

        public void Init()
        {
            Model = Canvas.GetComponent<LootItemModel>();
            Model.Item = LootItem.Item;
            SetLootText();
        }

        void SetLootText()
        {
            var lootText = LootItem.Gold != 0 ? LootItem.Gold + "g" : LootItem.Item.Name;
            if (LootItem.Item != null)
            {
                var stackable = LootItem.Item as IStackable;
                if (stackable != null && stackable.CurrentStacks > 0)
                {
                    lootText += " x" + stackable.CurrentStacks;
                }

                var color = RPG.Items.GetRarityColorById(LootItem.Item.RarityID);
                lootText = RPG.UI.FormatString(color, lootText);
            }

            ItemText.text = lootText;
        }

        public void Update()
        {

            if (LootItem.LootInWorldOnce && GetObject.PlayerSave.GamePersistence.LootedWorldObjects.FirstOrDefault(l => l == LootItem.LootId) != null)
            {
                Destroy(gameObject);
            }

            SetLootText();
            if(LootItem == null)
            {
                Destroy(gameObject);
                return;
            }

            if (Canvas == null)
            {
                return;
            }

            Canvas.transform.rotation = GetObject.RPGCamera.transform.rotation;

        }

        public void Loot()
        {
            var looted = false;
            if(LootItem.Gold != 0)
            {
                GetObject.PlayerMono.Player.Inventory.AddGold(LootItem.Gold);
                Debug.Log("You looted: " + LootItem.Gold + " gold.");
                looted = true;
            }
            else
            {
                if(Rm_RPGHandler.Instance.Items.AutoEquipOnLoot && (LootItem.Item is Apparel || LootItem.Item is Weapon))
                {
                    if(Rm_RPGHandler.Instance.Items.AutoEquipAlways)
                    {
                        var result = GetObject.PlayerCharacter.Equipment.EquipItem(LootItem.Item);
                        if (result == EquipResult.Success) looted = true;
                    }
                    else if(Rm_RPGHandler.Instance.Items.AutoEquipIfSlotEmpty)
                    {
                        if(LootItem.Item is Weapon)
                        {
                            var slot = GetObject.PlayerCharacter.Equipment.GetSlot("Weapon");
                            if(slot.Item == null)
                            {
                                var result = GetObject.PlayerCharacter.Equipment.EquipItem(LootItem.Item);
                                if (result == EquipResult.Success) looted = true;
                            }
                        }
                        else
                        {
                            var apparel = LootItem.Item as Apparel;
                            var slot = GetObject.PlayerCharacter.Equipment.GetSlot(apparel.apparelSlotID);
                            if (slot.Item == null)
                            {
                                var result = GetObject.PlayerCharacter.Equipment.EquipItem(LootItem.Item);
                                if (result == EquipResult.Success) looted = true;
                            }
                        }
                    }
                    else if(Rm_RPGHandler.Instance.Items.AutoEquipBasedOnLevel)
                    {
                        if (LootItem.Item is Weapon)
                        {
                            var slot = GetObject.PlayerCharacter.Equipment.GetSlot("Weapon");
                            var offHandslot = GetObject.PlayerCharacter.Equipment.GetSlot("OffHand");
                            var wep = LootItem.Item as Weapon;
                            if (slot.Item == null || (slot.Item as Weapon).RequiredLevel < wep.RequiredLevel || (Rm_RPGHandler.Instance.Items.AllowDualWield && Rm_RPGHandler.Instance.Items.EnableOffHandSlot && offHandslot.Item == null))
                            {
                                var result = GetObject.PlayerCharacter.Equipment.EquipItem(LootItem.Item);
                                if (result == EquipResult.Success) looted = true;
                            }
                        }
                        else
                        {
                            var apparel = LootItem.Item as Apparel;
                            var slot = GetObject.PlayerCharacter.Equipment.GetSlot(apparel.apparelSlotID);
                            if (slot.Item == null || (slot.Item as Apparel).RequiredLevel < apparel.RequiredLevel)
                            {
                                var result = GetObject.PlayerCharacter.Equipment.EquipItem(LootItem.Item);
                                if (result == EquipResult.Success) looted = true;
                            }
                        }
                    }
                }
                
                if(!looted)
                {
                    looted = GetObject.PlayerMono.Player.Inventory.AddItem(LootItem.Item);

                    if (looted)
                    {
                        Debug.Log("You looted: " + LootItem.Item.Name);
                    }
                }
                

            }


            TooltipUI.Clear();

            if(looted)
            {
                GetObject.PlayerSave.GamePersistence.LootedWorldObjects.Add(LootItem.LootId);
                Destroy(gameObject);
            }
        }

        
    }
}