using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public PanelAnim panelAnim; // 引用 PanelAnim 脚本
    public RectTransform pauseArea; // 用于定义暂停区域的UI元素（例如一个透明的区域）
    public RectTransform resumeArea; // 用于定义恢复区域的UI元素（例如一个透明的区域）

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测左键点击
        {
            Vector2 mousePosition = Input.mousePosition;

            // 检查鼠标是否在pauseArea内点击
            if (RectTransformUtility.RectangleContainsScreenPoint(pauseArea, mousePosition))
            {
                PauseGame();
            }
            
            if (RectTransformUtility.RectangleContainsScreenPoint(resumeArea, mousePosition))
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        panelAnim.ShowPanel(); 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panelAnim.HidePanel(); 
    }
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     pauseButton.onClick.AddListener(PauseGame);
    //     resumeButton.onClick.AddListener(ResumeGame);
    // }
}
