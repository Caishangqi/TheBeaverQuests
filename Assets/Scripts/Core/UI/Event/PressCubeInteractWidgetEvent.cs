using Core.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Event
{
    public class PressCubeInteractWidgetEvent
    {

        public InteractableView CurrentInteractable;
        public Canvas Canvas;
        public Vector2 Offset;
        public Button PickUpButton;

        public PressCubeInteractWidgetEvent(InteractableView currentInteractable, Canvas canvas, Vector2 offset, Button pickUpButton)
        {
            this.CurrentInteractable = currentInteractable;
            this.Canvas = canvas;
            this.Offset = offset;
            this.PickUpButton = pickUpButton;
        }
    }
}