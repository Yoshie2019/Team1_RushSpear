using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearControlScript : MonoBehaviour
{

    public static bool EasyClear;
    public static bool NormalClear;
    public static bool HardClear;
    public static bool VeryHardClear;

    public Image NormalImage;
    public Image HardImage;
    public Image VeryHardImage;

    public TextMeshProUGUI NormalText;
    public TextMeshProUGUI HardText;
    public TextMeshProUGUI VeryHardText;

    public Image NotNormalImage;
    public Image NotHardImage;
    public Image NotVeryHardImage;

    public TextMeshProUGUI NotNormalText;
    public TextMeshProUGUI NotHardText;
    public TextMeshProUGUI NotVeryHardText;

    // Start is called before the first frame update
    void Start()
    {
        NormalImage.enabled = false;
        HardImage.enabled = false;
        VeryHardImage.enabled = false;

        NormalText.enabled = false;
        HardText.enabled = false;
        VeryHardText.enabled = false;

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

        if (EasyClear)
        {
            NotNormalImage.enabled = false;
            NotNormalText.enabled = false;

            NormalImage.enabled = true;
            NormalText.enabled = true;
        }

        if (NormalClear)
        {
            NotHardImage.enabled = false;
            NotHardText.enabled = false;

            HardImage.enabled = true;
            HardText.enabled = true;
        }

        if (HardClear)
        {
            NotVeryHardImage.enabled = false;
            NotVeryHardText.enabled = false;

            VeryHardImage.enabled = true;
            VeryHardText.enabled = true;
        }

    }
}
