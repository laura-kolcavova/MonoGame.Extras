namespace MonoGame.Extras.Ecs.Engine.Interaction
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Reflection;

    public class Interaction2DComponent : IEntityComponent
    {
        // Properties
        public bool BeingDragged { get; set; }

        public bool BeingPressed { get; set; }

        public bool IsDraggable { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Size { get; set; }

        public Rectangle Bounds => new Rectangle(Origin.ToPoint(), Size.ToPoint());

        /// <summary>
        /// The event occurs when the pointer is moved onto an entity
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;

        /// <summary>
        /// The event occurs when the pointer is moved out of an entity
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;

        /// <summary>
        /// 	The event occurs when the pointer is moving while it is over an entity
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;

        /// <summary>
        /// The event occurs when any mouse button is pressed over an entity
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDown;

        /// <summary>
        /// The event occurs when any mouse button is released over an entity
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseUp;

        /// <summary>
        /// The event occurs when left mouse button is clicked on an entity
        /// </summary>
        public event EventHandler<MouseEventArgs> Click;

        /// <summary>
        /// The event occurs when the user starts to drag an entity
        /// </summary>
        public event EventHandler<DragEventArgs> DragStart;

        /// <summary>
        /// The event occurs when the user has finished dragging an entity
        /// </summary>
        public event EventHandler<DragEventArgs> DragEnd;

        /// <summary>
        /// The event occurs when an entity is being dragged
        /// </summary>
        public event EventHandler<DragEventArgs> DragMove;

        public void TriggerEvent<TEventArgs>(string eventName, TEventArgs eventArgs)
        {
            var eventInfo = this.GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (eventInfo != null)
            {
                var event_member = eventInfo.GetValue(this);
                // Note : If event_member is null, nobody registered to the event, you can't call it.
                if (event_member != null)
                    event_member.GetType().GetMethod("Invoke").Invoke(event_member, new object[] { this, eventArgs });
            }
        }

        protected virtual void OnClick(MouseEventArgs e)
        {
            Click?.Invoke(this, e);
        }

        protected virtual void OnDragEnd(DragEventArgs e)
        {
            DragEnd?.Invoke(this, e);
        }

        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }

        protected virtual void OnMouseEnter(MouseEventArgs e)
        {
            MouseEnter?.Invoke(this, e);
        }

        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }

        protected virtual void OnDragStart(DragEventArgs e)
        {
            DragStart?.Invoke(this, e);
        }

        protected virtual void OnDragMove(DragEventArgs e)
        {
            DragMove?.Invoke(this, e);
        }

        protected virtual void OnMouseLeave(MouseEventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }
    }
}