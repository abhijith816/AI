using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_testing
{
    class AdaptiveAStar
    {

        List<State> allStates = new List<State>();
        List<State> expandedStates = new List<State>();
        List<State> traversalTree = new List<State>();
        List<State> pathTree;
        List<State> coveredInPath;
        State actualStartState;
        int totalExpandedStates = 0;
        public void AdaptiveAStarAlgorithm(int[,] maze, Tuple<int, int> startStateTuple, Tuple<int, int> endStateTuple)
        {
            allStates = new List<State>();
            expandedStates = new List<State>();
            traversalTree = new List<State>();
            pathTree = new List<State>();
            coveredInPath = new List<State>();
            //List<State> openStates = new List<State>();

            actualStartState = new State(startStateTuple.Item1, startStateTuple.Item2);
            State startState = actualStartState;
            State goalState = new State(endStateTuple.Item1, endStateTuple.Item2);

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    State currentState = new State(i, j, maze[i, j]);
                    getHValue(currentState, goalState);
                    allStates.Add(currentState);
                }
            }

            startState = allStates.Where(a => a.rowIndex == startStateTuple.Item1 && a.colIndex == startStateTuple.Item2).FirstOrDefault();
            goalState = allStates.Where(a => a.rowIndex == endStateTuple.Item1 && a.colIndex == endStateTuple.Item2).FirstOrDefault();

            int counter = 0;
            bool isTargetReached = false;
            while (!isTargetReached)
            {                
                traversalTree = new List<State>();
                counter += 1;
                startState.search = counter;
                goalState.search = counter;
                goalState.gValue = Int32.MaxValue;

                //List<Tuple<int, State>> inputTuples = new List<Tuple<int,State>>();

                //closedStates = new List<State>();
                BinaryHeap<int, State> openStateHeap = new BinaryHeap<int, State>();
                //Adding Start State to the Open list.                
                openStateHeap.Enqueue(startState.fValue, startState);

                ComputePath(maze, openStateHeap, coveredInPath, allStates, goalState, counter);
                totalExpandedStates += expandedStates.Count;
                if (openStateHeap.Count == 0)
                {
                    Console.WriteLine("Unable to reach the target");
                    break;
                }
                else
                {

                    //For each expanded state we have to update the hvalue with the new HValue.So that next time they will be expanded based on the new cost.
                    foreach(State tempState in expandedStates)
                        getNewHValue(tempState, goalState);


                    //Filling the possible tree pointers
                    State tempcurrentState = goalState;
                    traversalTree.Add(tempcurrentState);
                    while (true)
                    {
                        tempcurrentState = tempcurrentState.previousState;
                        traversalTree.Add(tempcurrentState);
                        if (tempcurrentState.Equals(startState))
                        {
                            //Console.WriteLine("Reached start state from Target");
                            break;
                        }

                    }
                    //Reversing the list so that we can travel from start to target.
                    traversalTree.Reverse();

                    State previousState = new State();
                    previousState = traversalTree.First();
                    foreach (State cState in traversalTree)
                    {
                        if (!cState.Equals(actualStartState) && !cState.Equals(goalState) && cState.isBlocked)
                        {
                            startState = previousState;
                            startState.gValue = 0;
                            cState.cost = Int32.MaxValue;
                            break;
                        }
                        else if (cState.Equals(goalState))
                        {
                            Console.WriteLine("The target is reached");
                            isTargetReached = true;
                        }

                        else
                        {
                            if (!coveredInPath.Any(a => a.Equals(cState)))
                                coveredInPath.Add(cState);
                            //Console.WriteLine("The current State is " + new Tuple<int, int>(cState.rowIndex, cState.colIndex));
                        }

                        previousState = cState;
                    }
                    //Console.WriteLine("The expanded states count is : " + expandedStates.Count);
                    //Console.WriteLine(counter + " Search is over");
                    
                }

            }

            Console.WriteLine("Number of searches are : " + counter);
            Console.WriteLine("Total Number of expanded states are : " + totalExpandedStates);

        }

        private void ComputePath(int[,] maze, BinaryHeap<int, State> openList, List<State> alreadyPrintedList, List<State> stateList, State goalState, int counter)
        {            
            pathTree = new List<State>();
            expandedStates = new List<State>();
            List<State> closedStates = new List<State>();
            while (openList.Count > 0 && goalState.gValue > openList.PeekValue())
            {
                State currentState = openList.RemoveAndReturnValue();//openList.Peek().Item2;                    
                if (!expandedStates.Any(a => a.Equals(currentState)))
                    expandedStates.Add(currentState);
                if (!closedStates.Any(a => a.Equals(currentState)))
                    closedStates.Add(currentState);

                Stack<Tuple<int, int>> neighborStack = new Stack<Tuple<int, int>>();
                neighborStack = FindNeighbors(maze, currentState.rowIndex, currentState.colIndex, neighborStack);
                

                foreach (Tuple<int, int> currentTuple in neighborStack)
                {
                    State currentSuccessorState = allStates.Where(a => a.rowIndex == currentTuple.Item1 && a.colIndex == currentTuple.Item2).FirstOrDefault();
                    
                    //Condition to check the actual start states neighbors are blocked or not. If they are blocked then we will directly skip them. Dont know why we are doing this only for the first start state.
                    if(currentState.Equals(actualStartState) && currentSuccessorState.isBlocked)
                    {
                        currentSuccessorState.gValue = Int32.MaxValue;
                        continue;
                    }


                    //if (alreadyPrintedList.Any(a => a.Equals(currentSuccessorState)))
                    //    continue;

                    //if (expandedStates.Any(a => a.Equals(currentSuccessorState)) || (currentSuccessorState.gValue == Int32.MaxValue && !currentSuccessorState.Equals(goalState)))
                    //    continue;


                    if (closedStates.Any(a => a.Equals(currentSuccessorState)) || (currentSuccessorState.gValue == Int32.MaxValue && !currentSuccessorState.Equals(goalState)))
                        continue;

                    if (currentSuccessorState.search < counter)
                    {
                        currentSuccessorState.gValue = Int32.MaxValue;
                        currentSuccessorState.search = counter;
                    }
                    if (currentSuccessorState.gValue > currentState.gValue + currentSuccessorState.cost)
                    {
                        currentSuccessorState.gValue = currentState.gValue + currentSuccessorState.cost;
                        currentSuccessorState.previousState = currentState;
                        if (!pathTree.Contains(currentState))
                            pathTree.Add(currentState);

                        if (currentSuccessorState == goalState)
                            pathTree.Add(goalState);

                        if (openList.Contains(new Tuple<int, State>(currentSuccessorState.fValue, currentSuccessorState)))
                            openList.Remove(new Tuple<int, State>(currentSuccessorState.fValue, currentSuccessorState));

                        openList.Add(new Tuple<int, State>(currentSuccessorState.fValue, currentSuccessorState));
                    }
                }

            }
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
            if (rowIndex - 1 >= 0)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex - 1, colIndex));
            if (colIndex + 1 < numberOfColumns)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex, colIndex + 1));
            if (colIndex - 1 >= 0)
                stackAfterNeighbors.Push(new Tuple<int, int>(rowIndex, colIndex - 1));

            return stackAfterNeighbors;

        }

        public void getHValue(State currentState, State targetState)
        {
            int manhattanDistance = 0;
            manhattanDistance = Math.Abs(currentState.rowIndex - targetState.rowIndex) + Math.Abs(currentState.colIndex - targetState.colIndex);
            currentState.hValue = manhattanDistance;
            //return manhattanDistance;
        }

        public void getNewHValue(State currentState, State targetState)
        {
            int newHValue = 0;
            newHValue = targetState.gValue - currentState.gValue;
            currentState.hValue = newHValue;
        }

    }
}
