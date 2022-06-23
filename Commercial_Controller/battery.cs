using System;
using System.Collections.Generic;

namespace Commercial_Controller
{

    public class Battery
    {
        public List<FloorRequestButton> floorButtonsList = new List<FloorRequestButton>();
        public List<Column> columnsList = new List<Column>();
        private int columnID = 1;
        private int floor = 1;
        public int ID;
        public string status;

        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {

            this.ID = _ID;
            status = "online";



            if (_amountOfBasements > 0)
            {
                this.CreateBasementFloorRequestButtons(_amountOfBasements);
                this.CreateBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                _amountOfColumns--;
            }

            this.CreateFloorRequestButtons(_amountOfFloors);
            this.CreateColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);

        }


        public void CreateBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int>();

            floor = -1;
            servedFloors.Add(1);
            for (int i = 0; i < _amountOfBasements; i++)
            {
               
                servedFloors.Add(floor);
                floor--;

            }
            Column column = new Column(columnID.ToString(), _amountOfBasements, _amountOfElevatorPerColumn, servedFloors, true);
            columnsList.Add(column);
            columnID++;
            
        }

        private void CreateColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn)
        {
            double moean = _amountOfFloors / _amountOfColumns;
            double amountOfFloorsPerColumn = Math.Ceiling(moean);
            floor = 1;
            int floorAssigned = 1;

            for (int i = 0; i < _amountOfColumns; i++)
            {
                List<int> servedFloors = new List<int>();
                servedFloors.Add(1);
                for (int j = 0; j < _amountOfFloors; j++)
                {

                    if (floor <= amountOfFloorsPerColumn)
                    {

                        servedFloors.Add(floorAssigned);
                        floor++;

                    }
                    else
                    {
                        break;
                    }
                    floorAssigned++;
                }
                Column column = new Column(columnID.ToString(), _amountOfFloors, _amountOfElevatorPerColumn, servedFloors, false);
                columnsList.Add(column);
                columnID++;
                floor = 1;


            }
        }

        private void CreateFloorRequestButtons(int _amountOfFloors)
        {

            //int buttonFloor = 1;
            int floorRequestButtonID = 0;

            for (int i = 0; i < _amountOfFloors; i++)
            {
                floorRequestButtonID = i + 1;

                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, floorRequestButtonID, "up");
                floorButtonsList.Add(floorRequestButton);

                //buttonFloor++;

            }

        }

        private void CreateBasementFloorRequestButtons(int _amountOfBasements)
        {

            int buttonFloor = -1;
            int floorRequestButtonID = 0;

            for (int i = 0; i < _amountOfBasements; i++)
            {

                floorRequestButtonID = i + 1;
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, buttonFloor, "down");

                floorButtonsList.Add(floorRequestButton);

                buttonFloor--;
            }

        }

        public Column findBestColumn(int _requestedFloor)
        {

            Column bestColumn = null;

            foreach (Column column in this.columnsList)
            {

                if (column.servedFloorsList.Contains(_requestedFloor))
                {

                    bestColumn = column;

                }

            }

            return bestColumn;

        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = this.findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, _direction);
            elevator.addNewRequest(1);
            elevator.move();

            elevator.addNewRequest(_requestedFloor);
            elevator.move();

            return (column, elevator);
        }
    }
}

