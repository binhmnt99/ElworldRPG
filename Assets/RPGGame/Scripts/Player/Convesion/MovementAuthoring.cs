namespace RPG.Player
{
    using Unity.Entities;
    using UnityEngine;

    public class MovementAuthoring : MonoBehaviour
    {
        public float Speed;

        public class MovementBaker : Baker<MovementAuthoring>
        {
            public override void Bake(MovementAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new MovementSpeedComponent
                {
                    Value = authoring.Speed,
                });
            }
        }
    }
}