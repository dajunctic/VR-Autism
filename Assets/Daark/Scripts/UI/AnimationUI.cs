using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Daark
{
    public class AnimationUI : MonoBehaviour
    {
        [SerializeField] private AnimType animType;
#region Move
        [Header("Move Animation")]
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private Transform targetPos;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private Transform startPos;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] public Vector3 startPosVector3;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private float moveTime = 0.4f;
        [ShowIf("@animType == AnimType.Move && setPrePosition == true")]
        [SerializeField] private Vector3 moveDestination;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private float delayBeforeMove = 0f;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private bool moveFromStart;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private bool setPrePosition = false;
        [ShowIf("@animType == AnimType.Move")]
        [SerializeField] private bool autoSetStartPos = false;
#endregion

#region Pop
        [Header("Pop Animation")]
        [ShowIf("@animType == AnimType.Pop")]
        [SerializeField] private float popTime = 0.4f;
        [ShowIf("@animType == AnimType.Pop")]
        [SerializeField] private bool popFromStart = false;
        [ShowIf("@animType == AnimType.Pop")]
        [SerializeField] private Vector3 popScale = Vector3.one;
        [ShowIf("@animType == AnimType.Pop")]
        [SerializeField] private float delayBeforePop = 0f;
#endregion

#region Scale

        [Header("Scale Animation")] 
        [ShowIf("@animType == AnimType.Scale")] 
        [SerializeField] private bool scaleFromStart;
        [ShowIf("@animType == AnimType.Scale")]
        [SerializeField] private Vector3 scaleAmount = new Vector3(0, 0, 0);
        [ShowIf("@animType == AnimType.Scale")]
        [SerializeField] private Axis axis;
        [ShowIf("@animType == AnimType.Scale")]
        [SerializeField] private float scaleTime = 0.4f;
        [ShowIf("@animType == AnimType.Scale")]
        [SerializeField] private float delayTime = 0f;
        [ShowIf("@animType == AnimType.Scale")]
        [SerializeField] private bool dontSetPreScale = false;
#endregion

#region Shake
        [Header("Shake Animation - For RectTransform")]
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField] private LoopType loopType;
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField] bool loopFromStart = false;
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField] private float shakeDuration  = 1;
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField] private int shakeVibrato  = 1;
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField] private float shakeStrength  = 1;
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField]private float shakeRandom  = 1;
        [ShowIf("@animType == AnimType.Shake")]
        [SerializeField]private int loop = -1;
#endregion

#region Scale Idle
        [Header("Scale Idle")]
        [ShowIf("@animType == AnimType.ScaleIdle")]
        [SerializeField] private bool scaleIdleFromStart;
        [ShowIf("@animType == AnimType.ScaleIdle")]
        [SerializeField] private Vector3 scaleIdleSize;
        [ShowIf("@animType == AnimType.ScaleIdle")]
        [SerializeField] private float scaleIdleDuration;
#endregion

#region Jump Idle
        [Header("Jump Idle")]
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private bool jumpIdleFromStart;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private int jumpIdleNumJump;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private Vector3 jumpIdleEndValue;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private float jumpIdleDuration;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private float jumpIdleJumpPower;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private int jumpIdleLoop;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private LoopType jumpIdleLoopType;
        [ShowIf("@animType == AnimType.JumpIdle")]
        [SerializeField] private float timeJumpDelay;
#endregion

#region Rotate Idle
        [ShowIf("@animType == AnimType.RotateIdle")]
        [SerializeField] private float cycleLength = 5f;
#endregion

#region Rotate
        [ShowIf("@animType == AnimType.Rotate")]
        [SerializeField] private Vector3 firstRotatePos;
        [ShowIf("@animType == AnimType.Rotate")]
        [SerializeField] private Vector3 secondRotatePos;
        [ShowIf("@animType == AnimType.Rotate")]
        [SerializeField] private float rotateDuration;
        [ShowIf("@animType == AnimType.Rotate")]
        [SerializeField] private LoopType rotateLoopType;
        [ShowIf("@animType == AnimType.Rotate")]
        [SerializeField] private int rotateLoop;
