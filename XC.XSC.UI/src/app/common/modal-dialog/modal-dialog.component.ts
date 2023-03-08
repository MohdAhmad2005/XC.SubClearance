import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-dialog',
  templateUrl: './modal-dialog.component.html',
  styleUrls: ['./modal-dialog.component.css']
})
export class ModalDialogComponent implements OnInit {

  //#region Input/Output Properties
  @Input() ModalHeader: string;
  @Input() ModalBody: any;
  @Input() RequestedPage: string;
  @Input() Data: any;
  //#endregion
  constructor(private _activeModal: NgbActiveModal) { }

  ngOnInit(): void {
  }
  closeModal() {
    this._activeModal.close('Modal Closed');
  }
}
