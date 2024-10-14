using Core.Character;
using UnityEngine;

namespace Core.UI.Event
{
    public class GeneratedButtonClickedEvent
    {
        //GameObject Cube;
        
        public InteractableView interactableView;

        public GeneratedButtonClickedEvent(InteractableView interactableView)
        {
            this.interactableView = interactableView;
        }
    }
}