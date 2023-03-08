import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ChartConfig } from './chartconfig';


@Injectable({
  providedIn: 'root'
})
export class DoughnutService {

  constructor() { }

  public config: ChartConfig = {
    theme: 'lara-light-blue',
    dark: false,
    inputStyle: 'outlined',
    ripple: true
  };

  private configUpdate = new Subject<ChartConfig>();

    configUpdate$ = this.configUpdate.asObservable();

    updateConfig(config: ChartConfig) {
        this.config = config;
        this.configUpdate.next(config);
    }

    getConfig() {
        return this.config;
    }
}
