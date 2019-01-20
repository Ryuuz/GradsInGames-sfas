using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatType
{
    ginger, tabby, black
};

public class AIType : MonoBehaviour
{
    public CatType m_CatType;

    [HideInInspector]
    public KibbleType m_KibbleType;

    // Start is called before the first frame update
    void Start()
    {
        switch(m_CatType)
        {
            case CatType.black: m_KibbleType = KibbleType.star;
                break;
            case CatType.ginger: m_KibbleType = KibbleType.fish;
                break;
            case CatType.tabby: m_KibbleType = KibbleType.triangle;
                break;
        }
    }
}
