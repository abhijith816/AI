using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_testing
{
    class DepthFirstSearch
    {        
        public int[,] FillArrayUsingDepthFirstSearch(int[,] inputArray)
        {
            Random r = new Random();
            Stack<Tuple<int, int>> tempStack = new Stack<Tuple<int, int>>();
            int numberOfRows = inputArray.GetLength(0);
            int numberOfColumns = inputArray.GetLength(1);

            //Pick a random i and j and use them as starting indexes.
            int i = 0, j = 0;

            tempStack.Push(new Tuple<int, int>(i, j));

            while(tempStack.Count > 0)
            {
                Tuple<int, int> currentTuple = tempStack.Pop();
                int currentRowIndex = currentTuple.Item1;
                int currentColIndex = currentTuple.Item2;

                if (inputArray[currentRowIndex, currentColIndex] != -1)
                    continue;
                else
                {
                    int randomNumber = r.Next(1, 100);
                    if(randomNumber < 31)
                       inputArray[currentRowIndex, currentColIndex] = 0;
                    else
                        inputArray[currentRowIndex, currentColIndex] = 1;
                }

                tempStack = FindNeighbors(inputArray,currentRowIndex,currentColIndex,tempStack);

            }

            return inputArray;

        }

        public Stack<Tuple<int, int>> FindNeighbors(int[,] input, int rowIndex, int colIndex, Stack<Tuple<int, int>> tempStack)
        {
            Stack<Tuple<int, int>> stackAfterNeighbors = tempStack;
            //Stack<Tuple<int, int>> stackAfterNeighbors = tempStack;
            int numberOfRows = input.GetLength(0);
            int numberOfColumns = input.GetLength(1);

            //If the rowIndex is the last row then there will be no neighbor in next row.
            if (rowIndex + 1 < numberOfRows)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex + 1, colIndex));
            if(rowIndex - 1 >= 0)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex - 1, colIndex));
            if(colIndex + 1 < numberOfColumns)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex, colIndex+1));
            if(colIndex - 1 >=0)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex, colIndex -1));

            return stackAfterNeighbors;

        }


    }
}
