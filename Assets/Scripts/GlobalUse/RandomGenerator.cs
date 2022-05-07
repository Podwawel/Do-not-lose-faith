using System;
public static class RandomGenerator
{
   public static int ReturnRandomInt(int randomFrom, int randomTo)
   {
        Random randomer = new Random();

        return randomer.Next(randomFrom, randomTo);
   }

    public static float ReturnRandomPropability()
    {
        Random randomer = new Random();

        return (float)randomer.NextDouble();
    }

    public static float ReturnRandomFloat(float randomFrom, float randomTo)
    {
        float range = randomTo - randomFrom;

        float sample = ReturnRandomPropability();

        float scaled = (sample * range) + randomFrom;

        return scaled;
    }
}
