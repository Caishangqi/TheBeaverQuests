using System.Collections;
using System.Collections.Generic;
using Core.Game.AudioManager;
using Core.Game.AudioManager.Data;
using Core.Game.AudioManager.Events;
using UnityEngine;

public class PlateTriggeredHandler
{
    public PlateView PlateView;

    public PlateTriggeredHandler(PlateView PlateView)
    {
        this.PlateView = PlateView;
        PlateEvent.PlateTriggeredEvent += OnPlateTriggeredEvent;
    }

    public void HandlePlateUnTriggered(Collider2D other)
    {
        //Debug.Log("处理玩家将箱子从压力板移除 PlateView" + other.gameObject.name);
        //Debug.Log("压力板上的对象: " + PlateView.coveredGameObject.name + "离开的对象: " + other.gameObject.name);
        if (PlateView.coveredGameObject == other.gameObject)
        {
            if ((other.CompareTag("Cube")))
            {
                PlateView.coveredGameObject = null;
                PlateView.isCovered = false;
                PlateEvent.PlateUnTriggeredEvent?.Invoke(new PlateUnTriggeredEvent(
                    PlateView.gameObject.transform.position,
                    other.gameObject, PlateView.acceptCubeColor, PlateView.plateId)); //发送事件
                AudioEvent.PlaySoundEvent.Invoke(new PlaySoundEvent(this.PlateView.gameObject,
                    ESound.ENTITY_PLATE_RELEASED));
                PlateView.UpdatePlateVisual(); // 更新压力板状态

                //Debug.Log("处理玩家将箱子从压力板移除 PlateView.coveredGameObject = null");
            }
        }
    }

    public void HandlePlateTriggered(Collider2D other)
    {
        if (PlateView.coveredGameObject == null && other.CompareTag("Cube"))
        {
            PlateView.m_cubeCollider = other.GetComponent<BoxCollider2D>();

            CubeView cubeView = other.GetComponent<CubeView>();
            bool IsCubeHeld = cubeView.IsHeld;
            bool IsSameColor = false;
            if (cubeView)
            {
                IsSameColor = cubeView.cubeColor == PlateView.acceptCubeColor;

                if (IsSameColor && !IsCubeHeld)
                {
                    PlateView.isCovered = true;
                    PlateView.coveredGameObject = other.gameObject;
                    PlateView.UpdatePlateVisual(); // 更新压力板状态

                    PlateEvent.PlateTriggeredEvent?.Invoke(new PlateTriggeredEvent(
                        PlateView.gameObject.transform.position,
                        other.gameObject, PlateView.acceptCubeColor, PlateView.plateId));
                    AudioEvent.PlaySoundEvent.Invoke(new PlaySoundEvent(this.PlateView.gameObject,
                        ESound.ENTITY_PLATE_PRESSED));
                }
                else
                {
                    return;
                }
            }
        }
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

    public void OnDestroy()
    {
        PlateEvent.PlateTriggeredEvent -= OnPlateTriggeredEvent;
    }
}