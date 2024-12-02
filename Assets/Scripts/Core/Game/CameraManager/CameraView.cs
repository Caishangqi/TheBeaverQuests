using System;
using System.Collections;
using Core.AStarManager;
using Core.AStarManager.Events;
using Core.Game.CameraManager.Events;
using Core.Game.CameraManager.Handler;
using Core.Game.SceneManager;
using Core.Game.SceneManager.Events;
using UnityEngine;

namespace Core.Game.CameraManager
{
    public class CameraView : MonoBehaviour
    {
        #region CameraView

        #region Variable

        [Header("Camera Zoom")] public Camera mainCamera; // 需要操作的摄像头
        public float zoomSpeed = 5f; // 放大/缩小速度
        public float zoomLerpSpeed = 10f; // 放大/缩小插值速度
        public float minZoom = 2f; // 最小缩放值
        public float maxZoom = 10f; // 最大缩放值
        public float defaultZoom = 4.0f; // 默认值
        public float zoomRestoreDelay = 4.0f; // 默认值
        public float percentZoom = 1.0f;
        public float targetZoom;

        public bool needsToShowEvilTree = false;

        // Camera following
        [Header("Camera Following")] public Transform player; // 玩家对象的 Transform
        public float followSpeed = 0.1f; // 跟随速度
        public Vector3 cameraOffset = new Vector3(0, 0, -10);
        private Vector3 velocity = Vector3.zero;

        private Coroutine resetZoomCoroutine; // 存储重置缩放的协程引用

        public BoundsInt mapBounds; //从Astar事件获得

        public float boundsOffset = 50f; //相机可以向各个方向多移动的范围大小

        #endregion

        #region EventPool

        public CameraZoomEvent cameraZoomEvent;

        #endregion

        #region Components

        #endregion

        #region Handler

        CameraMovementHandler cameraMovementHandler;
        CameraInputHandler cameraInputHandler;

        #endregion

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            cameraMovementHandler = new CameraMovementHandler(this);
            cameraInputHandler = new CameraInputHandler(this);

            targetZoom = defaultZoom;
            cameraZoomEvent = new CameraZoomEvent(defaultZoom, this);

            AStarEvents.NodesMapCompletedEvent += OnNodesMapCompletedEvent;
            SceneEvent.SceneRequestUnLoadEvent += OnSceneRequestUnloadEvent;
            EvilTreeEvent.MarkAsDeadEvent += OnEvilTreeDeadEvent;
        }

        private void OnEvilTreeDeadEvent(MarkAsDeadEvent obj)
        {
            needsToShowEvilTree = true;
            GameObject evilTree = obj.EvilTree;

            StartCoroutine(MoveCameraToEvilTree(evilTree.transform.position, 6f));
        }

        private void OnSceneRequestUnloadEvent(SceneRequestUnLoadEvent obj)
        {
            needsToShowEvilTree = false;
        }

        private void OnNodesMapCompletedEvent(NodesMapCompletedEvent nodesMapCompletedEvent)
        {
            mapBounds = nodesMapCompletedEvent.maxBounds;
        }

        // Update is called once per frame
        void Update()
        {
            // Update zoom percent
            percentZoom = cameraMovementHandler.GetCameraPercentZoom();

            // 处理鼠标滚轮输入
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                targetZoom -= scrollInput * zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

                // Set game event
                cameraZoomEvent.currentZoom = targetZoom;
                cameraZoomEvent.CameraView = this;
                CameraEvent.CameraZoomEvent?.Invoke(cameraZoomEvent); // broadcast

                // 如果有正在运行的重置缩放协程，则停止它
                if (resetZoomCoroutine != null)
                {
                    StopCoroutine(resetZoomCoroutine);
                }

                if (SettingManager.SettingManager.instance.settingsData.enableCameraRestore)
                {
                    // 启动重置缩放的协程
                    resetZoomCoroutine = StartCoroutine(ResetZoomAfterDelay(zoomRestoreDelay));
                }
            }

            // 平滑插值缩放
            mainCamera.orthographicSize =
                Mathf.Lerp(mainCamera.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

            // 使用 CameraInputHandler 处理手势操作
            cameraInputHandler.HandleTouchInput();
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {
            if (!needsToShowEvilTree)
            {
                if (player != null)
                {
                    Vector3 targetPosition = player.position + cameraOffset;
                    mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition,
                        ref velocity, 1f / followSpeed);
                }
            }
        }

        // 重置缩放的协程方法
        public IEnumerator ResetZoomAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            // 平滑插值重置缩放
            float smoothSpeed = 2f; // 控制恢复到默认缩放的速度
            while (Mathf.Abs(mainCamera.orthographicSize - defaultZoom) > 0.01f)
            {
                targetZoom = Mathf.Lerp(mainCamera.orthographicSize, defaultZoom, Time.deltaTime * smoothSpeed);
                mainCamera.orthographicSize = targetZoom;
                cameraZoomEvent.currentZoom = targetZoom;
                CameraEvent.CameraZoomEvent?.Invoke(cameraZoomEvent); // broadcast
                yield return null; // 等待下一帧
            }

            // 确保最终精确设置到默认值
            mainCamera.orthographicSize = defaultZoom;
        }

        private IEnumerator MoveCameraToEvilTree(Vector3 targetPosition, float duration)
        {
            float elapsedTime = 0f;
            Vector3 initialPosition = mainCamera.transform.position;

            // 计算目标位置，考虑摄像机偏移量
            targetPosition += cameraOffset;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // 平滑插值移动摄像机
                mainCamera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);

                yield return null; // 等待下一帧
            }

            // 确保摄像机最终到达目标位置
            mainCamera.transform.position = targetPosition;

            // 重置状态
            needsToShowEvilTree = false;
        }
    }
}