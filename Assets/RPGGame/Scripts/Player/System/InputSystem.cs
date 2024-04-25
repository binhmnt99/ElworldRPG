namespace RPG.Player
{
    using UnityEngine;
    using Unity.Entities;

    public partial class InputSystem : SystemBase
    {
        private PlayerControls controls;

        protected override void OnCreate()
        {
            RequireForUpdate<InputComponent>();
            controls = new();
            controls?.Enable();
        }
        protected override void OnUpdate()
        {
            Vector2 move = controls.PlayerMap.Movement.ReadValue<Vector2>();

            SystemAPI.SetSingleton(new InputComponent{
                Movement = move,
            });
        }

        protected override void OnDestroy()
        {
            controls?.Disable();
        }
    }
}