namespace RPG.Inventory
{
    using UnityEngine;
    using Unity.Entities;
    using Unity.Burst;
    using System;
    using Unity.Collections;
    using RPG.Player;
    using System.Linq;

    // public partial class InventorySystem : SystemBase
    // {
    //     Entity playerEntity;
    //     HotBar hotBar;

    //     protected override void OnCreate()
    //     {
    //         //RequireForUpdate<HotBar>();
    //     }

    //     protected override void OnUpdate()
    //     {
    //         playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
    //         CreateHotBar();

    //     }

    //     private void CreateHotBar()
    //     {
    //         if (hotBar.InventorySize <= 0)
    //         {
    //             Debug.Log("ItemSlots is Empty");
    //             hotBar = EntityManager.GetComponentData<HotBar>(playerEntity);


    //         }
    //     }
    // }


    public partial struct InventorySystem : ISystem
    {
        private EntityManager entityManager;
        private Entity playerEntity;
        private HotBarSize hotBarSizeComponent;
        public void OnCreate(ref SystemState state)
        {
            entityManager = state.EntityManager;
        }
        public void OnUpdate(ref SystemState state)
        {
            entityManager = state.EntityManager;

            GetPlayerEntity();
            GetHotBarComponent();
            SetDynamicBufferToPlayer();

        }

        private void SetDynamicBufferToPlayer()
        {
            if (!entityManager.HasBuffer<ItemSlotBufferElement>(playerEntity))
            {
                Debug.Log("PlayerEntity is not have ItemSlotBufferElement");
                entityManager.AddBuffer<ItemSlotBufferElement>(playerEntity);
                Debug.Log("Add Buffer, PlayerEntiy has ItemSlotBufferElement");
                SetDefaultHotBarBuffer();
            }
        }

        private void SetDefaultHotBarBuffer()
        {
            DynamicBuffer<ItemSlotBufferElement> hotBarBuffer = entityManager.GetBuffer<ItemSlotBufferElement>(playerEntity);
            for (int i = 0; i < hotBarSizeComponent.Size; i++)
            {
                hotBarBuffer.Add(new ItemSlotBufferElement());
            }
        }

        private void GetHotBarComponent()
        {
            if (hotBarSizeComponent.Size <= 0)
            {
                Debug.Log("HotBarComponent size is lower than 0");
                hotBarSizeComponent = entityManager.GetComponentData<HotBarSize>(playerEntity);
                Debug.Log("Get HotBarComponent, HotBarComponent size is " + hotBarSizeComponent.Size);
            }
        }

        private void GetPlayerEntity()
        {
            if (playerEntity == Entity.Null)
            {
                Debug.Log("Player entity is empty");
                playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
                Debug.Log("Get player entity, Player entity is " + playerEntity.ToFixedString());
            }
        }
    }
}

