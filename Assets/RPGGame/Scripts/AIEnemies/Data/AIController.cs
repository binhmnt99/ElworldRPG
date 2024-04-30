namespace RPG.AI
{
    using System;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Physics.Authoring;

    [Serializable]
    public struct AIController : IComponentData
    {
        public float DetectionDistance;
        public float FOVAngle;
        public CustomPhysicsBodyTags DetectionFilter;
    }

    [Serializable]
    public struct EnemyTag : IComponentData{}
}