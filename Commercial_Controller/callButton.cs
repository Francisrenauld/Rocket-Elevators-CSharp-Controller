namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {

        public int ID;
        public int floor;
        public string direction;
        public CallButton(int callButtonID, int _floor, string _direction)
        {
            ID = callButtonID;
            floor = _floor;
            direction = _direction;
        }
    }
}