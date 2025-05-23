using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class AdsLoadingOverlay : MonoBehaviour
{
    [SerializeField]
    private Image _overlay;

    [SerializeField]
    private RectTransform _loadingIndicator;

    [SerializeField]
    private float _rotationSpeed = 1f;

    [SerializeField]
    private Ease _ease = Ease.InOutBounce;

    [SerializeField]
    private float _overlayTimeThreshold = 15f;

    private Sequence _loadingAnimation;
    private float _overlayTime;

    public void Restart()
    {
        _loadingIndicator.DOKill();
        StartAnimation();
    }

    private void Start()
    {
        AdsManager.Instance.OnAdsStarted += StartAnimation;
        AdsManager.Instance.OnAdsFinished += StopAnimation;
        StopAnimation();
        gameObject.SetActive(false);
    }

    private void StartAnimation()
    {
        gameObject.SetActive(true);
        _overlay.enabled = true;
        _loadingIndicator.gameObject.SetActive(true);

        _loadingAnimation = DOTween.Sequence();
        _loadingAnimation
            .AppendInterval(0.5f)
            .Append(
                _loadingIndicator.DORotate(
                    new Vector3(0, 0, -360),
                    _rotationSpeed,
                    RotateMode.FastBeyond360
                )
                .SetEase(_ease)
            )
            .SetLoops(-1, LoopType.Restart);
        _overlayTime = 0f;
    }

    private void StopAnimation()
    {
        _overlay.enabled = false;
        _loadingIndicator.transform.localRotation = Quaternion.identity;
        _loadingIndicator.gameObject.SetActive(false);
        _loadingAnimation.Kill();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        AdsManager.Instance.OnAdsStarted -= StartAnimation;
        AdsManager.Instance.OnAdsFinished -= StopAnimation;
    }

    private void Update()
    {
        _overlayTime += Time.deltaTime;
        if (_overlayTime >= _overlayTimeThreshold)
        {
            StopAnimation();
        }
    }
}
