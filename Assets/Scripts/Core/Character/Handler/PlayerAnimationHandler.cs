using Core.Character.Events;
using Core.Game.SceneManager;
using Core.Game.SceneManager.Events;
using Core.UI.InteractButtonWidget;
using Core.UI.InteractButtonWidget.Event;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Character.Handler
{
    public class PlayerAnimationHandler
    {
        private static readonly int NeedsToCarryFromTop = Animator.StringToHash("NeedsToCarryFromTop");
        public PlayerView PlayerView { get; set; }
        
        private GameObject hand;  // 手部骨骼的Transform
        private Vector3 handOffset = new Vector3(1.0f, 0, 0);  // 手部的偏移量
        
        // 引用手部的Animator
        private Animator handAnimator;  

        public PlayerAnimationHandler(PlayerView playerView)
        {
            this.PlayerView = playerView;
            //PlayerEvent.PlayerMoveEvent += OnPlayerMoveEvent;
            PlayerEvent.PlayerCarryCubeAnimeEvent += OnPlayerCarryCubeAnimeEvent;
            //InteractButtonEvent.InteractButtonClickEvent += OnInteractButtonClickEvent;
            PlayerEvent.PlayerLayDownEvent += OnPlayerLayDownEvent;
            SceneEvent.SceneUnLoadCompleteEvent += OnSceneUnloadCompleteEvent;
            //PlayerEvent.Player
            
            // 获取手部的Animator（假设手部是Player的子对象）
            handAnimator = playerView.hand.GetComponent<Animator>();
            Transform handTransform = playerView.hand.transform;  // 需要在PlayerView中定义手部Transform
        }

        private void OnSceneUnloadCompleteEvent(SceneUnLoadCompleteEvent obj)
        {
            PlayerView.animator.SetBool("IsWalking", false);
            PlayerView.animator.SetBool("IsCarryingAndMoving",false);
            PlayerView.animator.SetBool("IsCarryingAndIdle",false);
            PlayerView.animator.SetBool("IsInIdle", true);
            PlayerView.animator.ResetTrigger("NeedsToPutDown");
            PlayerView.animator.ResetTrigger("NeedsToCarryFromLeft");
            PlayerView.animator.ResetTrigger("NeedsToCarryFromRight");
            PlayerView.animator.ResetTrigger("NeedsToCarryFromTop");
            
            hand = PlayerView.hand;
            handAnimator = hand.GetComponent<Animator>();
            handAnimator.SetBool("HasCarried", false);
            handAnimator.ResetTrigger("NeedsToCarry");
        }

        private void OnPlayerLayDownEvent(PlayerLayDownCubeEvent obj)
        {
            if (PlayerView.transform.localScale.x < 0.0f)
            {
                // 立刻将角色的方向调回向左
                PlayerView.transform.localScale *= -1;
            }
            PlayerView.animator.SetBool("IsWalking", false);
            PlayerView.animator.SetBool("IsCarryingAndMoving",false);
            PlayerView.animator.SetBool("IsCarryingAndIdle",false);
            PlayerView.animator.SetBool("IsInIdle", true);
            
            PlayerView.animator.SetTrigger("NeedsToPutDown");
        }

        private void OnPlayerCarryCubeAnimeEvent(PlayerCarryCubeAnimeEvent obj)
        {
            
            PlayerView.animator.SetBool("IsWalking", false);
            PlayerView.animator.SetBool("IsInIdle", false);
            PlayerView.animator.SetBool("IsCarryingAndMoving",false);
            PlayerView.animator.SetBool("IsCarryingAndIdle",false);
            
            hand = PlayerView.hand;
            Vector2 originalPosition = hand.transform.position;
            handAnimator = hand.GetComponent<Animator>();
            handAnimator.SetBool("HasCarried", false);
            
            Vector3 cubePosition = PlayerView.currentHighlightedInteractableView.GameObject().transform.position;
            
            Vector3 playerPosition = PlayerView.GameObject().transform.position;
            
            float xdistance = playerPosition.x - cubePosition.x;
            float ydistance = playerPosition.y - cubePosition.y;

            if (xdistance > 0.0f &&  ydistance == 0.0f)
            {
                
                PlayerView.animator.SetTrigger("NeedsToCarryFromLeft");

                // 通过代码直接播放 PickUpCube 动画
                //PlayerView.animator.Play("CarryFromLeftAnim", 0); // BaseLayer (层0)
                
                // 调整手部位置，向左移动一格（假设1单位）
                hand.transform.position -= handOffset;  // 向左移1个单位
                
                // 播放手部动画
                //handAnimator.Play("PickUpCube", 0); // 播放手部动画
                
                handAnimator.SetTrigger("NeedsToCarry");
                // 打开手部SpriteRenderer
                //.hand.GetComponent<SpriteRenderer>().enabled = true;
                
                //hand.transform.position += handOffset;  // 调回原位
                //PlayerView.hand.GetComponent<SpriteRenderer>().enabled = false;
                handAnimator.SetBool("HasCarried", true);
            }
            
            

            if (xdistance < 0.0f &&  ydistance == 0.0f)
            {
                // // 将角色镜像反转，翻转X轴方向，使其面向右侧
                // Vector3 newScale = PlayerView.transform.localScale;
                // newScale.x = Mathf.Abs(newScale.x);  // 确保x是正的（面向右）
                // PlayerView.transform.localScale = newScale;
                
                PlayerView.animator.SetTrigger("NeedsToCarryFromRight");
                
                // 通过代码直接播放 PickUpCube 动画
                //PlayerView.animator.Play("Anim_PickUpCube", 0); // BaseLayer (层0)
                
                
                // 调整手部位置，向you移动一格（假设1单位）
                hand.transform.position += handOffset;  // 向右移1个单位
                
                // 播放手部动画
                //handAnimator.Play("Anim_Cube_PickUpCube", 0); // 播放手部动画
                
                //handAnimator.SetTrigger("NeedsToCarry");
                // 打开手部SpriteRenderer
                PlayerView.hand.GetComponent<SpriteRenderer>().enabled = true;
                
                //hand.transform.position -= handOffset;  // 调回原位
                //PlayerView.hand.GetComponent<SpriteRenderer>().enabled = true;
                handAnimator.SetBool("HasCarried", true);
                
            }

            else if (Mathf.Abs(ydistance)!=0)
            {
                if (PlayerView.transform.localScale.x < 0.0f)
                {
                    // 立刻将角色的方向调回向左
                    PlayerView.transform.localScale *= -1;
                }
                
                PlayerView.animator.SetBool("IsWalking", false);
                PlayerView.animator.SetTrigger("NeedsToCarryFromTop");
                // 通过代码直接播放 PickUpCube 动画
                //PlayerView.animator.Play("Anim_FrontRaiseCube", 0); // BaseLayer (层0)
            }
        }

        public void OnMovingAnimation()
        {
            if (PlayerView.transform.localScale.x < 0.0f)
            {
                // 立刻将角色的方向调回向左
                PlayerView.transform.localScale *= -1;
            }
            PlayerView.animator.SetBool("IsCarryingAndMoving", false);
            PlayerView.animator.SetBool("IsCarryingAndIdle", false);
            PlayerView.animator.SetBool("IsInIdle", false);
            PlayerView.animator.SetBool("IsWalking", true);
        }

        public void OnIdleAnimation()
        {
            if (PlayerView.transform.localScale.x < 0.0f)
            {
                // 立刻将角色的方向调回向左
                PlayerView.transform.localScale *= -1;
            }
            PlayerView.animator.SetBool("IsCarryingAndMoving", false);
            PlayerView.animator.SetBool("IsCarryingAndIdle", false);
            PlayerView.animator.SetBool("IsInIdle", true);
            PlayerView.animator.SetBool("IsWalking", false);
        }

        public void OnMovingWithCubeAnimation()
        {
            if (PlayerView.transform.localScale.x < 0.0f)
            {
                // 立刻将角色的方向调回向左
                PlayerView.transform.localScale *= -1;
            }
            PlayerView.animator.SetBool("IsCarryingAndMoving", true);
            PlayerView.animator.SetBool("IsCarryingAndIdle", false);
            PlayerView.animator.SetBool("IsInIdle", false);
            PlayerView.animator.SetBool("IsWalking", false);
        }

        public void OnIdleWithCubeAnimation()
        {
            if (PlayerView.transform.localScale.x < 0.0f)
            {
                // 立刻将角色的方向调回向左
                PlayerView.transform.localScale *= -1;
            }
            PlayerView.animator.SetBool("IsCarryingAndMoving", false);
            PlayerView.animator.SetBool("IsCarryingAndIdle", true);
            PlayerView.animator.SetBool("IsInIdle", false);
            PlayerView.animator.SetBool("IsWalking", false);
        }
    }
}