using System.Collections;
using System.Collections.Generic;
using Core.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeInteractUI : MonoBehaviour
{
        //public GameObject buttonPrefab; // 预制的按钮对象
         public Canvas canvas; // UI的Canvas，确保按钮生成在UI中
         public Vector2 offset = new Vector2(500, 0); // 按钮相对于高亮物体的偏移量
         
         private InteractableView currentInteractable; // 当前被高亮的物体
         private RectTransform buttonRectTransform; // 用于保存按钮的RectTransform
         private GameObject currentButton; // 保存当前生成的按钮，方便删除

        // 你需要的Sprite，用来作为按钮的背景图像
        public Sprite buttonSprite; 

        void Update()
        {
            // 检查当前是否有高亮的物体
            if (currentInteractable != null)
            {
                // 在物体周围生成按钮
                GenerateButtonNearObject(currentInteractable);
                Debug.Log("Current interactable: " + currentInteractable.name);
            }

            // 检测鼠标左键点击
            if (Input.GetMouseButtonDown(0)) // 0 表示鼠标左键
            {
                Vector2 mousePosition = Input.mousePosition;

                // 检查鼠标点击是否在按钮区域内
                if (buttonRectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(buttonRectTransform, mousePosition, Camera.main))
                {
                    OnButtonClick(currentInteractable); // 手动调用点击逻辑
                }
            }
        }

        // 设置当前被高亮的物体
        public void SetCurrentInteractable(InteractableView interactable)
        {
            currentInteractable = interactable;
        }

        // 生成按钮的逻辑
        private void GenerateButtonNearObject(InteractableView interactable)
        {
            // 如果已经生成过按钮，先清除旧的按钮
            if (currentButton != null)
            {
                Destroy(currentButton);
            }

            // 动态创建按钮对象
            currentButton = new GameObject("GeneratedButton");
            currentButton.transform.SetParent(canvas.transform); // 将按钮放到 Canvas 中

            // 获取物体的世界坐标并转换为屏幕坐标
            Vector3 objectWorldPosition = interactable.transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectWorldPosition);
            
            // 添加 Button 组件
            Button buttonComponent = currentButton.AddComponent<Button>();

            // 添加 Image 组件，Button 需要 Image 来显示背景
            Image buttonImage = currentButton.AddComponent<Image>();
            
            // 设置按钮的Sprite
            if (buttonSprite != null)
            {
                buttonImage.sprite = buttonSprite; // 设置为指定的Sprite
                buttonImage.SetNativeSize();// 根据 Sprite 尺寸调整按钮大小
            }
            else
            {
                Debug.LogWarning("ButtonSprite is not assigned!");
                buttonImage.color = Color.white; // 如果没有Sprite，则设置一个默认的颜色
            }

            // 设置按钮的 RectTransform
            buttonRectTransform = currentButton.GetComponent<RectTransform>();
            buttonRectTransform.sizeDelta = new Vector2(2, 2); // 设置按钮大小
            //buttonRectTransform.position = screenPosition + (Vector3)offset; // 设置按钮位置
            
            // 将屏幕坐标转换为Canvas的局部坐标
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPosition, Camera.main, out localPoint);

            // 将按钮的位置设置为转换后的局部坐标加上偏移量
            buttonRectTransform.localPosition = localPoint + offset;
        }

        // 按钮点击的回调
        private void OnButtonClick(InteractableView interactable)
        {
            Debug.Log("Button clicked for: " + interactable.name);
            // 在这里添加按钮点击时的具体逻辑
        }
        
        // 删除按钮的逻辑
        public void RemoveButtonForInteractable(InteractableView interactable)
        {
            // 如果当前的高亮物体是要移除高亮的物体，并且按钮已经生成
            if (currentInteractable == interactable)
            {
                if (currentButton != null)
                {
                    Debug.Log("Removing button for: " + interactable.name);
                    Destroy(currentButton); // 删除生成的按钮
                    currentButton = null;   // 清空按钮引用
                }
                currentInteractable = null; // 清空高亮的物体引用，防止再次生成按钮
            }
        }
        
}

