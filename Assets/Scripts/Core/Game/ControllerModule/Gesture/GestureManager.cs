using UnityEngine;

namespace Core.Game.ControllerModule.Gesture
{
    public class GestureManager : MonoBehaviour
    {
        private static GestureManager _instance;
        public static GestureManager Instance => _instance ??= new GameObject("GestureManager").AddComponent<GestureManager>();
    
        public bool IsTwoFingerGestureActive { get; set; } = false;

        private void Awake()
        {
            // if (_instance == null) _instance = this;
            // else Destroy(gameObject);
            // DontDestroyOnLoad(gameObject);
            if (_instance == null) 
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // 使该实例在场景切换时保持
            }
            else 
            {
                Destroy(gameObject); // 确保只有一个实例存在
            }
        }
    }
}
