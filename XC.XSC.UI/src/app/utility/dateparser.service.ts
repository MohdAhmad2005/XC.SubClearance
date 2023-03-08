import { Injectable } from '@angular/core';
import { EnvironmentService } from 'src/services/environment-service.service';

@Injectable({
  providedIn: 'root'
})
export class DateparserService {

  constructor(private _envService: EnvironmentService) { }

  FormatDate(date: string) {

    var _formattedDate = '';
    if (!!date) {
      const _date = new Date(date);
      let dateFormat = this._envService.dateFormat;
      switch (dateFormat) 
      {
        case 'DD/MM/YYYY':
          var date: string = (_date.getDate().toString().length <= 1 ? '0' + _date.getDate().toString() : _date.getDate().toString());
          var month: string = ((_date.getMonth() + 1).toString().length <= 1 ? '0' + (_date.getMonth() + 1).toString() : (_date.getMonth() + 1).toString());
          _formattedDate = date + "/" + month + "/" + _date.getFullYear();
          break;

      }
    }
    return _formattedDate;
  }

  FormatToISOString(date: Date) {
    if (!!date) {
      return date.toISOString();
    }
    return true;
  }

  FormatToCurrentTimeZone(date:Date){
    if(!!date){
      return new Date(date.getTime() - date.getTimezoneOffset() * 60 * 1000)
    }
  }
}
