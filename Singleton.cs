using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// where T : ??  >  제네릭 타입 자료형에게 제한을 둔다.
public class Singleton<T> : MonoBehaviour
    where T : class
{
    // 변수명이 대문자인 이유는?
    // = 보통 변수는 소문자이지만 해당 변수를 읽기 전용으로 공개할 경우
    //   동일한 이름을 사용해야하기 때문에 이때는 대문자로 사용한다.
    public static T Instance { get; private set; }

    protected int money;

    protected void Awake()
    {
        Instance = this as T;
    }
}

