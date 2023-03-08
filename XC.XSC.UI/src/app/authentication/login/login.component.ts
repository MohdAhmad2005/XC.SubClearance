import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertMessageComponent } from 'src/app/common/alert-message/alert-message.component';
import { AlertService } from 'src/app/common/alert-message/alert-service.service';
import { LoginDetailsDto } from 'src/app/models/user/login-details-dto';
import { AesEncryption } from '../../../services/aes-encryption-service.service';
import { RecoverPasswordComponent } from '../recover-password/recover-password.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  public recoverPasswordComponent: RecoverPasswordComponent;
  public alertMessage: AlertMessageComponent;
  public isChecked: boolean = false;
  public isLoginRequest: boolean = false;

  private activeAreaName: string = 'LOGIN';
  private returnUrl: string;
  public legalEntities: any = [];

  get emailid() {
    return this.loginForm.controls;
  }

  constructor(
    public _router: Router,
    private _route: ActivatedRoute,
    private _alertService: AlertService,
    private _cryptoService: AesEncryption,
    private _modalService: NgbModal
  ) {
  }

  ngOnInit(): void {

    this.loginForm = new FormGroup({
      userEmail: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required])
    });
    // let savedUserEmail = this._cookieService.get('xsc_u');
    // if (!!savedUserEmail) {
    //   let xsc_u = this._cryptoService.decrypt(savedUserEmail);
    //   if (!!xsc_u) {
    //     this.loginForm.get('userEmail').setValue(xsc_u);
    //     this.isChecked = true;
    //   }
    // }
    this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  rememberMe(event: any) {
    if (event.target.checked) {
      this.isChecked = true;
      let userEmail = this.loginForm.get('userEmail').value;
      if (!!userEmail) {
        let xsc_u = this._cryptoService.encrypt(userEmail);

      }
    }
    else {
      this.isChecked = false;

    }
  }

  get disableStyle(): string {
    return (this.loginForm.valid ? "" : "pointer-events: none;")
  }

  public setActivateArea = (areaName: any): string => {
    return this.activeAreaName = areaName;
  }

  public getActivateArea = (areaName: any): boolean => {
    return this.activeAreaName == areaName;
  }

  public onChildNavigation = (event): void => {
    this.loginForm.value.userEmail = event.email;

    this.setActivateArea(event.target);
  }

  public loginUser = (loginFormValue: any) => {

    const login = { ...loginFormValue };

    if (login.userEmail == undefined || login.userEmail == '' || login.userEmail == null) {
      this._alertService.error('UserName is required.');
      return;
    }

    if (login.password == undefined || login.password == '' || login.password == null) {
      this._alertService.error('Password is required.');
      return;
    }


    const loginDetail: LoginDetailsDto = {
      userName: login.userEmail,
      password: login.password
    };

    try {
      this._alertService.clear();
      this.isLoginRequest = true;
      /*
      this.authService.loginUser(loginDetail).subscribe(response => {

        this.isLoginRequest = false;

        if (response.isAuthSuccessful) {
          this._tokenStorage.saveToken(response.message.accessToken);
          this._tokenStorage.setRefreshToken(response.message.refreshToken);

          const tokenPayload = this._jwtService.GetTokenDecoded();

          const userInfo = <UserInfoDto>{
            email: tokenPayload['email'],
            firstName: tokenPayload['given_name'],
            lastName: tokenPayload['family_name'],
            id: tokenPayload['sid']
          };

          this._tokenStorage.setCustomerUserInfo(userInfo);
          this.authService.sendAuthStateChangeNotification(response.isAuthSuccessful);

          let ngbModalOptions: NgbModalOptions = {
            backdrop: 'static',
            keyboard: false,
          };

          let _legalEntityData = {
            legalEntityList: this.legalEntities
          }
          const modalRef = this._modalService.open(ModalDialogComponent, ngbModalOptions);
          modalRef.componentInstance.RequestedPage = 'Login';
          modalRef.componentInstance.Data = _legalEntityData;

          if (this.returnUrl != '' && this.returnUrl != '/' && this.returnUrl != undefined && this.returnUrl != null) {
            this._router.navigate([this.returnUrl]);
          }
        }
        else {
          this._alertService.error('Please enter valid credentials to login');
        }
      },
        error => {
          this.isLoginRequest = false;
          if (error.error.errorMessage == "Invalid Authentication") {
            this._alertService.error('Please enter valid credentials to login');
          } else { this._alertService.error('Unable to process login request. please try again later.'); }

        });
      */
    } catch (error) {
      this.isLoginRequest = false;
      this._alertService.error(error);
    }
  }

}
