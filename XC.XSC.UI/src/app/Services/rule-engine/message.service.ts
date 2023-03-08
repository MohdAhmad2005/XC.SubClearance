import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { GlobalService } from './global.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  constructor(
    private toastr: ToastrService,
    private globalService: GlobalService
  ) { }

  successmsg(msg: string) {
    this.toastr.success(msg, 'Success', {
      positionClass: 'toast-bottom-right',
      closeButton: true,
      timeOut: 10000
    });
  }
  hidesuccessmsg(msg: string, msgShow: boolean) {

    if (msgShow === true) {
      this.toastr.success(msg, 'Success', {
        positionClass: 'toast-bottom-right',
        closeButton: true,
        timeOut: 10000
      });
    }
  }
  errorsmsg(msg: string) {
    this.toastr.error(msg, 'Error', {
      positionClass: 'toast-bottom-right',
      closeButton: true,
      timeOut: 10000
    });
  }

  infotoastr(msg: string) {
    this.toastr.info(msg, 'Information', {
      positionClass: 'toast-bottom-right',
      closeButton: true,
      timeOut: 10000
    });
  }
  toastrwaring(msg: string) {
    this.toastr.warning(msg, 'Warning', {
      positionClass: 'toast-bottom-right',
      closeButton: true,
      timeOut: 10000
    });
  }
  toastrnotimeoutwaring(msg: string) {
    this.toastr.warning(msg, 'Warning', {
      positionClass: 'toast-bottom-right',
      closeButton: true,
      timeOut: 70000
    });
  }
}
