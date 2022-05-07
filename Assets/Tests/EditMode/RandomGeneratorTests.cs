using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RandomGeneratorTests
{
    [Test]
    public void IsWithinWideRange_ValueIsInGivenRange_True()
    {
        float from = 1234.124f;
        float to = 15294.41f;

        for(int i = 0; i < 100; i++)
        {
            float randomFloat = RandomGenerator.ReturnRandomFloat(from, to);
            Assert.Less(randomFloat, to);
            Assert.Greater(randomFloat, from);
        }
    }

    [Test]
    public void IsWithinCloseRange_ValueIsInGivenRange_True()
    {
        float from = 0.34f;
        float to = 0.64f;

        for (int i = 0; i < 100; i++)
        {
            float randomFloat = RandomGenerator.ReturnRandomFloat(from, to);
            Assert.Less(randomFloat, to);
            Assert.Greater(randomFloat, from);
        }
    }


}
