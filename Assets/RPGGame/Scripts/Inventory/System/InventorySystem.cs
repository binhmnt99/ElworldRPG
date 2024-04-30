namespace RPG.Inventory
{
    using UnityEngine;
    using Unity.Entities;
    using Unity.Burst;
    using System;
    using Unity.Collections;
    using RPG.Player;

    public partial class InventorySystem : SystemBase
    {
        Entity playerEntity;
        HotBar hotBar;

        protected override void OnCreate()
        {
            RequireForUpdate<HotBar>();
        }

        protected override void OnUpdate()
        {
            playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            CreateHotBar();

        }

        private void CreateHotBar()
        {
            hotBar = EntityManager.GetComponentObject<HotBar>(playerEntity);
            if (hotBar.ItemSlots == null)
            {
                hotBar.ItemSlots = new(hotBar.InventorySize);
                for (int i = 0; i < hotBar.InventorySize; i++)
                {
                    hotBar.ItemSlots.Add(new ItemSlot());
                }
            }
        }
    }
}