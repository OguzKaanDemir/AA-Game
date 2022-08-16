using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpinningBall : MonoBehaviour
{
    [SerializeField] Manager Manager;
    [SerializeField] TMP_Text Text;

    void Awake()
    {
        Manager = FindObjectOfType<Manager>();
        Text.text = (Manager.levelIndex + 1).ToString();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, (Manager.levelIndex + 1) * .2f), Space.Self);
    }
}
