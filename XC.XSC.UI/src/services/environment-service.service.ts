import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {

  public production: boolean = environment.production;
  public urlAddress: string = environment.domain;
  public smartClearApiUrl: string = environment.xscApiUrl;
  public encryption: any = environment.encryption;
  public otoCodeLength: number = environment.verificationCodeLength;
  public sessionTimeOut: number = environment.sessionTimeout;
  public dateFormat: string = environment.dateFormat;
  public uamApiUrl: string = environment.uamApiUrl;
  public ruleEngineApiUrl: string = environment.ruleEngineApiUrl;
  public DefaultPageSize: number = environment.DefaultPageSize;

  constructor() { }
}
