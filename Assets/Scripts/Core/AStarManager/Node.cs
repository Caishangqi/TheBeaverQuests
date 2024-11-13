using System;
using System.Collections.Generic;
using Core.AStarManager.Events;
using UnityEngine;

namespace Core.AStarManager
{
    public class Node : MonoBehaviour
    {
        public Node cameFrom; //起点
        public List<Node> connections; // 当前的连接
        public bool isWalkable = true; // 标记节点是否可以通行
        public List<Node> originalConnections = new List<Node>(); // 保存原始连接
        private bool isDisabled = false; // 标记当前节点是否被禁用

        public Vector3Int gridPosition;

        public float gScore; //how many moves it takes to each node
        public float hScore; //estimate cost from current node to end node

        public float FScore()
        {
            return gScore + hScore; //用于确定整体上的最佳路径
        }

        public void Start()
        {
            // 初始时保存原始的连接
            // if (originalConnections.Count == 0)
            // {
            //     originalConnections.AddRange(connections);
            // }
        }

        // 禁用连接，并从其他节点的连接列表中删除自己
        public void DisableConnections()
        {
            if (!isDisabled && connections.Count > 0)
            {
                // Debug.Log("Disabling connections for node at: " + transform.position);

                // 遍历连接的节点，从它们的 connections 列表中移除当前节点
                foreach (Node connectedNode in connections)
                {
                    connectedNode.RemoveConnection(this); // 从其他节点的连接列表中删除自己
                }

                connections.Clear(); // 清除当前节点的所有连接
                isDisabled = true; // 更新为禁用状态
            }
        }

        // 恢复连接，并重新加入到其他节点的连接列表中
        public void RestoreConnections()
        {
            if (isDisabled && connections.Count == 0 && originalConnections.Count > 0)
            {
                Debug.Log("Restoring connections for node at: " + transform.position);

                connections.AddRange(originalConnections); // 先恢复该节点自己的连接

                // 将自己重新加入到其他连接节点的 connections 列表中
                foreach (Node connectedNode in originalConnections)
                {
                    connectedNode.AddConnection(this);
                    //this.AddConnection(connectedNode); // 恢复自己对其他节点的连接（双向）
                }

                isDisabled = false; // 更新为恢复状态
            }
        }

        // 从 connections 列表中删除某个节点
        public void RemoveConnection(Node node)
        {
            if (connections.Contains(node))
            {
                connections.Remove(node);
                //Debug.Log("Removed connection from " + transform.position + " to " + node.transform.position);
            }
        }

        // 向 connections 列表中添加某个节点
        public void AddConnection(Node node)
        {
            if (!connections.Contains(node))
            {
                connections.Add(node);
                //Debug.Log("Added connection from " + transform.position + " to " + node.transform.position);
            }
        }
        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.blue;
        //
        //     if (connections.Count > 0)
        //     {
        //         for (int i = 0; i < connections.Count; i++)
        //         {
        //             // 绘制当前节点与相连节点之间的连线
        //             Gizmos.DrawLine(this.transform.position, connections[i].transform.position);
        //             //Gizmos.DrawLine(connections[i].transform.position, connections[i+1].transform.position);
        //         }
        //     }
        // }
    }
}