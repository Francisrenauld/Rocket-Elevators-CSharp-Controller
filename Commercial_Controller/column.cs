using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {

        public int ID;
        public string status;
        public int amountOfFLoors;
        public int amountOfElevators;
        public List<int> servedFloorsList = new List<int>();



        public Column(int _ID, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {

            ID = _ID;
            servedFloorsList = _servedFloors;
          /*  status = "active";
            amountOfElevators = _amountOfElevators;
            Elevator[] columnsList = { };*/

        }

        //Simulate when a user press a button on a floor to go back to the first floor
       /* public Elevator requestElevator(int userPosition, string direction)
        {
            
        }*/

    }
}