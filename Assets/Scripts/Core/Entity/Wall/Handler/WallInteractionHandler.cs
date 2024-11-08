using System.Linq;
using Core.Game.AudioManager;
using Core.Game.AudioManager.Data;
using Core.Game.AudioManager.Events;
using UnityEngine;

namespace Core.Entity.Wall.Handler
{
    public class WallInteractionHandler
    {
        WallView view;


        public WallInteractionHandler(WallView view)
        {
            this.view = view;
            PlateEvent.PlateTriggeredEvent += OnPlateTriggered;
            PlateEvent.PlateUnTriggeredEvent += OnPlateUnTriggered;
        }

        public bool CheckRequiredIndex()
        {
            return view.requiredIndex.All(view.containedIndex.Contains);
        }

        public void OnPlateTriggered(PlateTriggeredEvent plateTriggeredEvent)
        {
            if (!view.containedIndex.Contains(plateTriggeredEvent.plateId))
            {
                view.containedIndex.Add(plateTriggeredEvent.plateId);
            }

            if (CheckRequiredIndex())
            {
                view.DisableWallCollision();
                AudioEvent.PlaySoundEvent.Invoke(new PlaySoundEvent(this.view.gameObject,
                    ESound.ENTITY_WALL_COLLAPSED));
                Debug.Log("OnPlateTriggered");
            }
        }

        public void OnPlateUnTriggered(PlateUnTriggeredEvent plateUnTriggeredEvent)
        {
            view.containedIndex.Remove(plateUnTriggeredEvent.plateIndex);

            if (!CheckRequiredIndex())
            {
                view.EnableWallCollision();
                Debug.Log("OnPlateUnTriggered");
            }
        }

        public void OnDestroy()
        {
            PlateEvent.PlateTriggeredEvent -= OnPlateTriggered;
            PlateEvent.PlateUnTriggeredEvent -= OnPlateUnTriggered;
        }
    }
}