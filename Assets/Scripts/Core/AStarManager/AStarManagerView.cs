using System.Collections.Generic;
using System.Linq;
using Core.AStarManager.Events;
using Core.Game;
using Core.Game.Events;
using Core.Game.SceneManager;
using Core.Game.SceneManager.Events;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Core.AStarManager
{
    public class AStarManagerView : MonoBehaviour
    {
        [SerializeField] public List<Tilemap> collisionTilemaps = new List<Tilemap>();

        //public Tilemap walls;
        public Grid[,] grid;
        public Node nodePrefab;
        public List<Node> nodeList;

        public BoundsInt maxBounds;
        public int mapWidth;
        public int mapHeight;

        private bool canDrawGizmos;

        private Vector3Int newGridCenter; //新的生成格子的中心

        private void Awake()
        {
            SceneEvent.SceneLoadCompleteEvent += OnSceneLoadCompleteEvent;
            SceneEvent.SceneUnLoadCompleteEvent += OnSceneUnLoadCompleteEvent;
            AStarEvents.PathRequestEvent +=
                OnPathRequestEvent; // Request Path event, decouple with player and other entity
            GameGenericEvent.ColliderEnableEvent += OnColliderEnableEvent;
            GameGenericEvent.ColliderDisableEvent += OnColliderDisableEvent;
        }

        private void OnSceneUnLoadCompleteEvent(SceneUnLoadCompleteEvent obj)
        {
            /*foreach (Node node in nodeList)
            {
                Destroy(node.gameObject);
            }

            nodeList.Clear();
            collisionTilemaps.Clear();*/
        }

        private void OnColliderDisableEvent(ColliderDisableEvent obj)
        {
            Vector3Int pos = collisionTilemaps[0].WorldToCell(obj.gameObject.transform.position);
            Debug.Log("OnColliderDisableEvent" + pos);
            Node findNode = FindNodeByGridPosition(pos);
            if (findNode)
            {
                findNode.RestoreConnections();
                Debug.Log("RestoreConnections at Node: " + pos);
            }
        }

        private void OnColliderEnableEvent(ColliderEnableEvent obj)
        {
            Vector3Int pos = collisionTilemaps[0].WorldToCell(obj.gameObject.transform.position);
            Debug.Log("OnColliderEnableEvent" + pos);
            Node findNode = FindNodeByGridPosition(pos);
            if (findNode)
            {
                findNode.DisableConnections();
                Debug.Log("DisableConnections at Node: " + pos);
            }
        }

        private void OnPathRequestEvent(PathRequestEvent pathRequestEvent)
        {
            Node startNode = GetNearestNode(pathRequestEvent.StartPosition);
            Node targetNode = GetNearestNode(pathRequestEvent.TargetPosition);

            if (startNode != null && targetNode != null)
            {
                List<Node> path = GeneratePath(startNode, targetNode);
                pathRequestEvent.OnPathFound?.Invoke(path);
            }
            else
            {
                Debug.LogWarning("Could not find start or target node.");
            }
        }

        private Node GetNearestNode(Vector2 position)
        {
            // Node nearestNode = null;
            // float minDistance = float.MaxValue;
            //
            // // 遍历所有节点以找到距离指定位置最近的节点
            // foreach (Node node in nodeList)
            // {
            //     float distance = Vector2.Distance(node.transform.position, position);
            //     if (distance < minDistance)
            //     {
            //         minDistance = distance;
            //         nearestNode = node;
            //     }
            // }
            //
            // if (nearestNode != null)
            // {
            //     Debug.Log("Nearest node found at position: " + nearestNode.transform.position);
            // }
            // else
            // {
            //     Debug.LogWarning("No nearest node found.");
            // }
            //
            // return nearestNode;
            Node nearestNode = null;
            float minDistance = 1.1f; // 设置距离阈值为 1.1f

            // 遍历所有节点以找到距离指定位置最近且在范围内的节点
            foreach (Node node in nodeList)
            {
                float distance = Vector2.Distance(node.transform.position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestNode = node;
                }
            }

            // if (nearestNode != null)
            // {
            //     Debug.Log("Nearest node found within range at position: " + nearestNode.transform.position);
            // }
            // else
            // {
            //     Debug.LogWarning("No nearest node found within 1.1f range.");
            // }
            //
            // return nearestNode;
            // 如果在范围内没有找到节点，返回一个表示该点本身的位置
            if (nearestNode != null)
            {
                Debug.Log("Nearest node found within range at position: " + nearestNode.transform.position);
                return nearestNode;
            }
            else
            {
                Debug.LogWarning("No nearest node found within 1.1f range. Returning the original position.");
                return new Node { transform = { position = position } }; // 返回一个新节点对象，位置设为目标位置
            }
        }

        public void Update()
        {
            //每秒更新格子？<- 别,千万别
        }

        public static List<GameObject> GetNonTriggerColliders()
        {
            // 找到所有 Collider2D 类型的组件
            Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
            List<GameObject> nonTriggerColliders = new List<GameObject>();

            foreach (Collider2D collider in allColliders)
            {
                // 检查是否是 BoxCollider2D，并且 isTrigger 为 false
                if (collider is BoxCollider2D && collider.isTrigger == false)
                {
                    nonTriggerColliders.Add(collider.gameObject);
                }
            }

            return nonTriggerColliders;
        }

        private void OnSceneLoadCompleteEvent(SceneLoadCompleteEvent loadCompleteEvent)
        {
            Debug.Log("OnSceneLoadCompleteEvent");
            collisionTilemaps.Clear();
            // Prepare the array that contains all collied gameObject
            List<GameObject> gameObjects = new List<GameObject>();

            gameObjects = GameObject.FindGameObjectsWithTag("Collision").ToList();

            foreach (GameObject gameCollisionObject in gameObjects)
            {
                Tilemap tilemap = gameCollisionObject.GetComponent<Tilemap>();
                if (tilemap)
                {
                    collisionTilemaps.Add(tilemap);
                }
            }

            // 清空旧的 nodeList
            foreach (Node node in nodeList)
            {
                Destroy(node.gameObject);
            }

            grid = null;
            nodeList.Clear();


            // 初始化 grid，确保 grid 不为 null
            InitializeGrid();

            CreateNodes();

            // Update the nodes base on the non-tilemap collider object
            UpdatedNodes();
        }

        /// <summary>
        /// Update the nodes based on the non-tilemap collider object
        /// this methods will find all collide object grid position and
        /// try to disconnect the correspond nodes
        /// </summary>
        private void UpdatedNodes()
        {
            List<GameObject> nonTriggerColliderGameObject = GetNonTriggerColliders();
            foreach (GameObject gameObjectNonTrigger in nonTriggerColliderGameObject)
            {
                Vector3Int gameNonTriggerPos =
                    collisionTilemaps[0].WorldToCell(gameObjectNonTrigger.transform.position);
                Node correspondNode = FindNodeByGridPosition(gameNonTriggerPos);

                if (correspondNode)
                {
                    correspondNode.DisableConnections();
                    Debug.Log($"Disabled connections at Node: {gameNonTriggerPos}");
                }
            }
        }

        private void InitializeGrid()
        {
            // 初始化一个空的最大边界，假设至少有一个 Tilemap 存在
            maxBounds = collisionTilemaps[0].cellBounds;

            // 遍历所有的 Tilemap，找到整体地图的最小和最大边界
            foreach (var tilemap in collisionTilemaps)
            {
                if (maxBounds.xMin > tilemap.cellBounds.xMin)
                {
                    maxBounds.xMin = tilemap.cellBounds.xMin;
                }
                else
                {
                    maxBounds.xMin = maxBounds.xMin;
                }

                if (maxBounds.yMin > tilemap.cellBounds.yMin)
                {
                    maxBounds.yMin = tilemap.cellBounds.yMin;
                }
                else
                {
                    maxBounds.yMin = maxBounds.yMin;
                }

                if (maxBounds.xMax > tilemap.cellBounds.xMax)
                {
                    maxBounds.xMax = maxBounds.xMax;
                }
                else
                {
                    maxBounds.xMax = tilemap.cellBounds.xMax;
                }

                if (maxBounds.yMax > tilemap.cellBounds.yMax)
                {
                    maxBounds.yMax = maxBounds.yMax;
                }
                else
                {
                    maxBounds.yMax = tilemap.cellBounds.yMax;
                }
            }

            // 使用 maxBounds 的宽度和高度初始化 grid 数组
            mapWidth = maxBounds.size.x;
            mapHeight = maxBounds.size.y;
            grid = new Grid[mapWidth, mapHeight];


            // 计算 maxBounds 的中心点，并转换为世界坐标
            Vector3Int cellCenter = new Vector3Int(maxBounds.xMin + maxBounds.size.x / 2,
                maxBounds.yMin + maxBounds.size.y / 2, 0);
            Vector3 worldCenter = cellCenter;

            // 将世界坐标的 x、y、z 转为 int
            newGridCenter = new Vector3Int(Mathf.RoundToInt(worldCenter.x), Mathf.RoundToInt(worldCenter.y),
                Mathf.RoundToInt(worldCenter.z));

            Debug.Log("Grid initialized with width: " + mapWidth + " and height: " + mapHeight);
            Debug.Log("Grid center in world coordinates: " + newGridCenter);
        }

        public void CreateNodes()
        {
            int nodeCount = 0;

            // 遍历 x 和 y 范围，根据 maxBounds 计算格子坐标
            for (int x = maxBounds.xMin; x < maxBounds.xMax; x++)
            {
                for (int y = maxBounds.yMin; y < maxBounds.yMax; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, 0);

                    // 检查是否所有 Tilemap 的该格子位置都是可行走区域
                    bool isAllLayersWalkable = true;
                    foreach (Tilemap tilemap in collisionTilemaps)
                    {
                        if (tilemap.HasTile(cellPosition))
                        {
                            isAllLayersWalkable = false;
                            break;
                        }
                    }

                    // 如果该位置可行走，生成节点
                    if (isAllLayersWalkable)
                    {
                        // 将 cellPosition 转换为世界坐标，以便生成节点位置
                        // 假设每个单元格在世界坐标系中为 1x1 大小
                        Vector3 worldPosition = new Vector3(
                            cellPosition.x + 0.5f,
                            cellPosition.y + 0.5f,
                            0
                        );

                        // 创建节点并设置其属性
                        Node node = Instantiate(nodePrefab, worldPosition, Quaternion.identity);
                        node.gridPosition = cellPosition;
                        node.transform.SetParent(transform);
                        nodeList.Add(node); // 添加到节点列表
                        nodeCount++;
                    }
                }
            }

            //Debug.Log("Created " + nodeCount + " nodes.");
            CreateConnections();

            AStarEvents.NodesMapCompletedEvent?.Invoke(new NodesMapCompletedEvent(maxBounds));
        }

        void CreateConnections()
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                for (int j = i + 1; j < nodeList.Count; j++)
                {
                    if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 1.42f)
                    {
                        ConnectNodes(nodeList[i], nodeList[j]);
                        ConnectNodes(nodeList[j], nodeList[i]);
                    }
                }

                nodeList[i].originalConnections.AddRange(nodeList[i].connections);
            }

            canDrawGizmos = true;
            //SpawnAI(); 待用
        }

        void ConnectNodes(Node from, Node to)
        {
            if (from == to)
            {
                return;
            }

            from.connections.Add(to);
        }

        void SpawnAI()
        {
        }

        private void OnDrawGizmos()
        {
            if (canDrawGizmos)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    for (int j = 0; j < nodeList[i].connections.Count; j++)
                    {
                        Gizmos.DrawLine(nodeList[i].transform.position, nodeList[i].connections[j].transform.position);
                    }
                }
            }
        }

        public List<Node> GeneratePath(Node start, Node end)
        {
            List<Node> openSet = new List<Node>();
            // 从父节点获取所有子节点中的 Node
            //Node[] allNodes = nodesParent.GetComponentsInChildren<Node>();
            openSet = new List<Node>();

            foreach (Node n in FindObjectsOfType<Node>())
            {
                n.gScore = float.MaxValue;
            }

            start.gScore = 0;
            start.hScore = Vector2.Distance(start.transform.position, end.transform.position);
            openSet.Add(start); // 确保起点加入到 openSet

            while (openSet.Count > 0)
            {
                int lowestF = default;

                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FScore() < openSet[lowestF].FScore())
                    {
                        lowestF = i;
                    }
                }

                Node currentNode = openSet[lowestF];
                openSet.Remove(currentNode);

                if (currentNode == end)
                {
                    List<Node> path = new List<Node>();

                    path.Insert(0, end);

                    while (currentNode != start)
                    {
                        currentNode = currentNode.cameFrom;
                        path.Add(currentNode);
                    }

                    path.Reverse();
                    return path;
                }

                foreach (Node connectedNode in currentNode.connections)
                {
                    float heldGScore = currentNode.gScore + Vector2.Distance(currentNode.transform.position,
                        connectedNode.transform.position);

                    if (heldGScore < connectedNode.gScore)
                    {
                        connectedNode.cameFrom = currentNode;
                        connectedNode.gScore = heldGScore;
                        connectedNode.hScore =
                            Vector2.Distance(connectedNode.transform.position, end.transform.position);

                        if (!openSet.Contains(connectedNode))
                        {
                            openSet.Add(connectedNode);
                        }
                    }
                }

                if (start == null || end == null)
                {
                    Debug.LogWarning("Start or end node is null");
                    return null;
                }
            }

            Debug.LogWarning("No path found between the nodes.");
            return null;
        }

        private void OnDestroy()
        {
            SceneEvent.SceneLoadCompleteEvent -= OnSceneLoadCompleteEvent;
            AStarEvents.PathRequestEvent -=
                OnPathRequestEvent;
            GameGenericEvent.ColliderEnableEvent -= OnColliderEnableEvent;
            GameGenericEvent.ColliderDisableEvent -= OnColliderDisableEvent;
        }

        public Node FindNodeByGridPosition(Vector3Int gridPosition)
        {
            foreach (Node node in nodeList)
            {
                if (node.gridPosition == gridPosition)
                {
                    return node;
                }
            }

            return null;
        }
    }
}