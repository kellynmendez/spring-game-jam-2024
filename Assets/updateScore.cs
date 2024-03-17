using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateScore : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;

    // Start is called before the first frame update
    private void Awake()
    {
        _scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
