import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-dialog-info',
  templateUrl: './modal-dialog-info.component.html',
  styleUrls: ['./modal-dialog-info.component.css']
})
export class ModalDialogInfoComponent implements OnInit {

  constructor(private _activeModal: NgbActiveModal, private _router: Router) { }

  ngOnInit(): void {
  }

  BackToLogInPage() {
    this._activeModal.close('Modal Closed');
    this._router.navigate(["Login"]);
  }

}
