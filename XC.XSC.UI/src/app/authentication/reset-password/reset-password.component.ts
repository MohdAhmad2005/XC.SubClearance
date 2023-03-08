import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
//import { AuthService } from '../../../services/auth.service';
import { RegularExpressionServiceService } from '../../../services/regular-expression-service.service';
//import { TokenStorageService } from '../../../services/token-storage.service';
import { AlertService } from '../../common/alert-message/alert-service.service';
import { ModalDialogInfoComponent } from '../../common/modal-dialog-info/modal-dialog-info.component';
import { ResetPasswordDto } from '../../models/user/login-details-dto';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  //#region Variables Declaration
  public resetPasswordForm: FormGroup;
  public passwordsMatching: boolean = false;
  public passwordCriteriaMatching: boolean = true;

  //#endregion

  constructor(
    private _alertService: AlertService,
    private _regularExpressionService: RegularExpressionServiceService,
    //private _authService: AuthService,
    //private _tokenStorage: TokenStorageService,
    private _router: Router,
    private _modalService: NgbModal
  ) { }

  //#region Life Cycle Events
  ngOnInit(): void {
    //#region Form Creation
    this.resetPasswordForm = new FormGroup({
      oldPassword: new FormControl("", [Validators.required]),
      newPassword: new FormControl("", [Validators.required]),
      confirmNewPassword: new FormControl("", [Validators.required])
    });
    //#endregion

    //#region Password matching and criteria logic Implementation
    this.resetPasswordForm.controls['newPassword'].valueChanges.subscribe((val) => {

      if (!this._regularExpressionService.Password.test(val)) {
        this.passwordCriteriaMatching = false;
        this.resetPasswordForm.controls['newPassword'].setErrors({ 'incorrect': true });
      }
      else {
        this.passwordCriteriaMatching = true;
        this.resetPasswordForm.controls['newPassword'].setErrors(null);
      }

      if (this.resetPasswordForm.controls['confirmNewPassword'].value !== val) {
        this.passwordsMatching = false;
        this.resetPasswordForm.controls['confirmNewPassword'].setErrors({ 'incorrect': true });
      }
      else {
        this.passwordsMatching = true;
        this.resetPasswordForm.controls['confirmNewPassword'].setErrors(null);
      }
    })


    this.resetPasswordForm.controls['confirmNewPassword'].valueChanges.subscribe((val) => {

      if (this.resetPasswordForm.controls['newPassword'].value === val) {
        this.passwordsMatching = true;
        this.resetPasswordForm.controls['confirmNewPassword'].setErrors(null);
      } else {
        this.passwordsMatching = false;
        this.resetPasswordForm.controls['confirmNewPassword'].setErrors({ 'incorrect': true });
      }


    })
    //#endregion
  }

  //#endregion

  //#region Supplimentry Methods
  //#region disable Submit button method
  get disableStyle(): string {
    return (this.resetPasswordForm.valid ? "" : "pointer-events: none;")
  }
  //#endregion

  //#region Submit button click event
  public resetPassword = (resetPasswordFormValue: any) => {
    const resetPassword = { ...resetPasswordFormValue };

    // const userInfo = this._tokenStorage.getCustomerUserInfo();
    // const resetPasswordDetail: ResetPasswordDto = {
    //   userName: userInfo.email,
    //   oldPassword: resetPassword.oldPassword,
    //   newPassword: resetPassword.newPassword,
    //   confirmPassword: resetPassword.confirmNewPassword
      
    // };
/*
    try {
      this._authService.resetPassword(resetPasswordDetail).subscribe(response => {
        if (response) {
          //#region redirect to login popup changes
          let ngbModalOptions: NgbModalOptions = {
            keyboard: false,
          };
          const modalRef = this._modalService.open(ModalDialogInfoComponent, ngbModalOptions);

          setTimeout(() => {
            modalRef.close()
          }, 5000);

          this._router.navigate(["Login"]);
        //#endregion
        }
        else {
          this._alertService.error('Invalid user credentials');
        }
      },
        error => {
            this._alertService.error('Unable to process your request. please try again later.')
        });
    } catch (error) {
      this._alertService.error(error);
    }
    */
  }
  //#endregion

  //#region Show password on eye icon click
  ShowPassword(event: any) {
    var elementId = event.target.id;
    if (elementId == 'pwd-eye-oldPassword') {
      var element = <HTMLInputElement>document.getElementById("oldpassword");
      this.setElementType(element);
    }
    else if (elementId == 'pwd-eye-newPassword') {
      var element = <HTMLInputElement>document.getElementById("newpassword");
      this.setElementType(element);
    }
    else if (elementId == 'pwd-eye-newConfirmPassword') {
      var element = <HTMLInputElement>document.getElementById("newconfirmpassword");
      this.setElementType(element);
    }
  }

  //Below method is used to show and hide text on the textbox.
  setElementType(element: any) {
    if (!!element.value) {
      if (element.type === "password") {
        element.type = "text";
      } else {
        element.type = "password";
      }
    }
  }
  //#endregion

  //#endregion
}
