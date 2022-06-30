using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FX;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Color = UnityEngine.Color;

public class FadeOutOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    async UniTaskVoid Start() {
        var img = GetComponent<RawImage>();
        img.color = Color.black;
        await Effect.FadeOut(0.5f, alpha => img.color = img.color.WithAlpha(alpha));
    }

}
