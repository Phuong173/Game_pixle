using GameTool;
using UnityEngine;

namespace Pixel_Adventure_1.Scripts
{
    public class FakeFruit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PoolingManager.I.GetObject(ePooling.FruitCollected, transform.position, Quaternion.identity);
                AudioManager.I.Shot("Bonus");
                gameObject.SetActive(false);
            }
        }
    }
}