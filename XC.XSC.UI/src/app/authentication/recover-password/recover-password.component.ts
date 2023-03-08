import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertMessageComponent } from 'src/app/common/alert-message/alert-message.component';
import { AlertService } from 'src/app/common/alert-message/alert-service.service';
import { EmailServiceService } from '../../../services/email-service.service';
import { RecoverEmailDto } from '../../models/user/login-details-dto';

@Component({
  selector: 'app-recover-password',
  templateUrl: './recover-password.component.html',
  styleUrls: ['./recover-password.component.css']
})
export class RecoverPasswordComponent implements OnInit {

  //#region Variable declarations
  public recoverPasswordForm: FormGroup;
  public alertMessage: AlertMessageComponent;
  public IsMailSendSuccessfully: boolean = false;
  //#endregion

  //#region Input/output properties
  @Output() onBackToSignInClick: EventEmitter<{ source: string, target: string, email: string, otp: string }> = new EventEmitter<{ source: string, target: string, email: string, otp: string }>();
  @Input() recoveryEmailId: string;
  //#endregion

  get emailid() {
    return this.recoverPasswordForm.controls;
  }

  constructor(
    private _alertService: AlertService,
    private _emailServiceService: EmailServiceService,
    public _router: Router,
  ) { }

  ngOnInit(): void {
    this.recoverPasswordForm = new FormGroup({
      recoveryEmailId: new FormControl("", [Validators.required, Validators.email])
    });
  }

  //#region Recover password button click event
  public sendPasswordResetEmail = (recoveryFormValue) => {

    const pwdRevovery = { ...recoveryFormValue };

    if (pwdRevovery.recoveryEmailId == undefined || pwdRevovery.recoveryEmailId == '' || pwdRevovery.recoveryEmailId == null) {
      this._alertService.error('Recovery Email is required.');
      return;
    }

    const recoverEmail: RecoverEmailDto = {
      Email: pwdRevovery.recoveryEmailId
    };

    try {
      this._emailServiceService.SendResetPasswordLink(recoverEmail).subscribe(response => {
        this.IsMailSendSuccessfully = true;
      },
        error => {
          if (!!error.error && !!error.error.message && error.error.message == "Invalid Email") {
            this._alertService.error('Please enter valid email to reset your password');
          } else {
            this._alertService.error('Unable to process your request. please try again later.');
          }
        })
    }
    catch (error) {
      this._alertService.error(error);
    }
  }
  //#endregion

  //#region Other Methods

  public backToSignIn = (sourceArea) => {
    this.onBackToSignInClick.emit({ source: sourceArea, target: 'LOGIN', email: this.recoveryEmailId, otp: '' });
  }

  public validateControl = (controlName: string) => {
    return this.recoverPasswordForm.controls[controlName].status == 'INVALID' && this.recoverPasswordForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.recoverPasswordForm.controls[controlName].hasError(errorName)
  }
  //#endregion

}
