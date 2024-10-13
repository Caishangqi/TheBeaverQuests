using UnityEngine;

namespace Core.UI.Event
{
    public class PressCubeInteractWidgetEvent
    {
        public GameObject Cube;

        public PressCubeInteractWidgetEvent(GameObject cube)
        {
            this.Cube = cube;
        }
    }
}