using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerLogic
{
    public class PlayerBehaviour : PlayerBase
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] 
        private float cooldown;

        [SerializeField] 
        private Button bombButton;

        [SerializeField] 
        private Sprite[] avatars;
        
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        [SerializeField] 
        private Joystick joystick;

        [SerializeField]
        private HeartBody heartBody;

        [SerializeField] 
        private GameObject bombPrefab;

        [SerializeField] 
        private float speed;
        
        private int _direction;
        private Coroutine _bombCooldown;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            heartBody.OnStart += delegate(float f) { UIManager.Instance.HpSlider.maxValue = f; UIManager.Instance.HpSlider.value = f; };
            heartBody.OnDamaged += UpdateUi;
        }
        
        private void FixedUpdate()
        {
            ProcessInputs();
        }

        private void ProcessInputs()
        {
            _rigidbody2D.velocity = new Vector2(joystick.Horizontal*speed, joystick.Vertical*speed);

            TurnInDirection();
        }

        public void DropBomb()
        {
            if(_bombCooldown != null) return;
            _bombCooldown = StartCoroutine(BombCooldown());
        }

        private IEnumerator BombCooldown()
        {
            CreateBomb();
            bombButton.interactable = false;
            yield return new WaitForSeconds(cooldown);
            bombButton.interactable = true;
            _bombCooldown = null;
        }
        
        private void CreateBomb()
        {
            var bomb = Instantiate(bombPrefab);
            bomb.transform.position = transform.position;
        }
        
        private void TurnInDirection()
        {
            if (Mathf.Abs(_rigidbody2D.velocity.x) >= Mathf.Abs(_rigidbody2D.velocity.y))
                _direction = _rigidbody2D.velocity.x > 0 ? 0 : 1;
            else
                _direction = _rigidbody2D.velocity.y > 0 ? 3 : 2;
            
            spriteRenderer.sprite = avatars[_direction];
        }

        private void UpdateUi(float newHp)
        {
            UIManager.Instance.HpSlider.value = newHp;
            UIManager.Instance.HpText.text = $"{newHp} HP";
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out HeartBody otherBody))
            {
                heartBody.Damage(.5f);
            }
        }
    }
}