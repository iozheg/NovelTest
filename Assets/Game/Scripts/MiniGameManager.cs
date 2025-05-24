using System.Collections;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;
    [SerializeField]
    Sprite[] frontSprites;
    [SerializeField]
    Sprite backSprite;
    [SerializeField]
    int columns = 4;
    [SerializeField]
    int rows = 3;
    [SerializeField]
    float spacing = 2f;

    private readonly List<Card> cards = new ();
    private Card firstCard, secondCard;
    private bool canClick = true;
    private int pairsFound = 0;

    void Start()
    {
        GenerateCards();
    }

    void GenerateCards()
    {
        List<int> ids = new ();
        for (int i = 0; i < frontSprites.Length; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }
        Shuffle(ids);

        // Расчёт общего размера сетки
        float width = columns * spacing;
        float height = rows * spacing;
        Vector2 offset = new Vector2(width / 2f - spacing / 2f, height / 2f - spacing / 2f);

        for (int i = 0; i < ids.Count; i++)
        {
            int row = i / columns;
            int col = i % columns;

            float x = col * spacing - offset.x;
            float y = -row * spacing + offset.y;

            Vector3 pos = new Vector3(x, y, 0f);

            GameObject obj = Instantiate(cardPrefab, pos, Quaternion.identity, transform);
            Card card = obj.GetComponent<Card>();
            card.Init(ids[i], frontSprites[ids[i]], backSprite, this);
            cards.Add(card);
        }
    }

    public void OnCardClicked(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        canClick = false;

        if (firstCard.GetId() == secondCard.GetId())
        {
            firstCard.SetMatched();
            secondCard.SetMatched();
            pairsFound++;

            if (pairsFound == frontSprites.Length)
            {
                var player = Engine.GetService<IScriptPlayer>();
                player.PlayFromLabel("GameCompleted");
                SceneManager.UnloadSceneAsync("MiniGameScene");
            }
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            firstCard.FlipDown();
            secondCard.FlipDown();
        }

        firstCard = null;
        secondCard = null;
        canClick = true;
    }

    public bool CanFlip(Card card)
    {
        return canClick && (firstCard == null || secondCard == null);
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            int tmp = list[i];
            list[i] = list[rnd];
            list[rnd] = tmp;
        }
    }
}
