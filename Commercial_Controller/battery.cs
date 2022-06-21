using System;
using System.Collections.Generic;

namespace Commercial_Controller
{

    public class Battery
    {
        private List<FloorRequestButton> floorButtonsList = new List<FloorRequestButton>();
        private List<Column> columnsList = new List<Column>();
        private int columnID = 0;
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
            //this.findBestColumn(40);

        }


        public void CreateBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int>();

            floor = -1;

            for (int i = 0; i < _amountOfBasements; i++)
            {
                columnID = i + 1;

                servedFloors.Add(floor);
                floor--;

                Column column = new Column(columnID, _amountOfElevatorPerColumn, servedFloors, true);
                columnsList.Add(column);
            }
            //columnsList.ToArray();
            Console.WriteLine("test 1" + columnsList);
        }

        private void CreateColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfElevatorPerColumn)
        {
            double moean = _amountOfFloors / _amountOfColumns;
            double amountOfFloorsPerColumn = Math.Ceiling(moean);
            floor = 1;


            for (int i = 0; i < _amountOfColumns; i++)
            {
                List<int> servedFloors = new List<int>();

                for (int j = 0; j < amountOfFloorsPerColumn; j++)
                {

                    if (floor <= _amountOfFloors)
                    {

                        servedFloors.Add(floor);
                        floor++;
                    }

                }

                Column column = new Column(columnID, _amountOfElevatorPerColumn, servedFloors, false);
                columnsList.Add(column);
                columnID++;

            }
            Console.WriteLine("test 2" + columnsList);
        }

        private void CreateFloorRequestButtons(int _amountOfFloors)
        {

            int buttonFloor = 1;
            int floorRequestButtonID = 0;

            for (int i = 0; i < _amountOfFloors; i++)
            {
                floorRequestButtonID = i + 1;

                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "up");
                floorButtonsList.Add(floorRequestButton);

                buttonFloor++;

            }
            Console.WriteLine("test 3" + floorButtonsList);
        }

        private void CreateBasementFloorRequestButtons(int _amountOfBasements)
        {

            int buttonFloor = -1;
            int floorRequestButtonID = 0;

            for (int i = 0; i < _amountOfBasements; i++)
            {

                floorRequestButtonID = i + 1;
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "down");

                floorButtonsList.Add(floorRequestButton);

                buttonFloor--;
            }
            Console.WriteLine("test 4" + floorButtonsList);
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
            Console.WriteLine("test 5" + bestColumn);
            return bestColumn;

        }
        //Simulate when a user press a button at the lobby
        /* public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
         {

         }*/
    }
}

