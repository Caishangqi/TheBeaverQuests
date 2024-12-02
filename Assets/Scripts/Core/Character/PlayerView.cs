using System.Collections.Generic;
using Core.AStarManager;
using Core.AStarManager.Events;
using Core.Character.Events;
using Core.Character.Handler;
using Core.Game.AudioManager;
using Core.Game.AudioManager.Data;
using Core.Game.ControllerModule.Gesture;
using Core.Game.SceneManager;
using Core.Game.SceneManager.Events;
using Core.Game.TileMapManager.CubePlaceSelection;
using Core.Game.TileMapManager.CubePlaceSelection.Events;
using Core.UI.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Character
{
    public class PlayerView : MonoBehaviour, IPointerClickHandler
    {
        #region PlayerView

        [SerializeField] public float speed = 5;
        [SerializeField] public float interactionRadius = 3f; // 交互范围半径
        [SerializeField] public LayerMask interactableLayerMask; // 可交互物品的图层
        [SerializeField] public Animator animator;

        #region Handler

        [SerializeField] public PlayerInputHandler playerInputHandler;
        [SerializeField] public PlayerActionHandler playerActionHandler;
        [SerializeField] public PlayerInteractHandler playerInteractHandler;
        [SerializeField] public PlayerAnimationHandler playerAnimationHandler;
        //[SerializeField] public PlayerCollectHandler playerCollectHandler;

        #endregion

        #region Variables

        private AudioManagerView audioManagerView { get; set; }
        private List<Node> path = new List<Node>();
        public Vector2 targetPosition;
        public bool isMoving;
        [SerializeField] public InteractableView currentHighlightedInteractableView;

        public float doubleTapThreshold = 0.5f; // 双击之间的最大间隔时间
        private float lastTapTime = 0f;
        private int tapCount = 0;
        private GameObject detectArea;
        private bool isSingleFingerActionBlocked = false;
        private float singleFingerBlockTimer = 0f;
        private float blockDuration = 0.2f;

        #endregion


        #region Components

        [SerializeField] public SpriteRenderer spriteRenderer;

        [SerializeField] public Rigidbody2D rigidbody2D;

        [SerializeField] public CircleCollider2D playerCollider;

        [SerializeField] public GameObject hand; //手部

        [SerializeField] public AudioSource audioStepSource;

        #endregion

        [SerializeField] public PlayerSo playerData { get; set; }


        // Start is called before the first frame update
        public void Start()
        {
            playerInputHandler = new PlayerInputHandler(this);
            playerActionHandler = new PlayerActionHandler(this);
            playerInteractHandler = new PlayerInteractHandler(this);
            playerAnimationHandler = new PlayerAnimationHandler(this);
            SceneEvent.SceneUnLoadCompleteEvent += OnSceneUnLoadCompleteEvent;

            // Audio Manager
            audioStepSource = gameObject.AddComponent<AudioSource>();
            audioManagerView = FindObjectOfType<AudioManagerView>();
            if (!audioManagerView)
            {
                Debug.LogException(
                    new MissingComponentException("AudioManagerView not found, please add one in the scene."));
            }

            playerData = new PlayerSo();
            targetPosition = transform.position;
            isMoving = false;

            //gameObject.SetActive(false);
        }

        /// <summary>
        /// Handle logic when the scene is prepare for unloading, handle
        /// cube when player carried cube
        /// </summary>
        /// <param name="sceneUnLoadCompleteEvent"></param>
        private void OnSceneUnLoadCompleteEvent(SceneUnLoadCompleteEvent sceneUnLoadCompleteEvent)
        {
            CubeView carriedCube = playerData.carriedObj;
            if (carriedCube != null)
            {
                carriedCube.IsHeld = false;
                Destroy(carriedCube.gameObject);
                playerData.carriedObj = null;
            }
        }


        // Update is called once per frame
        private void Update()
        {
            // Debug Code
            if (Input.GetKeyUp(KeyCode.R))
            {
                playerActionHandler.RequestCubePlaceSelectionUI();
            }

            if (singleFingerBlockTimer > 0)
            {
                singleFingerBlockTimer -= Time.deltaTime;
                isSingleFingerActionBlocked = true;
            }
            else
            {
                isSingleFingerActionBlocked = false;
            }

            // 检查双指操作状态
            if (GestureManager.Instance.IsTwoFingerGestureActive)
            {
                Debug.Log("Has banned 1 finger moving");
                isSingleFingerActionBlocked = true; // 阻止单指操作
                singleFingerBlockTimer = blockDuration; // 设置延迟计时器
                return;
            }

            if (isSingleFingerActionBlocked)
            {
                isSingleFingerActionBlocked = false; // 每帧重置状态
                return; // 阻止本帧的单指输入
            }


            // 检测触摸或鼠标输入
            if (Input.touchCount == 1 | Input.GetMouseButtonDown(0))
            {
                // // 检测触摸或鼠标输入
                // if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
                // {
                // 判断是否是在 UI 上
                if (EventSystem.current != null)
                {
                    if (Input.touchCount > 0)
                    {
                        // Magical code, change when you learn all touchphase
                        // =============================================================================================
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            // 如果有触摸操作，检查第一个触点是否在 UI 上
                            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                            {
                                return; // 在 UI 上，直接返回
                            }
                            else
                            {
                                PlayerEvent.PlayerUnClickGameObject.Invoke(new PlayerUnClickGameObject(this));
                            }
                        }
                        // =============================================================================================
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        // 如果是鼠标操作，检查鼠标是否在 UI 上
                        if (EventSystem.current.IsPointerOverGameObject())
                        {
                            return; // 在 UI 上，直接返回
                        }
                    }
                }

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    // 仅当触摸开始或结束时处理逻辑
                    if (touch.phase == TouchPhase.Began)
                    {
                        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        Collider2D hitCollider = Physics2D.OverlapPoint(touchPosition);

                        if (hitCollider != null && hitCollider.gameObject == gameObject)
                        {
                            CubePlaceSelectionEvent.RequestCubePlaceSelectionUIEvent.Invoke(
                                new RequestCubePlaceSelectionUIEvent(this));
                            return;
                        }

                        // 获取世界坐标位置
                        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        targetPosition = new Vector2(worldPosition.x, worldPosition.y);

                        // 调用路径请求事件
                        AStarEvents.PathRequestEvent?.Invoke(new PathRequestEvent(
                            transform.position,
                            targetPosition,
                            foundedPath =>
                            {
                                if (foundedPath != null && foundedPath.Count > 0)
                                {
                                    this.path = foundedPath;
                                    isMoving = true;
                                    UIEvent.MovingCheckerOKEvent?.Invoke(new MovingCheckerOKEvent(targetPosition));
                                    Debug.Log("Path generated. Path length: " + foundedPath.Count);
                                }
                                else
                                {
                                    UIEvent.MovingCheckerNOTEvent?.Invoke(new MovingCheckerNOTEvent(targetPosition));
                                    Debug.LogWarning("No valid path generated.");
                                }
                            }
                        ));
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    // 获取世界坐标位置
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    targetPosition = new Vector2(worldPosition.x, worldPosition.y);

                    // 调用路径请求事件
                    AStarEvents.PathRequestEvent?.Invoke(new PathRequestEvent(
                        transform.position,
                        targetPosition,
                        foundedPath =>
                        {
                            if (foundedPath != null && foundedPath.Count > 0)
                            {
                                this.path = foundedPath;
                                isMoving = true;
                                UIEvent.MovingCheckerOKEvent?.Invoke(new MovingCheckerOKEvent(targetPosition));
                                Debug.Log("Path generated. Path length: " + foundedPath.Count);
                            }
                            else
                            {
                                UIEvent.MovingCheckerNOTEvent?.Invoke(new MovingCheckerNOTEvent(targetPosition));
                                Debug.LogWarning("No valid path generated.");
                            }
                        }
                    ));

                    // 如果鼠标点击，则调用 PlayerUnClickGameObject 事件
                    PlayerEvent.PlayerUnClickGameObject?.Invoke(new PlayerUnClickGameObject(this));
                }
            }

            playerInteractHandler.DetectInteractable();
        }

        private bool IsPointerOverUIElement()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            return raycastResults.Count > 0;
        }

        private void FixedUpdate()
        {
            MoveAlongPath();

            if (isMoving && playerData.carriedObj == null)
            {
                playerAnimationHandler.OnMovingAnimation();
            }

            if (!isMoving && playerData.carriedObj == null)
            {
                playerAnimationHandler.OnIdleAnimation();
            }

            if (isMoving && playerData.carriedObj != null)
            {
                playerAnimationHandler.OnMovingWithCubeAnimation();
            }

            if (!isMoving && playerData.carriedObj != null)
            {
                playerAnimationHandler.OnIdleWithCubeAnimation();
            }
        }

        private void MoveAlongPath()
        {
            if (isMoving && path.Count > 0)
            {
                Node nextNode = path[0];
                Vector2 currentPosition = rigidbody2D.position;
                Vector2 direction = ((Vector2)nextNode.transform.position - currentPosition).normalized;
                Vector2 newPosition = currentPosition + direction * speed * Time.fixedDeltaTime;

                // 如果接近当前目标节点，则更新当前节点并移除它
                if (Vector2.Distance(newPosition, nextNode.transform.position) < 0.1f)
                {
                    newPosition = nextNode.transform.position;
                    path.RemoveAt(0);

                    if (path.Count == 0)
                    {
                        isMoving = false;
                    }
                }

                rigidbody2D.MovePosition(newPosition);

                if (rigidbody2D.velocity.magnitude > 0.1f || isMoving)
                {
                    PlayerEvent.PlayerMoveEvent?.Invoke(new PlayerMoveEvent(newPosition, this));
                    if (!audioStepSource.isPlaying)
                    {
                        PlayFootstepSound();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // 检查进入的对象是否是 detectArea
            if (other.gameObject == detectArea)
            {
                DamEvent.CheckIfCanBuildEvent?.Invoke(new CheckIfCanBuildEvent());
                Debug.Log("Entered Detect Area");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // 检查离开的对象是否是 detectArea
            if (other.gameObject == detectArea)
            {
                UIEvent.SetHammerRelativeComponentsEnabledEvent?.Invoke(new SetHammerRelativeComponentsEnabledEvent());
                Debug.Log("Exited Detect Area");
            }
        }

        // 可视化交互半径
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }

        private void OnDestroy()
        {
            playerActionHandler.OnDestroy();
            playerInputHandler.OnDestroy();
            playerInteractHandler.OnDestroy();
            SceneEvent.SceneUnLoadCompleteEvent -= OnSceneUnLoadCompleteEvent;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        private void PlayFootstepSound()
        {
            AudioClip clip = audioManagerView.getAudioData(ESound.ENTITY_PLAYER_WALK).audioClip;
            audioStepSource.clip = clip;
            audioStepSource.Play();
        }
    }

    #endregion
}