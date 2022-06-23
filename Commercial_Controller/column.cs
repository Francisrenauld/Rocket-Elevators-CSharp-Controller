using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {

        public string ID;
        public string status;
        public int amountOfFLoors;
        public int amountOfElevators;
        public int buttonFloor = 0;
        public int callButtonID = 1;
        public int elevatorID = 1;
        public List<int> servedFloorsList = new List<int>();
        public List<Elevator> elevatorsList = new List<Elevator>();
        public List<CallButton> callButtonsList = new List<CallButton>();

        public Column(string _ID, int _amountOfFloors, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {

            ID = _ID;
            servedFloorsList = _servedFloors;
            status = "active";
            amountOfElevators = _amountOfElevators;


            this.CreateElevators(_amountOfFloors, _amountOfElevators);
            this.CreateCallButtons(_amountOfFloors, _isBasement);

        }

        public void CreateCallButtons(int _amountOfFloors, bool _isBasement)
        {
            //create buton for floor
            if (_isBasement == true)
            {
                buttonFloor = -1;

                for (int i = 0; i < _amountOfFloors; i++)
                {

                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "down");
                    callButtonsList.Add(callButton);
                    buttonFloor--;
                    callButtonID++;
                }
            }
            else
            {

                buttonFloor = 1;

                for (int i = 0; i < _amountOfFloors; i++)
                {

                    CallButton callButton = new CallButton(callButtonID, buttonFloor, "up");
                    callButtonsList.Add(callButton);
                    buttonFloor++;
                    callButtonID++;
                }
            }
        }

        public void CreateElevators(int _amountOfFloors, int _amountOfElevators)
        {
            //create elevator
            for (int i = 0; i < _amountOfElevators; i++)
            {

                Elevator elevator = new Elevator(elevatorID.ToString(), "idle", _amountOfFloors, 1);
                elevatorsList.Add(elevator);
                elevatorID++;

            }
        }

        public Elevator requestElevator(int userPosition, string direction)
        {
            //call elevator to user
            Elevator elevator = null;
            elevator = this.findElevator(userPosition, direction);
            elevator.addNewRequest(userPosition);
            elevator.move();
            elevator.addNewRequest(1);
            elevator.move();

            return elevator;
        }

        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            //find the best elevato for destination
            Elevator bestElevator = elevatorsList[0];
            int bestScore = 5;
            int referenceGap = 10000000;

            bestElevatorInformations bestElevatorInfo = null;
            

            if (requestedFloor < 0)
            {
                foreach (Elevator elevator in this.elevatorsList)
                {

                    if (1 == elevator.currentFloor && elevator.status == "stopped")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(1, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (1 == elevator.currentFloor && elevator.status == "idle")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(2, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (1 > elevator.currentFloor && elevator.direction == "up")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(2, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (1 < elevator.currentFloor && elevator.direction == "down")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(3, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (elevator.status == "idle")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(4, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(5, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }

                    bestElevator = bestElevatorInfo.BestElevator;
                    bestScore = bestElevatorInfo.BestScore;
                    referenceGap = bestElevatorInfo.ReferenceGap;
                }
            }
            else
            {

                foreach (Elevator elevator in this.elevatorsList)
                {
                    if (requestedFloor == elevator.currentFloor && elevator.status == "stopped" && requestedDirection == elevator.direction)
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(1, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (requestedFloor > elevator.currentFloor && elevator.status == "stopped" && elevator.direction == "up" && requestedDirection == "up")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(2, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(2, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (elevator.status == "idle")
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(4, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }
                    else if (elevator.direction != requestedDirection)
                    {
                        bestElevatorInfo = this.checkIfElevatorIsBetter(5, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    }

                    bestElevator = bestElevatorInfo.BestElevator;
                    bestScore = bestElevatorInfo.BestScore;
                    referenceGap = bestElevatorInfo.ReferenceGap;
                }
            }
            return bestElevator;
        }
        public bestElevatorInformations checkIfElevatorIsBetter(int scoreToCheck, Elevator newElevator, int bestScore, int referenceGap, Elevator bestElevator, int floor)
        {
            // check the best elevator
            int gap = 0;
            if (scoreToCheck < bestScore )
            {

                bestScore = scoreToCheck;
                bestElevator = newElevator;
                referenceGap = Math.Abs(newElevator.currentFloor - floor);
            }
            else if (bestScore == scoreToCheck)
            {
                gap = Math.Abs(newElevator.currentFloor - floor);
                if (referenceGap > gap)
                {
                    bestElevator = newElevator;
                    referenceGap = gap;
                }
            }
            return new bestElevatorInformations(bestElevator, bestScore, referenceGap);
        }
    }
}
