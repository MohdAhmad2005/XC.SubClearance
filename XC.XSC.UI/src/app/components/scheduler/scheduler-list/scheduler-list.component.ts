import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GenericModalComponent } from 'src/app/common/generic-modal/generic-modal.component';

@Component({
  selector: 'app-scheduler-list',
  templateUrl: './scheduler-list.component.html',
  styleUrls: ['./scheduler-list.component.css']
})
export class SchedulerListComponent implements OnInit {

  constructor(private _modalService: NgbModal
    ,private _activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }
  public openPopUp(): void {
    const modalRef = this._modalService.open(GenericModalComponent);
    modalRef.componentInstance.header = "Add Schedular";
    modalRef.componentInstance.param = "add_Schedular"   
  }

}
