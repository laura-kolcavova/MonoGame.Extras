namespace MonoGame.Extras.Ecs.Engine.Physics
{
    using Microsoft.Xna.Framework;
    using MonoGame.Extras;
    using MonoGame.Extras.Ecs.Systems;

    public class PhysicsSystem : EntitySystem, IUpdateSystem
    {
        private ComponentMapper<Transform2DComponent> _transform2DMapper;
        private ComponentMapper<RigidBody2DComponent> _physics2DMapper;

        public PhysicsSystem() : base(Aspect
            .All(typeof(Transform2DComponent), typeof(RigidBody2DComponent)))
        {

        }

        public override void Initialize(IComponentMapperService componentService)
        {
            _transform2DMapper = componentService.GetMapper<Transform2DComponent>();
            _physics2DMapper = componentService.GetMapper<RigidBody2DComponent>();
        }

        public void Update(GameTime gameTime)
        {
            float delta = gameTime.GetElapsedSeconds();

            foreach(int entityId in ActiveEntities)
            {
                var transform2D = _transform2DMapper.Get(entityId);
                var physics2D = _physics2DMapper.Get(entityId);


                if (physics2D.Velocity != Vector2.Zero)
                {
                    transform2D.Position += physics2D.Velocity * delta;
                }
            }
        }


        //public void MoveToPosition(Vector2 target, float speed)
        //{
        //    Transform.SetPosition(Vector2Ext.MoveTowards(Transform.Position, target, speed));
        //}
    }
}
