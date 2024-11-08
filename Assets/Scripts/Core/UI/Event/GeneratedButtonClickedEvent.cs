using Core.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Event
{
    public class GeneratedButtonClickedEvent
    {
        public InteractableView interactableView;
        
        //public GameObject generatedButton;

        public GeneratedButtonClickedEvent(InteractableView interactableView)
        {
            //this.generatedButton = generatedButton;
            this.interactableView = interactableView;
        }
        
    }
}