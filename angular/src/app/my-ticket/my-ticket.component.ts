import { Component, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryServiceProxy, DepartmentServiceProxy, TicketDto, TicketServiceProxy } from '@shared/service-proxies/service-proxies';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-my-ticket',
  templateUrl: './my-ticket.component.html',
  styleUrl: './my-ticket.component.css'
})
export class MyTicketComponent extends AppComponentBase{
  isAdmin : boolean=false;
  ITAdmin : boolean=false;
  ticket: TicketDto = new TicketDto();
  MaintenanceAdmin : boolean=false;
  tickets: TicketDto[] = [];
  statuses: { name: string, value: number }[] = [
    { name: 'Open', value: 0 },
    { name: 'Rejected', value: 1 },
    { name: 'Pending', value: 2 },
    { name: 'WaitingForInstructions', value: 3 },
    { name: 'WaitingForApproval', value: 4 },
    { name: 'OutSourced', value: 5 }
  ];
  selectedAssignee :any;

  ngOnInit(): void {
    this.checkRole(this.abpSession.userId); 
    this.getAllTickets();
  }
  
  constructor(
    public bsModalRef: BsModalRef,
    private categoryServiceProxy: CategoryServiceProxy,
    private departmentServiceProxy: DepartmentServiceProxy,
    private ticketServiceProxy: TicketServiceProxy,
    private abpSession: AbpSessionService,
    injector: Injector,
    private _router: Router
    ) {
      super(injector);
  }

  updateTicekt(ticket: any) {
    // this.ticket.assignedTo = ticket.assignedTo;
    this.ticket.id = ticket
    this.ticket.status = ticket.status;
    this.ticket.remarks = ticket.remarks;
    this.ticketServiceProxy.createCategory(this.ticket).subscribe((result) => {
      this.notify.success(this.l('Ticket Created Successfully'));
      this._router.navigate(['/app/my-ticket']);
    })
  }
  getAllTickets(){
   
    this.ticketServiceProxy.getTickets().subscribe((result) => {
      this.tickets = result;
      this.tickets.forEach((ticket) => {
        if (Array.isArray(ticket.users)) {
          ticket.users = ticket.users.map(user => user.toString());
          
        } else {
          ticket.users = [];
        }
      });
    });
  }
  
  
  checkRole(userId : number){
    if(userId != null){
      this.ticketServiceProxy.getUserRoleByUserId(userId).subscribe((result) => {
        this.isAdmin = result.roleId == 1 ? true : false;
        this.ITAdmin = result.roleId == 2 ? true : false;
        this.MaintenanceAdmin = result.roleId == 3 ? true : false;
      })
      this.isAdmin = true;
    }
  }
}
