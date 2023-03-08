import { Component, OnInit } from '@angular/core';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {

  accountDetailForm: FormGroup
  
  constructor(private _rootFormGroup: FormGroupDirective) { }

  ngOnInit(): void {
    this.accountDetailForm = this._rootFormGroup.control;
  }

}
