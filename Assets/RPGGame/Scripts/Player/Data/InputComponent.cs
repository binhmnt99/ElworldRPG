namespace RPG.Player
{
    using Unity.Entities;
    using UnityEngine;

    public struct InputComponent : IComponentData
    {
        public Vector2 Movement;
    }
}