using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo {
    public Dictionary<string,float> floatParameters;
    public Dictionary<string, int> intParameters;
    public Dictionary<string, bool> boolParameters;
    public EventInfo()
    {
        floatParameters = new Dictionary<string, float>();
        intParameters = new Dictionary<string, int>();
        boolParameters = new Dictionary<string, bool>();
    }

    public bool AddBool(string parameterName, bool parameterValue)
    {
        if (boolParameters.ContainsKey(parameterName))            
        {
            boolParameters[parameterName] = parameterValue;
            return false;
        }
        else{
            boolParameters.Add(parameterName, parameterValue);
            return true;
        }           
    }
    public bool SetBool(string parameterName, bool parameterValue)
    {
        if (boolParameters.ContainsKey(parameterName))
        {
            boolParameters[parameterName] = parameterValue;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetBool(string parameterName)
    {
        if (boolParameters.ContainsKey(parameterName))
        {
            return boolParameters[parameterName];
        }
        else
        {
            return false;
        }
    }
    public bool AddFloat(string parameterName, float parameterValue)
    {
        if (floatParameters.ContainsKey(parameterName))
        {
            floatParameters[parameterName] = parameterValue;
            return false;
        }
        else
        {
            floatParameters.Add(parameterName, parameterValue);
            return true;
        }
    }
    public bool SetFloat(string parameterName, float parameterValue)
    {
        if (floatParameters.ContainsKey(parameterName))
        {
            floatParameters[parameterName] = parameterValue;
            return true;
        }
        else
        {
            return false;
        }
    }
    public float GetFloat(string parameterName)
    {
        if (floatParameters.ContainsKey(parameterName))
        {
            return floatParameters[parameterName];
        }
        else
        {
            return 0f;
        }
    }

    public bool AddInt(string parameterName, int parameterValue)
    {
        if (intParameters.ContainsKey(parameterName))
        {
            intParameters[parameterName] = parameterValue;
            return false;
        }
        else
        {
            intParameters.Add(parameterName, parameterValue);
            return true;
        }
    }
    public bool SetInt(string parameterName, int parameterValue)
    {
        if (intParameters.ContainsKey(parameterName))
        {
            intParameters[parameterName] = parameterValue;
            return true;
        }
        else
        {
            return false;
        }
    }
    public int GetInt(string parameterName)
    {
        if (intParameters.ContainsKey(parameterName))
        {
            return intParameters[parameterName];
        }
        else
        {
            return 0;
        }
    }
}
