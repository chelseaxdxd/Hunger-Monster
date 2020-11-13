using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpText : MonoBehaviour
{
    Text text;
    public static int HpAmount;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        HpAmount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = HpAmount.ToString();
        if (HpAmount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
