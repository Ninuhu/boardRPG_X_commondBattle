
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RouletteUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform needlePivot;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button spinButton;

    [Header("回転設定")]
    [SerializeField] private float spinDuration = 3f;
    [SerializeField] private int minExtraRotations = 5;
    [SerializeField] private int maxExtraRotations = 8;

    private bool isSpinning;

    // 外部に結果を返す
    public void PlayRoulette(Action<int> onResult)
    {
        if (isSpinning) return;

        StartCoroutine(Spin(onResult));
    }

    private IEnumerator Spin(Action<int> onResult)
    {
        isSpinning = true;

        if (spinButton != null)
            spinButton.interactable = false;

        resultText.text = "";

        // 毎回0°から開始
        needlePivot.rotation = Quaternion.identity;

        // 結果決定
        int result = GetWeightedResult();

        // 停止位置取得
        float targetAngle = GetTargetAngle(result);

        // 5～8周 + 目標位置
        float extraRotation =
            360f * UnityEngine.Random.Range(
                minExtraRotations,
                maxExtraRotations + 1);

        // 時計回りなのでマイナス方向
        float startAngle = 0f;
        float finalAngle = -extraRotation - targetAngle;

        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            float t = elapsed / spinDuration;

            // Ease Out Cubic
            float easedT = 1f - Mathf.Pow(1f - t, 3);

            // 常に時計回り
            float angle = Mathf.Lerp(
                startAngle,
                finalAngle,
                easedT);

            needlePivot.rotation =
                Quaternion.Euler(0f, 0f, angle);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // 最終位置へ補正
        needlePivot.rotation =
            Quaternion.Euler(0f, 0f, finalAngle);

        Debug.Log($"停止角度 : {needlePivot.eulerAngles.z}");

        resultText.text = $"MOVE : {result}";

        // 結果通知
        onResult?.Invoke(result);

        if (spinButton != null)
            spinButton.interactable = true;

        isSpinning = false;
    }

    // 各数字の中央角度
    private float GetTargetAngle(int result)
    {
        switch (result)
        {
            case 0: return 6f;    // 0°～12°
            case 1: return 41f;   // 12°～70°
            case 2: return 99f;   // 70°～128°
            case 3: return 157f;  // 128°～186°
            case 4: return 215f;  // 186°～244°
            case 5: return 273f;  // 244°～302°
            case 6: return 331f;  // 302°～360°
            default: return 0f;
        }
    }

    // 確率付きルーレット
    private int GetWeightedResult()
    {
        int r = UnityEngine.Random.Range(0, 100);

        if (r < 4) return 0;

        if (r < 20) return 1;
        if (r < 36) return 2;
        if (r < 52) return 3;
        if (r < 68) return 4;
        if (r < 84) return 5;

        return 6;
    }
}