#endregion

        [ShowIf("@animType != AnimType.Shake && animType != AnimType.ScaleIdle && animType != AnimType.JumpIdle && animType != AnimType.HandleInScript && animType != AnimType.TypeText && animType != AnimType.Rotate")]
        [SerializeField] private Ease ease;
        [SerializeField] private bool setUpdate = true;

        private void OnEnable()
        {
            if(animType == AnimType.Pop)
            {
                this.transform.localScale = Vector3.zero;
            }
            if(animType == AnimType.Scale && !dontSetPreScale)
            {
                switch(axis)
                {
                    case Axis.x:
                    this.transform.localScale = new Vector3(0, 1, 1);
                    break;
                    case Axis.y:
                    this.transform.localScale = new Vector3(1, 0, 1);
                    break;
                    case Axis.z:
                    this.transform.localScale = new Vector3(1, 1, 0);
                    break;
                    case Axis.All:
                    this.transform.localScale = new Vector3(0, 0, 0);
                    break;
                }
            }
            if(animType == AnimType.Move)
            {
                if(startPos != null)
                    this.transform.position = startPos.transform.position;
            }
            if(animType == AnimType.Move && autoSetStartPos)
            {
                if(startPos == null)
                {
                    startPosVector3 = this.transform.position;
                }
                    
            }
            if(animType == AnimType.Pop && popFromStart == true)
            {
                PopUp();
            }
            PlayAnimation();
        }

        void OnDisable()
        {
            DisableDoTween();
        }

        private void Start()
        {
            PlayAnimation();
        }

        public void ResetPosition()
        {
            if(animType == AnimType.Move && !autoSetStartPos)
            {
                if(startPos != null)
                    this.transform.position = startPos.transform.position;
            }
            else
            {
                this.transform.position = startPosVector3;
            }
        }

        public void PlayAnimation()
        {
            if(animType == AnimType.Shake && loopFromStart == true)
            {
                Shake();
            }
            else if(animType == AnimType.Move && moveFromStart == true)
            {
                MoveToPos();
            }
            else if(animType == AnimType.ScaleIdle && scaleIdleFromStart == true)
            {
                ScaleIdle();
            }
            else if(animType == AnimType.JumpIdle && jumpIdleFromStart == true)
            {
                JumpIdle();
            }
            else if(animType == AnimType.Pop && popFromStart == true)
            {
                PopUp();
            }
            else if(animType == AnimType.RotateIdle)
            {
                RotateIdle();
            }
            else if (animType == AnimType.Scale && scaleFromStart)
            {
                Scale();
            }
        }

        public void MoveToPos(System.Action onComplete = null)
        {
            if(setPrePosition == false && targetPos != null)
            {
                transform.DOMove(targetPos.transform.position, moveTime).SetEase(ease).SetTarget(this).SetDelay(delayBeforeMove).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            }
            else
            {
                transform.DOMove(moveDestination, moveTime).SetEase(ease).SetTarget(this).SetDelay(delayBeforeMove).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            }
        }

        public void PopUp(System.Action onComplete = null)
        {
            transform.DOScale(popScale, popTime).SetEase(ease).SetTarget(this).SetDelay(delayBeforePop).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        public void DoLocalMove(Vector3 destination,System.Action onComplete = null)
        {
            transform.DOLocalMove(destination, 0.4f).SetEase(ease).SetTarget(this).SetDelay(delayBeforeMove).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        public void DoMove(Vector3 destination,System.Action onComplete = null)
        {
            transform.DOMove(destination, 0.4f).SetEase(ease).SetTarget(this).SetDelay(delayBeforeMove).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        private IEnumerator FadeRoutine(bool isFadeIn)
        {
            if(this.TryGetComponent<Image>(out Image img))
            {
                float targetAlpha = isFadeIn ? 1.0f : 0.0f;
                float fadeRate = 0;
                Color curColor = img.material.color;
                while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
                {
                    curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
                    img.material.color = curColor;
                    yield return null;
                }
            }
        }

        public void Shake()
        {
            var rect = GetComponent<RectTransform>();
            if (rect)
            {
                rect.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, shakeRandom).SetLoops(loop, loopType).SetTarget(this);
            }
        }

        public void Shake(float shakeDuration, int shakeStrength, int shakeVibrato, int shakeRandom, int loop, LoopType loopType, System.Action onComplete = null)
        {
            var rect = GetComponent<RectTransform>();
            if (rect)
            {
                rect.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, shakeRandom).SetLoops(loop, loopType).SetUpdate(setUpdate).SetTarget(this).OnComplete(()=>
                {
                    onComplete?.Invoke();
                });    
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public void Scale(System.Action onComplete = null)
        {
            transform.DOScale(scaleAmount, scaleTime).SetEase(ease).SetUpdate(setUpdate).SetDelay(delayTime).SetTarget(this).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }

        public void Scale(Vector3 startSize ,Vector3 scaleSize, float duration, Ease easeType ,System.Action onComplete = null)
        {
            transform.localScale = startSize;
            transform.DOScale(scaleSize,duration).SetEase(easeType).SetUpdate(setUpdate).SetTarget(this).OnComplete(()=>
            {
                onComplete?.Invoke();
            });
        }

        public void ScaleIdle()
        {
            transform.DOScale(scaleIdleSize, scaleIdleDuration).SetLoops(-1, LoopType.Yoyo).SetUpdate(setUpdate).SetTarget(this);
        }
        public void ScaleIdle(Vector3 scaleSize, float scaleIdleDuration)
        {
            transform.DOScale(scaleSize, scaleIdleDuration).SetLoops(-1, LoopType.Yoyo).SetUpdate(setUpdate).SetTarget(this);
        }

        public void JumpIdle()
        {
            // this.Delay(timeJumpDelay, true, () =>
            // {
            //     transform.DOLocalJump(jumpIdleEndValue, jumpIdleJumpPower, jumpIdleNumJump, jumpIdleDuration).SetLoops(jumpIdleLoop, jumpIdleLoopType).SetTarget(this);
            // });
        }
        public void ClosePop(System.Action onComplete = null)
        {
            transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).SetUpdate(setUpdate).SetTarget(this).OnComplete(()=>
            {
                onComplete?.Invoke();
            });
        }

        public void DisableDoTween()
        {
            // transform.DOKill(false);
            DOTween.Kill(this);
        }

        public void RotateIdle()
        {
            transform.DORotate(new Vector3(0, 0, 360), cycleLength, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetUpdate(true).SetTarget(this);
        }

        public void TypingEffect(string text, TextMeshProUGUI tmpText, float delay)
        {
            StartCoroutine(Typewriter(text, tmpText, delay));
            // DOTween.Sequence().SetDelay(delay).AppendCallback(()=>{
            //     StartCoroutine(Typewriter(text, tmpText));
            // }).SetUpdate(setUpdate);
        }   

        private IEnumerator Typewriter(string text, TextMeshProUGUI tmpText, float delay)
        {
            if (setUpdate) // realtime
            {
                yield return new WaitForSecondsRealtime(delay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
            tmpText.text = "";
            foreach (char c in text)
            {
                tmpText.text = tmpText.text + c;
                yield return new WaitForSeconds(0.03f);
            }
            yield return null;
        }

        public void Rotate()
        {
            transform.DORotate(firstRotatePos, rotateDuration).SetTarget(this).SetLoops(rotateLoop, rotateLoopType).SetUpdate(setUpdate).OnComplete(()=>
            {
                transform.DORotate(secondRotatePos, rotateDuration).SetTarget(this);
            });
        }

        public void Rotate(Vector3 firstRotatePos, Vector3 secondRotatePos, float rotateDuration, int rotateLoop, LoopType rotateLoopType)
        {
            transform.DOLocalRotate(firstRotatePos, rotateDuration).SetLoops(rotateLoop, rotateLoopType).SetUpdate(true).SetTarget(this).OnComplete(() =>
                {
                    transform.DOLocalRotate(secondRotatePos, rotateDuration).SetTarget(this).OnComplete(() =>
                    {
                        transform.DOLocalRotate(firstRotatePos, rotateDuration).SetTarget(this);
                    });
                });
        }
    }

    public enum Axis
    {
        x,
        y,
        z,
        All
    }

    public enum AnimType
    {
        Pop,
        Move,
        Scale,
        Shake,
        ScaleIdle,
        JumpIdle,
        RotateIdle,
        TypeText,
        HandleInScript,
        Rotate,
    }
}