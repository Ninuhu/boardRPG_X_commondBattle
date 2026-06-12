using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RouletteUI : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private TMP_Text resultText;

    [Header("回転設定")]
    [SerializeField] private float spinTime = 1.5f;   // 回転時間
    [SerializeField] private float minDelay = 0.02f;   // 最速
    [SerializeField] private float maxDelay = 0.2f;    // 最遅

    private Coroutine rouletteCoroutine;

    // ルーレット開始
    public void PlayRoulette(Action<int> onResult)
    {
        if (rouletteCoroutine != null)
            StopCoroutine(rouletteCoroutine);

        rouletteCoroutine = StartCoroutine(Roll(onResult));
    }

    private IEnumerator Roll(Action<int> onResult)
    {
        float time = 0f;

        resultText.text = "";

        int finalValue = GetResult();

        while (time < spinTime)
        {
            float t = time / spinTime;

            // 徐々に遅くする（加速→減速）
            float delay = Mathf.Lerp(minDelay, maxDelay, t);

            int randomValue = UnityEngine.Random.Range(0, 7);
            numberText.text = randomValue.ToString();

            yield return new WaitForSeconds(delay);
            time += delay;
        }

        // 最終結果
        numberText.text = finalValue.ToString();
        resultText.text = $"MOVE : {finalValue}";

        onResult?.Invoke(finalValue);
    }

    // 確率付きルーレット
    private int GetResult()
    {
        int r = UnityEngine.Random.Range(0, 100);

        // 0 → 4%
        if (r < 4) return 0;

        // 1〜6 → 各16%
        if (r < 20) return 1;
        if (r < 36) return 2;
        if (r < 52) return 3;
        if (r < 68) return 4;
        if (r < 84) return 5;

        return 6;
    }
}