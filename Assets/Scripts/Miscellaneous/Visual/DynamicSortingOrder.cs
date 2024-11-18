using UnityEngine;

namespace Miscellaneous.Visual
{
    [ExecuteInEditMode]
    public class DynamicSortingOrder : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
    
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        void Update()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}
