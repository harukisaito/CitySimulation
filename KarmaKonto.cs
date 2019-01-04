using System;

public class KarmaKonto
{
    private int karmaPoints;

    public int KarmaPoints 
    {
        get { return karmaPoints;}
        set { karmaPoints = value;}
    }

    public KarmaKonto(int karmaPoints)
    {
        this.karmaPoints = karmaPoints;
    }

    /// <summary>
    /// This method lets you gain karma.
    /// </summary>
    /// <param name="pointsToGain">Pass the amount of karma points gained as int.</param>
    public void GainKarma(int pointsToGain)
    {
        KarmaPoints += pointsToGain;
    }

    /// <summary>
    /// This method makes you lose karma.
    /// </summary>
    /// <param name="pointsToLose">Pass the amount of karma points lost as int.</param>
    public void LoseKarmaPoints(int pointsToLose)
    {
        KarmaPoints -= pointsToLose;
    }


    public override string ToString()
    {
        return "KarmaPoints= " + karmaPoints;
    }
}