using System;
using UnityEngine;

namespace Core.UI.Event
{
    public class UIEvent
    {
        public static Action<CubePickUpEvent> CubePickUpEvent;
        
        public static Action<PauseClickedEvent> PauseClickedEvent;
        public static Action<ResumeClickedEvent> ResumeClickedEvent;
        public static Action<BackToMainMenuEvent> BackToMainMenuEvent;
        
        public static Action<PressCubeInteractWidgetEvent> PressCubeInteractWidgetEvent;
        public static Action<GeneratedButtonClickedEvent> GeneratedButtonClickedEvent;
        
        public static Action<MovingCheckerOKEvent> MovingCheckerOKEvent;
        public static Action<MovingCheckerNOTEvent> MovingCheckerNOTEvent;

        public static Action<StartEvent> StartEvent;
        public static Action<CreditEvent> CreditEvent;
        public static Action<QuitEvent> QuitEvent;
        public static Action<BackButtonEvent> BackButtonEvent;
        public static Action<SettingEvent> SettingEvent;
        
        public static Action<InventoryCollectEvent> InventoryCollectEvent;

        public static Action<ShowHammerButtonEvent> ShowHammerButtonEvent;
        public static Action<SetHammerRelativeComponentsEnabledEvent> SetHammerRelativeComponentsEnabledEvent;
        
        public static Action<SelectLevelButtonEvent> SelectLevelButtonEvent;

        public static Action<ShowStoryBoardEvent> ShowStoryBoardEvent;

        public static Action<ShowVictoryEvent> ShowVictoryEvent;
    }
}