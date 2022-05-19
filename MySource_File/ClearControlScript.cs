using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearControlScript : MonoBehaviour
{
    // 各ステージのクリア状態を判定する用のを管理するスクリプト
    // ステージセレクト画面やゲーム本編のクリア時に使用

    // 各ステージのクリア状況を判別するための変数
    public static bool EasyClear;           // Easyモード用
    public static bool NormalClear;         // Normalモード用
    public static bool HardClear;           // Hardモード用
    public static bool VeryHardClear;       // VeryHardモード用

    // 各ステージの解放後に表示される画像
    public Image NormalImage;               // Normalモード用
    public Image HardImage;                 // Hardモード用
    public Image VeryHardImage;             // VeryHardモード用

    // 各ステージの解放後に表示されるテキスト
    public TextMeshProUGUI NormalText;      // Normalモード用
    public TextMeshProUGUI HardText;        // Hardモード用
    public TextMeshProUGUI VeryHardText;    // VeryHardモード用

    // 各ステージの未開放中に表示される画像
    public Image NotNormalImage;            // Normalモード用
    public Image NotHardImage;              // Hardモード用
    public Image NotVeryHardImage;          // VeryHardモード用

    // 各ステージの未開放中に表示されるテキスト
    public TextMeshProUGUI NotNormalText;   // Normalモード用
    public TextMeshProUGUI NotHardText;     // Hardモード用
    public TextMeshProUGUI NotVeryHardText; // VeryHardモード用

    // Start is called before the first frame update
    void Start()
    {
        // 解放時に使用する画像とテキストはfalse(非表示)
        NormalImage.enabled = false;
        HardImage.enabled = false;
        VeryHardImage.enabled = false;

        NormalText.enabled = false;
        HardText.enabled = false;
        VeryHardText.enabled = false;

        // 未開放用に使用する画像とテキストはtrue(表示)
        NotNormalImage.enabled = true;
        NotHardImage.enabled = true;
        NotVeryHardImage.enabled = true;

        NotNormalText.enabled = true;
        NotHardText.enabled = true;
        NotVeryHardText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        // Easyモードクリア済みの場合
        if (EasyClear)
        {
            // Normalモード未開放用の画像とテキストをfalse(非表示)
            NotNormalImage.enabled = false;
            NotNormalText.enabled = false;

            // Normalモード解放時に使用する画像とテキストをtrue(表示)
            NormalImage.enabled = true;
            NormalText.enabled = true;
        }

        // Normalモードクリア済みの場合
        if (NormalClear)
        {
            // Hardモード未開放用の画像とテキストをfalse(非表示)
            NotHardImage.enabled = false;
            NotHardText.enabled = false;

            // Hardモード解放時に使用する画像とテキストをtrue(表示)
            HardImage.enabled = true;
            HardText.enabled = true;
        }

        // Hardモードクリア済みの場合
        if (HardClear)
        {
            // VeryHardモード未開放用の画像とテキストをfalse(非表示)
            NotVeryHardImage.enabled = false;
            NotVeryHardText.enabled = false;

            // VeryHardモード解放時に使用する画像とテキストをtrue(表示)
            VeryHardImage.enabled = true;
            VeryHardText.enabled = true;
        }

    }
}
