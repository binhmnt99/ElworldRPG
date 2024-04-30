namespace RPG.Inventory
{
    using System;
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using UnityEngine;
    
    [Serializable]
    public class HotBarSizeAuthoring : MonoBehaviour
    {
        public int Size;

        public class HotBarSizeBaker : Baker<HotBarSizeAuthoring>
        {
            public override void Bake(HotBarSizeAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new HotBarSize{
                    Size = authoring.Size,
                });
            }
        }
    }
}