using System;
using UnityEngine;

namespace Core.Entity.Portal.Events
{
    public struct PortalRequestSceneLoadEvent
    {
        public PortalView portalView { get; set; }
        public String sceneName { get; set; }

        public PortalRequestSceneLoadEvent(PortalView portalView, String sceneName)
        {
            this.portalView = portalView;
            this.sceneName = sceneName;
        }
    }
}