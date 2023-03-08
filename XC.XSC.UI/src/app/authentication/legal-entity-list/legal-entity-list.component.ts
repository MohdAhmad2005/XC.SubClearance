import { Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-legal-entity-list',
  templateUrl: './legal-entity-list.component.html',
  styleUrls: ['./legal-entity-list.component.css']
})
export class LegalEntityListComponent implements OnInit {


  //#region Input/Output Properties
  @Input() LegalEntityList: any;
  //#endregion

  //#region Variable Declaration
  private returnUrl: string;
  public legalEntityList: any;
  public selectedLegalEntity: string;
  //#endregion
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private _activeModal: NgbActiveModal) { }

  //#region Life Cycle events
  ngOnInit(): void {
    if (!!this.LegalEntityList) {
      this.legalEntityList = this.LegalEntityList.legalEntityList;
    }

    this.returnUrl = this._activatedRoute.snapshot.queryParams['returnUrl'] || '/';
  }
  //#endregion

  //#region Dropdown change event
  LegalEntityChanged(event) {
    this.selectedLegalEntity = event.target.value;
  }
  //#endregion

  //#region Submit button click event
  SubmitLegalEntityList() {

    if (!!this.selectedLegalEntity) {
      this._activeModal.close('Modal Closed');

      if (this.returnUrl != '' && this.returnUrl != '/' && this.returnUrl != undefined && this.returnUrl != null)
        this._router.navigate([this.returnUrl]);
      else
        this._router.navigate(["Dashboard"]);
    }

  }
  //#endregion

}
