using Unity.VisualScripting;

namespace RPG.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Unity.Collections;
    using Unity.Entities;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class BackPack : IComponentData
    {
        public int InventorySize;
        public List<ItemSlot> ItemSlots;
    }

    [Serializable]
    public class HotBar : IComponentData
    {
        public int InventorySize;
        public List<ItemSlot> ItemSlots;
    }

    [Serializable]
    public struct ItemSlot
    {
        public ItemData ItemData;
        public int StackSize;


    }

    //[Serializable]
    public struct ItemData
    {
        public FixedString64Bytes ItemID;
        public FixedString64Bytes ItemName;
        public FixedString128Bytes ItemDescription;
        public Entity ImageEntity;
        public int MaxStackSize;

        public void Empty()
        {
            ItemID = null;
            ItemName = null;
            ItemDescription = null;
            ImageEntity = Entity.Null;
            MaxStackSize = -1;
        }

        public override bool Equals(object obj) => obj is ItemData itemSlotData
        && itemSlotData.ItemID == ItemID
        && itemSlotData.ItemName == ItemName
        && itemSlotData.ItemDescription == ItemDescription
        && itemSlotData.ImageEntity == ImageEntity
        && itemSlotData.MaxStackSize == MaxStackSize;

        public bool Equals(ItemData other) => this == other;
        public override int GetHashCode() => HashCode.Combine(ItemID, ItemName, ItemDescription, ImageEntity, MaxStackSize);
        public override string ToString() => $"ID: {ItemID} - Name: {ItemName} - Desc: {ItemDescription} - Image: {ImageEntity} - MaxSize: {MaxStackSize}";

        public static bool operator ==(ItemData a, ItemData b) => a.ItemID == b.ItemID
        && a.ItemName == b.ItemName
        && a.ItemDescription == b.ItemDescription
        && a.ImageEntity == b.ImageEntity
        && a.MaxStackSize == b.MaxStackSize;

        public static bool operator !=(ItemData a, ItemData b) => !(a == b);
    }
}