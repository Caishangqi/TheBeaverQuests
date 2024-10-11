using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCoveredHandler
{
    public PlateView PlateView;

    public PlateCoveredHandler(PlateView PlateView)
    {
        this.PlateView = PlateView;
        PlateEvent.PlateCoveredEvent += OnPlateCoveredEvent;
    }

    // 处理覆盖事件
    private void OnPlateCoveredEvent(PlateCoveredEvent PlateCoveredEvent)
    {
        Debug.Log($"{PlateCoveredEvent:gameObject} has covered the pressure plate.");
        // 在这里实现更多的业务逻辑，例如改变场景状态或触发其他行为
    }

    ~PlateCoveredHandler()
    {
        PlateEvent.PlateCoveredEvent -= OnPlateCoveredEvent;
    }
}
