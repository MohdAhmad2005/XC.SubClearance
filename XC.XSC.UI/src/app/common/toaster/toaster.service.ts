import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ToasterService {
  public modalToastKey: string = "modalToast";

  constructor(private _messageService: MessageService) { }

  public errorMessage(message: string, key?: string): void {
    this._messageService.add({ severity: 'error', summary: 'Error', key: key, detail: message, sticky: false });
  }

  public successMessage(message: string, key?: string): void {
    this._messageService.add({ severity: 'success', summary: 'Success', key: key, detail: message, sticky: false });
  }

  public warningMessage(message: string, key?: string): void {
    this._messageService.add({ severity: 'warn', summary: 'Warning', key: key, detail: message, sticky: false });
  }
  public infoMessage(message: string, key?: string): void {
    this._messageService.add({ severity: 'info', summary: 'Informative', key: key, detail: message, sticky: false });
  }

  public clear() {
    this._messageService.clear();
  }
}
