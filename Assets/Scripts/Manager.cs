using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject Arrow, StartScene, ShootButton, WinScene, LoseScene, Level, NewLevel;

    TMP_Text TargetText;
    public bool isWin, isLose, canShoot;
    public float arrowSpeed, levelIndex, currentArrow;
    public int[] arrowCount;

    void Update()
    {
        Target();
    }

    public void StartArrow()
    {
        Destroy(StartScene.transform.GetChild(0).GetChild(0).gameObject);

        GameObject newArrow = Instantiate(Arrow, new Vector2(0, -4), Quaternion.identity, GameObject.Find("StartObjs").transform);
        Destroy(newArrow.transform.GetChild(0).GetChild(0).gameObject);
        newArrow.transform.DOMoveY(GameObject.Find("Circle").transform.position.y - 2, arrowSpeed).OnComplete(() =>
        {
            newLevel();
            StartScene.SetActive(false);
            canShoot = true;
            ShootButton.SetActive(true);
        });
    }

    public void ShootArrow()
    {
        if (canShoot && !isWin && !isLose)
        {
            canShoot = false;

            GameObject newArrow = Instantiate(Arrow, new Vector2(0, -4), Quaternion.identity);
            newArrow.transform.parent = GameObject.Find("SpinningBall").transform.parent;
            newArrow.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = currentArrow.ToString();
            newArrow.transform.DOMoveY(GameObject.Find("SpinningBall").transform.position.y - 2, arrowSpeed - .5f).OnComplete(() =>
            {
                currentArrow += 1;
                newArrow.transform.parent = GameObject.Find("SpinningBall").transform;
                CheckWin();
                canShoot = true;
            });
        }
    }

    void Target()
    {
        if (GameObject.Find("Target") != null)
        {
            TargetText = GameObject.Find("Target").GetComponent<TMP_Text>();
            TargetText.text = ((currentArrow - 1) + " / " + arrowCount[(int)levelIndex]).ToString();
        }
    }

    void CheckWin()
    {
        if (currentArrow - 1 == arrowCount[(int)levelIndex])
        {
            WinScene.transform.GetChild(0).GetComponent<TMP_Text>().text = $"LEVEL {levelIndex + 1} COMPLATED!";
            isWin = true;
            ShootButton.SetActive(false);
            WinScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Lose()
    {
        if (!isLose)
        {
            LoseScene.transform.GetChild(0).GetComponent<TMP_Text>().text = $"LEVEL {levelIndex + 1} FAILED!";
            ShootButton.SetActive(false);
            LoseScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ReStart()
    {
        isLose = false;
        canShoot = true;
        currentArrow = 1;
        ShootButton.SetActive(true);
        LoseScene.SetActive(false);
        DestroyLevel();
        newLevel();
        NewLevel.SetActive(true);
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        isWin = false;
        levelIndex += 1;
        currentArrow = 1;
        ShootButton.SetActive(true);
        WinScene.SetActive(false);
        DestroyLevel();
        newLevel();
        NewLevel.SetActive(true);
        Time.timeScale = 1;
    }

    void newLevel()
    {
        NewLevel = Instantiate(Level);
        NewLevel.name = "NewLevel";
        NewLevel.SetActive(true);
    }

    void DestroyLevel()
    {
        Destroy(GameObject.Find("NewLevel"));
    }
}
