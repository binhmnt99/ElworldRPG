namespace RPG.Player
{
    using UnityEngine;
    using Unity.Entities;
    using Unity.Burst;

    [BurstCompile]
    public partial struct PlayerControllerSystem : ISystem
    {
        private Entity playerEntity;
        private EntityManager entityManager;
        private InputComponent inputComponent;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputComponent>();
            entityManager = state.EntityManager;

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery entityQuery = entityManager.CreateEntityQuery(typeof(InputComponent));
            entityQuery.TryGetSingletonEntity<InputComponent>(out Entity entity);
            playerEntity = entity;
            Debug.Log(entity);
        }
    }
}