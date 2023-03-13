using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenericClass<T> where T : Rigidbody
{
    public T StandardData;
  public TestGenericClass(T newData)
    {
        newData = StandardData;
    }
}
