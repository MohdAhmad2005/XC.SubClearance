import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class ConvertObjectToQueryparamService {

  constructor() { }

   setQueryParamString(object: any) {
    let queryParamString = [];
     for (var objItem in object)
       if (object.hasOwnProperty(objItem)) {
         queryParamString.push(encodeURIComponent(objItem) + "=" + encodeURIComponent(object[objItem]));
      }
    return queryParamString.join("&");
  }
}
