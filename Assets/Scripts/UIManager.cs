using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
        public static UIManager Instance;

        public GameObject winPanel;
        public GameObject losePanel;

        [field:SerializeField]
        public Slider HpSlider { get; private set; }
        
        [field:SerializeField]
        public Text EnemiesText { get; private set; }
        
        [field:SerializeField]
        public Text HpText { get; private set; }

        public void Restart()
        {
                SceneManager.LoadScene(0);
        }

        private void OnEnable()
        {
                Instance = this;
        }

        private void OnDisable()
        {
                Instance = null;
        }
}