using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    [System.Serializable]
    public struct Values
    {
        [SerializeField] public float powerUse;

        [SerializeField] public float happiness;

        [SerializeField] public float pollution;

        [SerializeField] public float moneyGeneration;

        public Values(float inPowerUse, float inHappiness, float inPollution)
        {
            powerUse = inPowerUse; happiness = inHappiness; pollution = inPollution; moneyGeneration = 0;
        }

        public Values(float inPowerUse, float inHappiness, float inPollution, float inMoneyGeneration)
        {
            powerUse = inPowerUse; happiness = inHappiness; pollution = inPollution; moneyGeneration = inMoneyGeneration;
        }

    }
}

