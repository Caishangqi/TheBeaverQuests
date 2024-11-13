using UnityEngine;

namespace Core.Character
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] GameObject Player; //找到player做自己的父类
        
        private void Update()
        {
            // 检查当前 Hand 是否在 Player 下面
            if (transform.parent != Player.transform)
            {
                // 如果 Hand 没有在 Player 下面，则重新附加
                AttachToPlayer();
            }
        }

        // 将 Hand 附加到 Player
        private void AttachToPlayer()
        {
            transform.SetParent(Player.transform); // 将 Hand 设置为 Player 的子物体
            Debug.Log("Hand has been reattached to Player.");
        }
    }
}