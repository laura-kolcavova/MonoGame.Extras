namespace MonoGame.Extras.Ecs.Engine.Interaction
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using MonoGame.Extras.Math;
    using MonoGame.Extras.Input;
    using MonoGame.Extras.Ecs.Systems;
    using MonoGame.Extras.ViewportAdapters;
    using MonoGame.Extras.Ecs.Engine.Physics;
    using MonoGame.Extras.Interaction;

    public class InteractionSystem : EntitySystem, IUpdateSystem
    {
        private readonly ViewportAdapter _viewportAdapter;

        public InteractionSystem(ViewportAdapter viewportAdapter = null) : base (Aspect
            .All(typeof(Interaction2DComponent), typeof(Transform2DComponent)))
        {
            _viewportAdapter = viewportAdapter;

            current_pressedButtons = Array.Empty<MouseButtons>();
            prev_pressedButtons = Array.Empty<MouseButtons>();
        }

        private ComponentMapper<Interaction2DComponent> _interaction2DMapper;
        private ComponentMapper<Transform2DComponent> _transform2DMapper;

        private MouseState current_mouseState;
        private MouseState prev_mouseState;
        private MouseButtons[] current_pressedButtons;
        private MouseButtons[] prev_pressedButtons;
        private Point _dragStartPosition;

        public override void Initialize(IComponentMapperService componentService)
        {
            _interaction2DMapper = componentService.GetMapper<Interaction2DComponent>();
            _transform2DMapper = componentService.GetMapper<Transform2DComponent>();
        }

        public void Begin()
        {
            current_mouseState = Mouse.GetState();
            current_pressedButtons = current_mouseState.GetPressedButtons();
        }

        public void End()
        {
            prev_mouseState = current_mouseState;
            prev_pressedButtons = current_pressedButtons;
        }

        public void Update(GameTime gameTime)
        {
            Begin();

            foreach(int entityId in ActiveEntities)
            {
                Process(entityId);
            }

            End();
        }

        public void Process(int entityId)
        {
            var transform = _transform2DMapper.Get(entityId);
            var interaction = _interaction2DMapper.Get(entityId);

            // Check for Events
            bool isHovered = IsEntityHovered(transform, interaction, current_mouseState);
            bool wasHovered = IsEntityHovered(transform, interaction, prev_mouseState);

            // Interaction properties
            bool beingPressed = interaction.BeingPressed;
            bool beingDragged = interaction.BeingDragged;

            // MouseEnter
            if (CheckMouseEnter(isHovered, wasHovered))
            {
                interaction.TriggerEvent("MouseEnter", BuildMouseEvent(entityId));
            }

            // MouseLeave
            if (CheckMouseLeave(isHovered, wasHovered))
            {
                interaction.TriggerEvent("MouseLeave", BuildMouseEvent(entityId));
            }

            // MouseMove
            if (CheckMouseMove(isHovered))
            {
                interaction.TriggerEvent("MouseMove", BuildMouseEvent(entityId));
            }

            // MouseDown
            if (CheckMouseDown(isHovered))
            {
                // Set Being Pressed Property
                if(!beingPressed && current_mouseState.LeftButton == ButtonState.Pressed)
                {
                    interaction.BeingPressed = true;
                }

                interaction.TriggerEvent("MouseDown", BuildMouseEvent(entityId));
            }

            // MouseUp
            if (CheckMouseUp(isHovered))
            {
                interaction.TriggerEvent("MouseUp", BuildMouseEvent(entityId));
            }

            // Click
            if (CheckClick(beingPressed, isHovered))
            {
                interaction.TriggerEvent("Click", BuildMouseEvent(entityId));
            }

            // DragStart
            if (CheckDragStart(beingDragged, beingPressed, isHovered))
            {
                // Set Being Dragged Property
                interaction.BeingDragged = true;

                if (interaction.IsDraggable)
                {
                    var mousePosition =
                        _viewportAdapter == null ?
                        current_mouseState.Position : _viewportAdapter.PointToScreen(current_mouseState.Position);

                    var pos = Vector2
                        .Transform(interaction.Origin, transform.WorldMatrix)
                        .ToPoint();

                    _dragStartPosition = mousePosition - pos;
                }

                interaction.TriggerEvent("DragStart", BuildDragEvent(entityId));
            }

            // DragEnd
            if (CheckDragEnd(beingDragged))
            {
                // Set Being Dragged Property
                interaction.BeingDragged = false;
                _dragStartPosition = Point.Zero;

                interaction.TriggerEvent("DragEnd", BuildDragEvent(entityId));
            }

            // DragMove
            if (CheckDragMove(beingDragged))
            {
                // Move Entity if IsDraggable = true
                if(interaction.IsDraggable)
                {
                    var mousePosition =
                       _viewportAdapter == null ?
                       current_mouseState.Position : _viewportAdapter.PointToScreen(current_mouseState.Position);

                    var newPos = (mousePosition - _dragStartPosition).ToVector2();

                    newPos -= (transform.WorldPosition - transform.Position);

                    transform.Position = newPos;
                }

                interaction.TriggerEvent("DragMove", BuildDragEvent(entityId));
            }

            // DragEnter

            // DragLeave

            // Drop

            // BeingPressed - Check if Released
            if(beingPressed && current_mouseState.LeftButton == ButtonState.Released)
            {
                interaction.BeingPressed = false;
            }
        }

        #region Check Event Methods

        // Mouse Event Check Methods

        private bool CheckMouseEnter(bool isHovered, bool wasHovered)
        {
            return isHovered && !wasHovered;
        }

        private bool CheckMouseLeave(bool isHovered, bool wasHovered)
        {
            return !isHovered && wasHovered;
        }

        private bool CheckMouseMove(bool isHovered)
        {
            return isHovered && current_mouseState.Position != prev_mouseState.Position;
        }

        private bool CheckMouseDown(bool isHovered)
        {
            return
                isHovered &&
                current_pressedButtons.Length > 0 &&
                prev_pressedButtons.Length == 0;
        }

        private bool CheckMouseUp(bool isHovered)
        {
            return
                isHovered &&
                current_pressedButtons.Length == 0 &&
                prev_pressedButtons.Length > 0;
        }

        private bool CheckClick(bool beingPressed, bool isHovered)
        {
            return beingPressed && isHovered && current_mouseState.LeftButton == ButtonState.Released;
        }

        private bool CheckDragStart(bool beingDragged, bool beingPressed, bool isHovered)
        {
            return !beingDragged && beingPressed && isHovered && current_mouseState.Position != prev_mouseState.Position;
        }

        private bool CheckDragEnd(bool beingDragged)
        {
            return beingDragged && current_mouseState.LeftButton == ButtonState.Released;
        }

        private bool CheckDragMove(bool beingDragged)
        {
            return beingDragged && current_mouseState.Position != prev_mouseState.Position;
        }

        #endregion

        #region Build EventArgs Methods

        private MouseEventArgs BuildMouseEvent(int entityId)
        {
            var button = current_pressedButtons.Length > 0 ? current_pressedButtons[0] : MouseButtons.None;

            return new MouseEventArgs()
            {
                TargetId = entityId,
                MouseState = current_mouseState,
                Button = button,
                Buttons = current_pressedButtons
            };
        }

        private DragEventArgs BuildDragEvent(int entityId)
        {
            var button = current_pressedButtons.Length > 0 ? current_pressedButtons[0] : MouseButtons.None;

            return new DragEventArgs()
            {
                TargetId = entityId,
                MouseState = current_mouseState,
                Button = button,
                Buttons = current_pressedButtons
            };
        }

        #endregion

        #region Helper Methods

        public bool IsEntityHovered(Transform2DComponent transform2D, Interaction2DComponent interaction2D, MouseState mouseState)
        {
            var bounds = interaction2D.Bounds.Transform(transform2D.WorldMatrix);

            var mousePosition =
                _viewportAdapter == null ?
                mouseState.Position : _viewportAdapter.PointToScreen(mouseState.Position);

            return bounds.Intersects(mousePosition);
        }

        #endregion
    }
}
