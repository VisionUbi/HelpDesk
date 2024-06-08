using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Tickets
{
    public enum TicketStatus
    {
        Open = 0, Rejected = 1, Pending = 2 , Resolved = 3,
        WaitingForInstrunctions = 4, WaitingForApproval = 5, OutSourced = 6
    }
}
