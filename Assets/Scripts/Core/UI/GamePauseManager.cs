using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    //public PanelAnim panelAnim; // 引用 PanelAnim 脚本
    public RectTransform pauseArea; // 用于定义暂停区域的UI元素（例如一个透明的区域）
    public RectTransform resumeArea; // 用于定义恢复区域的UI元素（例如一个透明的区域）
    public GameObject panel; // 目标panel
    
    public AnimationCurve showcurve;
    public AnimationCurve hidecurve;
    public float animSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        //pauseButton.onClick.AddListener(PauseGame);
        //resumeButton.onClick.AddListener(ResumeGame);
        // 确保初始状态面板是隐藏的，通过缩放来控制
        // 打印 pauseArea 和 resumeArea 的位置信息
        if (pauseArea != null)
        {
            Debug.Log("PauseArea Position (world): " + pauseArea.position); // 世界坐标
            Debug.Log("PauseArea Position (local): " + pauseArea.anchoredPosition); // 局部坐标
        }
        else
        {
            Debug.LogError("PauseArea is not assigned.");
        }

        if (resumeArea != null)
        {
            Debug.Log("ResumeArea Position (world): " + resumeArea.position); // 世界坐标
            Debug.Log("ResumeArea Position (local): " + resumeArea.anchoredPosition); // 局部坐标
        }
        else
        {
            Debug.LogError("ResumeArea is not assigned.");
        }

        // 将面板的初始状态设置为隐藏状态
        if (panel != null)
        {
            panel.transform.localScale = Vector3.zero; // 隐藏面板，缩放为0
        }
        else
        {
            Debug.LogError("Panel is not assigned.");
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测左键点击
        {
            // 获取鼠标点击的世界坐标
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);


            // 检查鼠标是否在pauseArea内点击
            // 检查鼠标点击是否在 pauseArea 内
            if (RectTransformUtility.RectangleContainsScreenPoint(pauseArea, Input.mousePosition, Camera.main))
            {
                PauseGame(); // 调用暂停逻辑
            }

            // 检查鼠标点击是否在 resumeArea 内
            if (RectTransformUtility.RectangleContainsScreenPoint(resumeArea, Input.mousePosition, Camera.main))
            {
                ResumeGame(); // 调用恢复逻辑
            }
            // if (RectTransformUtility.RectangleContainsScreenPoint(pauseArea, mousePosition))
            // {
            //     Debug.Log("clicking");
            //     PauseGame();
            // }
            //
            // if (RectTransformUtility.RectangleContainsScreenPoint(resumeArea, mousePosition))
            // {
            //     ResumeGame();
            // }
        }
    }

    public void ShowPanel()
    {
        if(panel != null)
        {
            Debug.Log("Starting ShowPanel Coroutine");
            StartCoroutine(ShowPanel(panel));
        }
        else
        {
            Debug.LogError("Panel is not assigned in PanelAnim script");
        }
    }

    public void HidePanel()
    {
        StartCoroutine(HidePanel(panel));
    }
    
    IEnumerator ShowPanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * showcurve.Evaluate(timer);
            timer += Time.deltaTime * animSpeed;
            yield return null;
        }
        gameObject.transform.localScale = Vector3.one;
    }
    
    IEnumerator HidePanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * hidecurve.Evaluate(timer);
            timer += Time.deltaTime * animSpeed;
            yield return null;
        }
        gameObject.transform.localScale = Vector3.zero;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        ShowPanel(); 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        HidePanel(); 
    }
}
