namespace RPG.Player
{
    using Unity.Entities;
    using UnityEngine;

    public class InputAuthoring : MonoBehaviour
    {
        public Vector2 Movement{ get; private set; }

        public class InputBaker : Baker<InputAuthoring>
        {
            public override void Bake(InputAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new InputComponent
                {
                    Movement = authoring.Movement,
                });
            }
        }
    }
}