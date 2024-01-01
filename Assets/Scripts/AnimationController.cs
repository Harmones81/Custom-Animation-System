using UnityEngine;

namespace CustomAnimation.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimationController : MonoBehaviour
    {
        public Animation CurrentAnimation { get; private set; }
        public int CurrentKeyframe { get; private set; }

        public bool IsFinished => CurrentKeyframe >= _frameCount - 1 && _durationTimer >= _frameDuration;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animation _defualtAnimation;

        private float _durationTimer;
        private int _frameRate => (int)(1f / Time.unscaledDeltaTime);
        private int _frameCount => CurrentAnimation.KeyframeCount;
        private float _frameDuration => CurrentAnimation.keyframes[CurrentKeyframe].duration;

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

        private void Start() => CurrentAnimation = _defualtAnimation;

        private void Update() => PlayAnimation();

        private void PlayAnimation()
        {
            float incrementValue = 1f / _frameRate;

            _spriteRenderer.sprite = CurrentAnimation.keyframes[CurrentKeyframe].sprite;

            if (_durationTimer < _frameDuration)
            {
                _durationTimer += incrementValue;
            }
            else
            {
                CurrentKeyframe++;

                if (IsFinished)
                {
                    if (CurrentAnimation.loop)
                    {
                        CurrentKeyframe = CurrentAnimation.loopFrame;
                    }
                }

                _durationTimer = 0;
            }
        }

        private void OnGUI()
        {
            float middle = Screen.width / 2;
            float right = Screen.width - 250;
            float left = 150;
            
            GUI.Label(new Rect(middle, 20f, 150f, 150f), "FPS: " + (int)(1f / Time.unscaledDeltaTime));
            GUI.Label(new Rect(right, 20f, 800f, 150f), "Frame Count: " + CurrentAnimation.CalculateTotalFrameCount());
            GUI.Label(new Rect(left, 20f, 500f, 150f), "Current Animation: " + CurrentAnimation.name);
        }
    }
}
