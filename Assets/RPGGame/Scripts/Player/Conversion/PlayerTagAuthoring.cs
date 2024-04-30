namespace RPG.Player
{
    using Unity.Entities;
    using UnityEngine;
    
    public class PlayerTagAuthoring : MonoBehaviour
    {
        public class PlayerTagBaker : Baker<PlayerTagAuthoring>
        {
            public override void Bake(PlayerTagAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PlayerTag{});
            }
        }
    }
}
