using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{ 
    [SerializeField] AudioClip breakingSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] breakingSprites;

    Level level;
    GameStatus gameStatus;

    [SerializeField] int timesHit = 0;

    void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable") level.CountingBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = breakingSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else ShowNextSpriteHit();

    }

    private void ShowNextSpriteHit()
    {
        int spriteIndex = timesHit - 1;
        if (breakingSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = breakingSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        TriggerSFX();
        Destroy(gameObject);
        level.BrokenBlock();
        gameStatus.IncreaseScore();
        TriggerSparklEffect();
    }

    private void TriggerSFX()
    {
        AudioSource.PlayClipAtPoint(breakingSound, Camera.main.transform.position);
    }

    private void TriggerSparklEffect()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
