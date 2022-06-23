using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commercial_Controller
{
    public class bestElevatorInformations
    {
        public Elevator BestElevator = null;
        public int BestScore = 0;
        public int  ReferenceGap = 0;

        public bestElevatorInformations(Elevator bestElevator, int bestScore, int referenceGap)
        {
            BestElevator = bestElevator;
            BestScore = bestScore;
            ReferenceGap = referenceGap;
        }
    }
}
