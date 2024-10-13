using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseManager : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public PanelAnim panelAnim; // 引用 PanelAnim 脚本

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
    void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }
}
