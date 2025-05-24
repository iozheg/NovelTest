using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    int id;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite frontSprite;
    [SerializeField]
    Sprite backSprite;

    private MiniGameManager gameManager;
    private bool isFlipped = false;
    private bool isMatched = false;
    private bool isAnimating = false;

    void Start()
    {
        spriteRenderer.sprite = backSprite;
    }

    public void Init(int cardId, Sprite front, Sprite back, MiniGameManager gm)
    {
        id = cardId;
        frontSprite = front;
        backSprite = back;
        gameManager = gm;
    }

    void OnMouseDown()
    {
        if (isAnimating || isFlipped || isMatched || !gameManager.CanFlip(this)) return;

        Flip(true);
        gameManager.OnCardClicked(this);
    }

    public void Flip(bool faceUp)
    {
        isAnimating = true;

        transform.DOScaleX(0, 0.25f).OnComplete(() =>
        {
            spriteRenderer.sprite = faceUp ? frontSprite : backSprite;
            isFlipped = faceUp;

            transform.DOScaleX(1, 0.25f).OnComplete(() =>
            {
                isAnimating = false;
            });
        });
    }

    public void FlipUp() => Flip(true);
    public void FlipDown() => Flip(false);

    public void SetMatched()
    {
        isMatched = true;
    }

    public bool IsMatched() => isMatched;
    public int GetId() => id;

    void OnDestroy()
    {
        transform.DOKill();
    }
}
