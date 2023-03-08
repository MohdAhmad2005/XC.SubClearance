import { Component, OnInit } from '@angular/core';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-organisation-details',
  templateUrl: './organisation-details.component.html',
  styleUrls: ['./organisation-details.component.css']
})
export class OrganisationDetailsComponent implements OnInit {

  orgDetailForm: FormGroup;
  constructor(private _rootFormGroup: FormGroupDirective) { }

  ngOnInit(): void {
    this.orgDetailForm = this._rootFormGroup.control;
  }

}
