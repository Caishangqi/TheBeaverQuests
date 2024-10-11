using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTriggeredHandler
{
    public PlateView PlateView;

    public PlateTriggeredHandler(PlateView PlateView)
    {
        this.PlateView = PlateView;
        PlateEvent.PlateTriggeredEvent += OnPlateTriggeredEvent;
    }

    //处理trigger事件
    private void OnPlateTriggeredEvent(PlateTriggeredEvent PlateTriggeredEvent)
    {
        Debug.Log($"{PlateTriggeredEvent:instigator.name} has triggered the pressure plate.");
        // 在这里实现业务逻辑，例如打开门或激活机关
    }

    ~PlateTriggeredHandler()
    {
        PlateEvent.PlateTriggeredEvent -= OnPlateTriggeredEvent;
    }
    
}
