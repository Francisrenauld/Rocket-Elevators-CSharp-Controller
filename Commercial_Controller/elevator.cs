using System.Collections.Generic;
using System.Threading;

namespace Commercial_Controller
{
    public class Elevator
    {
        public List<int> completedRequestsList = new List<int>();
        public int screenDisplay;
        public string ID;
        public string status;
        public int amountOfFloors;
        public string direction;
        public int currentFloor;
        public bool overweight;
        public List<int> floorRequestsList = new List<int>();
        public Elevator(string _elevatorID, string _status, int _amountOfFloors, int _currentFloor)
        {

            ID = _elevatorID;
            status = _status;
            amountOfFloors = _amountOfFloors;
            Door door = new Door(_elevatorID, "closed");
            direction = null;
            currentFloor = _currentFloor;
            overweight = false;
        }
        public void move()
        {
            //make the elvator moove
            int destination;

            while (floorRequestsList.Count > 0)
            {

                destination = floorRequestsList[0];
                status = "moving";

                if (currentFloor < destination)
                {

                    direction = "up";
                    this.sortFloorList();
                    destination = floorRequestsList[0];

                    while (currentFloor < destination)
                    {
                        currentFloor++;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                else if (currentFloor > destination)
                {

                    direction = "down";
                    this.sortFloorList();
                    destination = floorRequestsList[0];

                    while (currentFloor > destination)
                    {
                        currentFloor--;
                        this.screenDisplay = this.currentFloor;
                    }
                }
                status = "stopped";
                operateDoors();
                completedRequestsList.Add(destination);
                floorRequestsList.RemoveAt(0);
            }

            status = "idle";

        }

        public void sortFloorList()
        {
            //sort the list with where the direction is
            if (direction == "up")
            {
                floorRequestsList.Sort();
            }
            else
            {
                floorRequestsList.Sort();
                floorRequestsList.Reverse();
            }
        }

        public void operateDoors()
        {
            //stop the code for 5 sec

            Thread.Sleep(800);

        }

        public void addNewRequest(int requestedFloor)
        {
            //add the request into the list
            if (floorRequestsList.Contains(requestedFloor) == false)
            {
                floorRequestsList.Add(requestedFloor);
            }
            if (currentFloor < requestedFloor)
            {
                direction = "up";
            }
            if (currentFloor > requestedFloor)
            {
                direction = "down";
            }
        }
    }
}