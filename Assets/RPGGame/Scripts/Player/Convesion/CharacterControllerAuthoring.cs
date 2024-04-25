namespace RPG.Player
{
    using Unity.Entities;
    using UnityEngine;

    public class CharacterControllerAuthoring : MonoBehaviour
    {
        public CharacterController characterController;

        public class CharacterControllerBaker : Baker<CharacterControllerAuthoring>
        {
            public override void Bake(CharacterControllerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new CharacterControllerComponent
                {
                    CharacterController = GetEntity(authoring.characterController,TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}