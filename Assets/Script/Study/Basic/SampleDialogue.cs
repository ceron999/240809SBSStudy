using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleDialogue : MonoBehaviour
{
    public bool isWaitInput = false;

    public string questionMessage;

    public string[] answerGroup_1;
    public string[] answerGroup_2;
    public string[] answerGroup_3;

    private void Start()
    {
        ShowQuestion();
    }

    private void Update()
    {
        if (isWaitInput == true)
        {
            bool isInputTaken = false;

            // To do : 1,2,3번키 입력을 받는 부분을 구현해본다.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 1번 그룹에서 랜덤한 값을 출력한다.
                int random = Random.Range(0, answerGroup_1.Length);
                Debug.Log(answerGroup_1[random]);

                isInputTaken = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 2번 그룹에서 랜덤한 값을 출력한다.
                int random = Random.Range(0, answerGroup_2.Length);
                Debug.Log(answerGroup_2[random]);

                isInputTaken = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // 3번 그룹에서 랜덤한 값을 출력한다.
                int random = Random.Range(0, answerGroup_3.Length);
                Debug.Log(answerGroup_3[random]);

                isInputTaken = true;
            }

            // To do : Space 키 입력을 받으면 1,2,3번 선택한 문장을 출력하고, isWaitInput을 false 로 변경한다.
            if (isInputTaken)
            {
                isWaitInput = false;
                ShowQuestion();
            }
        }
    }

    public void ShowQuestion()
    {
        Debug.Log(questionMessage);
        isWaitInput = true;
    }
}

