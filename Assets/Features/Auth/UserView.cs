using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _rating;

    public string Name {
        get => _nameText.text;
        set => _nameText.text = value;
    }

    public string Rating
    {
        get => _rating.text;
        set => _rating.text = value;
    }
}
