namespace RPG.Inventory
{
    using System;
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using UnityEngine;
    
    [Serializable]
    public class HotBarHolderAuthoring : MonoBehaviour
    {
        public int Size;

        public class InventoryHolderBaker : Baker<HotBarHolderAuthoring>
        {
            public override void Bake(HotBarHolderAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                AddComponentObject(entity, new HotBar{
                    InventorySize = authoring.Size,
                });
            }
        }
    }
}