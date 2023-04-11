using System;

// THIS IS A MONTE CARLO SIMULATION FOR THE TRANSITION MATRIX
// OF THE ENEMY MARKOV STATE AUTOMATON
public class Simulation
{
    private static string[] states = {
        "Idle", "Move", "Attack"
    };
    
    private static double[,] transitionMatrix = new double[3,3]{
        { 0.1, 0.6, 0.3 }, // From idle(0), to 0,1,2
        { 0.1, 0.5, 0.4 }, // From move(1), to 0,1,2
        { 0, 1, 0 }, // From atk(2), to 0,1,2
    };

    public static void Main(string[] args)
    {
        Random rand;
        double[] probabilities = new double[3];
        double rng;
        int simulationLength = 500;
        int _Idle;
        int _Move;
        int _Atk;

        for(int j=0; j<3; j++){
            rand = new Random();
            _Idle = 0;
            _Move = 0;
            _Atk = 0;
            
            System.Console.Write("\n\n========== ");
            System.Console.Write(states[j]+" State");
            System.Console.WriteLine(" ==========");
            probabilities = GetRow(transitionMatrix, j);
            
            for(int i=0; i<simulationLength; i++){
            rng = rand.NextDouble();

            if (rng <= probabilities[0])
                _Idle++;
            else if (rng <= probabilities[0]+probabilities[1])
                _Move++;
            else
                _Atk++;
        }
        
        double idleAve = (double)_Idle/(double)simulationLength;
        double idleVar = Variance(_Idle, simulationLength, idleAve);
        double idleStd = StdDev(idleVar);
        
        
        double moveAve = (double)_Move/(double)simulationLength;
        double moveVar = Variance(_Move, simulationLength, moveAve);
        double moveStd = StdDev(moveVar);
        
        double atkAve = (double)_Atk/(double)simulationLength;
        
        double atkVar = Variance(_Atk, simulationLength, atkAve);
        double atkStd = StdDev(atkVar);
        
        System.Console.WriteLine("To Idle Ave: "+idleAve);
        System.Console.WriteLine("Variance: " + idleVar);
        System.Console.WriteLine("Standard Deviation: " + idleStd);
        
        System.Console.WriteLine("\nTo Move Ave: "+moveAve);
        System.Console.WriteLine("Move Variance: " + moveVar);
        System.Console.WriteLine("Move Standard Deviation: " + moveStd);
        
        System.Console.WriteLine("\nTo Atk Ave: "+ atkAve);
        System.Console.WriteLine("Attack Variance: " + atkVar);
        System.Console.WriteLine("Attack Standard Deviation: " + atkStd);
        }

    }

    private static double[] GetRow(double[,] matrix, int rowIndex){
        int columns = matrix.GetLength(1);
        double[] row = new double[columns];
        for (int i = 0; i < columns; i++) { row[i] = matrix[rowIndex, i]; }
        return row;
    }
    
    private static double Variance(int sum, int n, double mean){
        double variance = (double)(sum * sum) / n - (mean * mean);
        return variance;
    }

    private static double StdDev(double variance){
        double stdDev = Math.Sqrt(variance);
        return stdDev;
    }
}
