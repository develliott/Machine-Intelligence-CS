﻿namespace CS_GA.Business.Common.Data_Structure
{
    public interface IPopulation
    {
        IIndividual MostSuitableIndividualToProblem { get; set; }
        int Size { get; }
        void InitialisePopulation();
        IIndividual GetIndividual(int populationIndex);
        void SetIndividual(int populationIndex, IIndividual individual);
    }
}