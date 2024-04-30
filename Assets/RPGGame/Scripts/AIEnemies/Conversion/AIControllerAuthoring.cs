namespace RPG.AI
{
    using Unity.Entities;
    using UnityEngine;
    
    public class AIControllerAuthoring : MonoBehaviour
    {
        public AIController AIController;
        public EnemyTag EnemyTag;

        public class AIControllerBaker : Baker<AIControllerAuthoring>
        {
            public override void Bake(AIControllerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,authoring.EnemyTag);
                AddComponent(entity,authoring.AIController);
            }
        }
    }
}