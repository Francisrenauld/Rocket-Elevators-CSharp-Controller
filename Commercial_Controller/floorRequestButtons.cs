namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class FloorRequestButton
    {

        public int ID;
        public int floor;
        public string status;
        public string direction;


        public FloorRequestButton(int _id, int _floor, string _direction)
        {
            ID = _id;
            floor = _floor;
            direction = _direction;
        }
    }
}