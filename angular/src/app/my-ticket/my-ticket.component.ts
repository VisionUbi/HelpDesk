import { Component, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { CategoryServiceProxy, DepartmentServiceProxy, TicketDto, TicketServiceProxy } from '@shared/service-proxies/service-proxies';
import { AbpSessionService } from 'abp-ng2-module';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-my-ticket',
  templateUrl: './my-ticket.component.html',
  styleUrls: ['./my-ticket.component.css']
})
export class MyTicketComponent extends AppComponentBase {
  isAdmin: boolean = false;
  ITAdmin: boolean = false;
  MaintenanceAdmin: boolean = false;
  tickets: TicketDto[] = [];
  statuses: { name: string, value: number }[] = [
    { name: 'Open', value: 0 },
    { name: 'Rejected', value: 1 },
    { name: 'Pending', value: 2 },
    { name: 'Resolved', value: 3 },
    { name: 'WaitingForInstructions', value: 4 },
    { name: 'WaitingForApproval', value: 5 },
    { name: 'OutSourced', value: 6 }
  ];
  selectedAssignee: any;

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

  ngOnInit(): void {
    this.checkRole(this.abpSession.userId); 
    this.getAllTickets();
  }

  updateTicket(ticket: TicketDto) {
    const updatedTicket = new TicketDto();
    updatedTicket.id = ticket.id;
    updatedTicket.assignedTo = ticket.assignedTo;
    updatedTicket.status = ticket.status;
    updatedTicket.remarks = ticket.remarks;

    this.ticketServiceProxy.createCategory(updatedTicket).subscribe((result) => {
      this.notify.success(this.l('Ticket Updated Successfully'));
      this.getAllTickets();
    });
  }

  getAllTickets() {
    this.ticketServiceProxy.getTickets().subscribe((result) => {
      this.tickets = result;
      // this.tickets.forEach((ticket) => {
      //   if (!Array.isArray(ticket.users)) {
      //     ticket.users = [];
      //   }
      // });
    });
  }

  checkRole(userId: number) {
    if (userId != null) {
      this.ticketServiceProxy.getUserRoleByUserId(userId).subscribe((result) => {
        this.isAdmin = result.roleId === 1;
        this.ITAdmin = result.roleId === 2;
        this.MaintenanceAdmin = result.roleId === 3;
      });
    }
  }
}
