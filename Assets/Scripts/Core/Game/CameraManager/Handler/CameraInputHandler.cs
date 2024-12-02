using System.Collections;
using Core.Game.ControllerModule.Gesture;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Collections;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace Core.Game.CameraManager.Handler
{
    public class CameraInputHandler
    {
        CameraView cameraView;
        
        private float initialZoom;
        private UnityEngine.Vector2 previousTouchDistance;
        private UnityEngine.Vector3 previousMousePosition;
        private UnityEngine.Vector3 initialPosition; // 记录摄像机初始位置
        private bool isReturningToInitialPosition = false; // 标识是否正在返回初始位置
        private float distanceThreshold = 10f; // 用于区分拖动和缩放的距离阈值

        public CameraInputHandler(CameraView cameraView)
        {
            this.cameraView = cameraView;
            initialPosition = cameraView.mainCamera.transform.position; // 初始化摄像机位置

            //DamEvent.PlayWaterDisappearAnimEvent += OnPlayerWaterDisappearAnimEvent;
        }

        // private void OnPlayerWaterDisappearAnimEvent(PlayWaterDisappearAnimEvent obj)
        // {
        //     cameraView.StartCoroutine(ZoomToSize(cameraView.maxZoom, 6f)); 
        // }

        // 检查和处理双指手势的主方法
        public void HandleTouchInput()
        {
            if (Input.touchCount == 2)
            {
                GestureManager.Instance.IsTwoFingerGestureActive = true;
                
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                // 当手势开始时初始化缩放
                if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
                {
                    //initialZoom = cameraView.mainCamera.orthographicSize;
                    previousTouchDistance = touch1.position - touch2.position;
                    isReturningToInitialPosition = false;
                }

                // 当手势移动时处理缩放和拖动
                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    HandlePinchZoom(touch1, touch2);
                    HandleDrag(touch1, touch2);
                    // UnityEngine.Vector2 currentTouchDistance = touch1.position - touch2.position;
                    // float distanceChange = Mathf.Abs(currentTouchDistance.magnitude - previousTouchDistance.magnitude);
                    //
                    // // 根据距离变化来判断是缩放还是拖动
                    // if (distanceChange > distanceThreshold)
                    // {
                    //     HandlePinchZoom(touch1, touch2); // 如果距离变化大于阈值，则执行缩放
                    // }
                    // else
                    // {
                    //     HandleDrag(touch1, touch2); // 否则执行拖动
                    // }
                    //
                    // previousTouchDistance = currentTouchDistance; // 更新距离
                    
                    Debug.Log("Has banned 1 finger moving");
                }
            }
            else //处理gestures
            {
                GestureManager.Instance.IsTwoFingerGestureActive = false; // 解除双指操作
            }
            
            // 处理鼠标左键和右键同时按下
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                HandleMouseDrag();
                GestureManager.Instance.IsTwoFingerGestureActive = true;
                Debug.Log("Has banned 1 finger moving");
            }
            else //处理gestures
            {
                GestureManager.Instance.IsTwoFingerGestureActive = false; // 解除双指操作
            }
            
            if (!isReturningToInitialPosition)
            {
                isReturningToInitialPosition = true;
                cameraView.StartCoroutine(cameraView.ResetZoomAfterDelay(cameraView.zoomRestoreDelay));
            }
        }

        
        // 处理双指缩放
        private void HandlePinchZoom(Touch touch1, Touch touch2)
        {
            // UnityEngine.Vector2 currentTouchDistance = touch1.position - touch2.position;
            // float distanceDelta = previousTouchDistance.magnitude - currentTouchDistance.magnitude;
            //
            // float targetZoom = initialZoom + distanceDelta * cameraView.zoomSpeed * Time.deltaTime;
            // targetZoom = Mathf.Clamp(targetZoom, cameraView.minZoom, cameraView.maxZoom);
            // cameraView.targetZoom = targetZoom;
            //
            // cameraView.cameraZoomEvent.currentZoom = targetZoom;
            // CameraEvent.CameraZoomEvent?.Invoke(cameraView.cameraZoomEvent);
            //
            // previousTouchDistance = currentTouchDistance;
            UnityEngine.Vector2 currentTouchDistance = touch1.position - touch2.position;
            float distanceDelta = previousTouchDistance.magnitude - currentTouchDistance.magnitude;

            float targetZoom = cameraView.mainCamera.orthographicSize + distanceDelta * cameraView.zoomSpeed * Time.deltaTime;
            cameraView.targetZoom = Mathf.Clamp(targetZoom, cameraView.minZoom, cameraView.maxZoom);

            previousTouchDistance = currentTouchDistance;
        }

        // 处理双指拖动
        private void HandleDrag(Touch touch1, Touch touch2)
        {
            // 使用 Vector2 而不是 int
            UnityEngine.Vector2 touchDeltaPosition = (touch1.deltaPosition + touch2.deltaPosition) / 2;
    
            // // 将 touchDeltaPosition 的 x 和 y 应用到相机的平移上
            // UnityEngine.Vector3 translation = new UnityEngine.Vector3(-touchDeltaPosition.x * cameraView.followSpeed * Time.deltaTime, 
            //     -touchDeltaPosition.y * cameraView.followSpeed * Time.deltaTime, 
            //     0);
            // cameraView.mainCamera.transform.Translate(translation);
            //
            // // 限制相机在 mapBounds 范围内
            // UnityEngine.Vector3 cameraPosition = cameraView.mainCamera.transform.position;
            // cameraPosition.x = Mathf.Clamp(cameraPosition.x, cameraView.mapBounds.min.x, cameraView.mapBounds.max.x);
            // cameraPosition.y = Mathf.Clamp(cameraPosition.y, cameraView.mapBounds.min.y, cameraView.mapBounds.max.y);
            // cameraView.mainCamera.transform.position = cameraPosition;
            
            // 计算阻力，根据相机位置与初始位置的距离来增加阻力
            UnityEngine.Vector3 currentPos = cameraView.mainCamera.transform.position;
            UnityEngine.Vector3 displacement = currentPos - initialPosition;
            float resistanceFactor = Mathf.Clamp(displacement.magnitude / cameraView.maxZoom, 0f, 1f); // 阻力系数0到1
            UnityEngine.Vector3 translation = new UnityEngine.Vector3(
                -touchDeltaPosition.x * cameraView.followSpeed * Time.deltaTime * (1 - resistanceFactor), 
                -touchDeltaPosition.y * cameraView.followSpeed * Time.deltaTime * (1 - resistanceFactor), 
                0);

            // 更新摄像机位置，考虑阻力
            //cameraView.mainCamera.transform.Translate(translation);
            cameraView.mainCamera.transform.position = UnityEngine.Vector3.Lerp(cameraView.mainCamera.transform.position, currentPos + translation, 0.5f);
            LimitCameraPosition();
        }
        
        private void HandleMouseDrag()
        {
            if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
            {
                previousMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                UnityEngine.Vector3 mouseDelta = Input.mousePosition - previousMousePosition;
                previousMousePosition = Input.mousePosition;

                // UnityEngine.Vector3 translation = new UnityEngine.Vector3(-mouseDelta.x * cameraView.followSpeed * Time.deltaTime, 
                //     -mouseDelta.y * cameraView.followSpeed * Time.deltaTime, 
                //     0);
                // cameraView.mainCamera.transform.Translate(translation);
                
                // 计算阻力，使用摄像机位置与初始位置的距离
                UnityEngine.Vector3 currentPos = cameraView.mainCamera.transform.position;
                UnityEngine.Vector3 displacement = currentPos - initialPosition;
                float resistanceFactor = Mathf.Clamp(displacement.magnitude / cameraView.maxZoom, 0f, 1f); // 阻力系数0到1

                // 计算带有阻力的平移量
                UnityEngine.Vector3 translation = new UnityEngine.Vector3(
                    -mouseDelta.x * cameraView.followSpeed * Time.deltaTime * (1 - resistanceFactor), 
                    -mouseDelta.y * cameraView.followSpeed * Time.deltaTime * (1 - resistanceFactor), 
                    0);

                cameraView.mainCamera.transform.Translate(translation);

                LimitCameraPosition();
            }
        }

        private void LimitCameraPosition()
        {
            UnityEngine.Vector3 cameraPosition = cameraView.mainCamera.transform.position;
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, cameraView.mapBounds.min.x - cameraView.boundsOffset, cameraView.mapBounds.max.x + cameraView.boundsOffset);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, cameraView.mapBounds.min.y - cameraView.boundsOffset, cameraView.mapBounds.max.y + cameraView.boundsOffset);
            cameraView.mainCamera.transform.position = cameraPosition;
        }
        
        private IEnumerator ZoomToSize(float targetSize, float duration)
        {
            float initialSize = cameraView.mainCamera.orthographicSize;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration; // 计算进度
                cameraView.mainCamera.orthographicSize = Mathf.Lerp(initialSize, targetSize, t);
                yield return null; // 等待下一帧
            }

            // 确保最终缩放到目标大小
            cameraView.mainCamera.orthographicSize = targetSize;
        }

    }
}