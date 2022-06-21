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
        private int ID;
        private string status;

        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {

            ID = _ID;
            status = "online";
            
            

            if (_amountOfBasements > 0)
            {
            //    this.createBasementFloorRequestButtons(_amountOfBasements);
                this.createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                _amountOfColumns--;
            }

          //  this.createFloorRequestButtons(_amountOfFloors);
              this.createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);


        }


        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int>();   

            floor = -1;

            for(int i = 0; i < _amountOfBasements; i++)
            {
                columnID = i + 1;

                servedFloors.Add(floor);
                floor--;

                Column column = new Column(columnID, _amountOfElevatorPerColumn, servedFloors, true);
                columnsList.Add(column);                
            }
            //columnsList.ToArray();
            Console.WriteLine(columnsList);
        }

        private void createColumns(int _amountOfColumns,int _amountOfFloors,int _amountOfElevatorPerColumn)
        {
            double moean = _amountOfFloors / _amountOfColumns;
            double amountOfFloorsPerColumn = Math.Ceiling(moean);
            floor = 1;
            

            for (int i = 0; i < _amountOfColumns; i++)
            {
                List<int> servedFloors = new List<int>();

                for (int j = 0; j < amountOfFloorsPerColumn; j++)
                {

                    if(floor <= _amountOfFloors)
                    {

                        servedFloors.Add(floor);
                        floor++;
                    }

                }

                Column column = new Column(columnID,_amountOfElevatorPerColumn, servedFloors, false);
                columnsList.Add(column);
                columnID++;

            }

        }

        private void createFloorRequestButtons(int _amountOfFloors)
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
        }

        private void createBasementFloorRequestButtons(int _amountOfBasements)
        {

            int buttonFloor = -1;







        }

      /*  public Column findBestColumn(int _requestedFloor)
        {
            
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            
        }*/
    }
}

