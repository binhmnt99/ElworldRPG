namespace RPG.AI
{
    using System.Drawing;
    using Unity.Burst;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Physics;
    using Unity.Transforms;
    using UnityEngine;

    [BurstCompile]
    public partial struct AIControllerSystem : ISystem
    {
        private EntityQuery entityQuery;

        private ThirdPersonCharacterControl thirdControl;
        private AIController aiController;
        private LocalTransform localTransform;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            entityQuery = state.GetEntityQuery(ComponentType.ReadOnly<EnemyTag>());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            PhysicsWorld physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld;
            NativeList<DistanceHit> distanceHits = new NativeList<DistanceHit>(Allocator.TempJob);

            foreach (var entity in entityQuery.ToEntityArray(Allocator.TempJob))
            {
                distanceHits.Clear();

                thirdControl = state.EntityManager.GetComponentData<ThirdPersonCharacterControl>(entity);
                aiController = state.EntityManager.GetComponentData<AIController>(entity);
                localTransform = state.EntityManager.GetComponentData<LocalTransform>(entity);
                //Debug.Log(string.Format("{0}",control.DetectionDistance));
                AllHitsCollector<DistanceHit> allHitsCollector = new AllHitsCollector<DistanceHit>(aiController.DetectionDistance, ref distanceHits);

                PointDistanceInput distanceInput = new PointDistanceInput
                {
                    Position = localTransform.Position,
                    MaxDistance = aiController.DetectionDistance,
                    Filter = new CollisionFilter { BelongsTo = CollisionFilter.Default.BelongsTo, CollidesWith = aiController.DetectionFilter.Value },
                };
                physicsWorld.CalculateDistance(distanceInput, ref allHitsCollector);

                Entity selectedTarget = Entity.Null;

                for (int i = 0; i < allHitsCollector.NumHits; i++)
                {
                    Entity hitEntity = distanceHits[i].Entity;
                    //Debug.Log(hitEntity.ToFixedString());
                }
            }

            // foreach (var (characterControl, aiController, localTransform) in SystemAPI.Query<RefRW<ThirdPersonCharacterControl>, AIController, LocalTransform>())
            // {
            //     // Clear our detected hits list between each use
            //     distanceHits.Clear();

            //     // Create a hit collector for the detection hits
            //     AllHitsCollector<DistanceHit> hitsCollector = new AllHitsCollector<DistanceHit>(aiController.DetectionDistance, ref distanceHits);

            //     // Detect hits that are within the detection range of the AI character
            //     PointDistanceInput distInput = new PointDistanceInput
            //     {
            //         Position = localTransform.Position,
            //         MaxDistance = aiController.DetectionDistance,
            //         Filter = new CollisionFilter { BelongsTo = CollisionFilter.Default.BelongsTo, CollidesWith = aiController.DetectionFilter.Value },
            //     };
            //     physicsWorld.CalculateDistance(distInput, ref hitsCollector);

            //     // Iterate on all detected hits to try to find a human-controlled character...
            //     Entity selectedTarget = Entity.Null;
            //     for (int i = 0; i < hitsCollector.NumHits; i++)
            //     {

            //         Entity hitEntity = distanceHits[i].Entity;


            //         if (IsWithinFOV(localTransform.Position, SystemAPI.GetComponent<LocalTransform>(hitEntity).Position, aiController.FOVAngle))
            //         {
            //             //Debug.Log("Detected");
            //             if (SystemAPI.HasComponent<ThirdPersonCharacterComponent>(hitEntity) && !SystemAPI.HasComponent<AIController>(hitEntity))
            //             {
            //                 selectedTarget = hitEntity;
            //             }
            //             break;
            //         }

            //         // //If it has a character component but no AIController component, that means it's a human player character
            //         // if (SystemAPI.HasComponent<ThirdPersonCharacterComponent>(hitEntity) && !SystemAPI.HasComponent<AIController>(hitEntity))
            //         // {
            //         //     selectedTarget = hitEntity;
            //         //     break; // early out
            //         // }
            //     }

            //     Debug.Log(selectedTarget.ToFixedString());

            //     // In the character control component, set a movement vector that will make the ai character move towards the selected target
            //     if (selectedTarget != Entity.Null)
            //     {
            //         characterControl.ValueRW.MoveVector = math.normalizesafe(SystemAPI.GetComponent<LocalTransform>(selectedTarget).Position - localTransform.Position);
            //     }
            //     else
            //     {
            //         characterControl.ValueRW.MoveVector = float3.zero;
            //     }
            //}
        }

        // Function to check if a position is within FOV
        private bool IsWithinFOV(float3 origin, float3 target, float angle)
        {
            float3 direction = math.normalize(target - origin);
            float3 targetDirection = math.normalize(math.forward() - origin);
            float angleToTarget = math.degrees(math.acos(math.dot(direction, math.forward())));
            return angleToTarget <= angle * 0.5f;
        }
    }
}