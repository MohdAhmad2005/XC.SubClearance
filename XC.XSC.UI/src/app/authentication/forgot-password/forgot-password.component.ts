import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertMessageComponent } from 'src/app/common/alert-message/alert-message.component';
import { AlertService } from 'src/app/common/alert-message/alert-service.service';
import { ForgotPasswordDto } from '../../models/user/login-details-dto';
import { RegularExpressionServiceService } from '../../../services/regular-expression-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  //#region Variables Declaration
  public forgotPasswordForm: FormGroup;
  public passwordsMatching: boolean = false;
  public passwordCriteriaMatching: boolean = true;
  public alertMessage: AlertMessageComponent;
  public userId: string;
  //#endregion


  @Input() recoveryEmailId: string;

  get emailid() {
    return this.forgotPasswordForm.controls;
  }

  constructor(
    private _regularExpressionService: RegularExpressionServiceService,
    private _alertService: AlertService,
    private _routeParams: ActivatedRoute,
    private _router: Router,
    private _modalService: NgbModal

  ) {
  }

  ngOnInit(): void {

    this._routeParams.queryParams.subscribe(params => {
      this.userId = params['xsc_uid'];
    });


    this.forgotPasswordForm = new FormGroup({
      PasswordNew: new FormControl("", Validators.required),
      PasswordConfirm: new FormControl("", Validators.required)
    });

    //#region Password matching and criteria logic Implementation
    this.forgotPasswordForm.controls['PasswordNew'].valueChanges.subscribe((val) => {

      if (!this._regularExpressionService.Password.test(val)) {
        this.passwordCriteriaMatching = false;
        this.forgotPasswordForm.controls['PasswordNew'].setErrors({ 'incorrect': true });
      }
      else {
        this.passwordCriteriaMatching = true;
        this.forgotPasswordForm.controls['PasswordNew'].setErrors(null);
      }

      if (this.forgotPasswordForm.controls['PasswordConfirm'].value !== val) {
        this.passwordsMatching = false;
        this.forgotPasswordForm.controls['PasswordConfirm'].setErrors({ 'incorrect': true });
      }
      else {
        this.passwordsMatching = true;
        this.forgotPasswordForm.controls['PasswordConfirm'].setErrors(null);
      }
    })


    this.forgotPasswordForm.controls['PasswordConfirm'].valueChanges.subscribe((val) => {

      if (this.forgotPasswordForm.controls['PasswordNew'].value === val) {
        this.passwordsMatching = true;
        this.forgotPasswordForm.controls['PasswordConfirm'].setErrors(null);
      } else {
        this.passwordsMatching = false;
        this.forgotPasswordForm.controls['PasswordConfirm'].setErrors({ 'incorrect': true });
      }


    })
    //#endregion
  }

  get disableStyle(): string {
    return (this.forgotPasswordForm.valid ? "" : "pointer-events: none;")
  }


  //#region Submit button click event
  public resetForgotPassword = (forgotPasswordForm) => {

    const pwdforgot = { ...forgotPasswordForm };
    if (pwdforgot.PasswordNew == undefined || pwdforgot.PasswordNew == '' || pwdforgot.PasswordNew == null) {
      this._alertService.error('New Password is required.');
      return;
    }
    if (pwdforgot.PasswordConfirm == undefined || pwdforgot.PasswordConfirm == '' || pwdforgot.PasswordConfirm == null) {
      this._alertService.error('Confirm Password is required.');
      return;
    }
    if (pwdforgot.PasswordNew != pwdforgot.PasswordConfirm) {
      this._alertService.error('Passwords did not match');
      return;
    }
    const forgotPassword: ForgotPasswordDto = {
      userId: this.userId,
      newPassword: pwdforgot.PasswordNew,
      confirmPassword: pwdforgot.PasswordConfirm,
    };
    // try {
    //   this._authService.ForgotPassword(forgotPassword).subscribe(response => {
    //     if (response) {
    //       //#region redirect to login popup changes
    //       let ngbModalOptions: NgbModalOptions = {
    //         keyboard: false,
    //       };
    //       const modalRef = this._modalService.open(ModalDialogInfoComponent, ngbModalOptions);

    //       setTimeout(() => {
    //         modalRef.close()
    //       }, 5000);

    //       this._router.navigate(["Login"]);
    //     //#endregion
    //     }
    //   },
    //     error => {
    //       this._alertService.error('Unable to process request. please try again later.');
    //     });
    // } catch (error) {
    //   this._alertService.error(error);
    // }
  }
  //#endregion
}
