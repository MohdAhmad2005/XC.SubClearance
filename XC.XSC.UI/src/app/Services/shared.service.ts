import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { distinctUntilChanged } from 'rxjs/operators';
import { DatePipe } from '@angular/common'
import { QuestionBase } from '../common/dynamic-form/question-base';
import { IResponse } from '../models/IResponse';
import { EnvironmentService } from 'src/services/environment-service.service';
import { Event } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SharedService{
  @Output() messageDetail = new EventEmitter();
  url="getForm"
  testData: any;
  formData: FormData = new FormData();
  fileList: FileList;
   public currentUser: any;
    constructor(private http: HttpClient,private dp:DatePipe,  private _envService: EnvironmentService,) {
  }
  public dynamicFormData: QuestionBase<string>[] = [];
  public setCronvalue: Subject<any> = new Subject();
  public autocomplete: Subject<any> = new Subject();
  public autoCompleteValueChange: Subject<any> = new Subject();
  public setMultiselectDropdownValue: BehaviorSubject<any> = new BehaviorSubject(null);
  public setTextBoxValue: Subject<any> = new Subject();
  public sendTextboxValue: Subject<any> = new Subject();
  defaultDate:any;
  @Output() getActionEventEmitter: EventEmitter<any> = new EventEmitter();  
  @Output() autoComplete = new EventEmitter();
  @Output() fileChange = new EventEmitter();
  @Output() onSelectedDate = new EventEmitter();

  /**
   * @description Removes duplicate from Array of objects
   * @param originalArray Holds the data of array passed when method is called.
   * @param prop is condition on which we can remove duplicate data from array.
   * @returns  It return the new array with no duplicate value.
   */
  removeDuplicate(originalArray, prop) {
    var newArray = [];
    var lookupObject = {};
    for (var i in originalArray) {
      lookupObject[originalArray[i][prop]] = originalArray[i];
    }
    for (i in lookupObject) {
      newArray.push(lookupObject[i]);
    }
    return newArray;
  }

  /**
   * @description Validates all form fields
   * @param formGroup 
   * @param questions 
   */
  validateAllFormFields(formGroup: FormGroup, questions: any) {
    questions.forEach(x => {
      if (x.validations) {
        formGroup.controls[x.key].markAsDirty();
      }
    });
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });

      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control, questions);
      }
    });
  }
  public setPaginationData: Subject<any> = new Subject();
  /**


  /**
   * Adds validation
   * @param item 
   * @param form 
   */
  addValidation(item, form: FormGroup) {
    item.validations = [{ name: "required", validator: Validators.required, message: `${item.label} is required` }];
    form.controls[item.key].setValidators([Validators.required]);
    form.controls[item.key].updateValueAndValidity();
  }


  /**
   * Adds pattern
   * @param item 
   * @param form 
   */
  addPattern(item, form:FormGroup){
    item.validations = [{ name: "pattern", validator: Validators.pattern, message: `${item.label} must be alphanumeric and hyphen` }];
    form.controls[item.key].setValidators([Validators.pattern("^[a-zA-Z0-9-]*$")]);
    form.controls[item.key].updateValueAndValidity();
  }

  /**
   * Removes validation
   * @param item 
   * @param form 
   */
  removeValidation(item, form: FormGroup) {
    item.validations = [];
    form.controls[item.key].clearValidators();
    form.controls[item.key].updateValueAndValidity();
  }


  /**
   * @description Sets value
   * @param object 
   * @param path 
   * @param value 
   * @returns  
   */
  setValue(object, path, value) {
    path = path.replace(/[\[]/gm, '.').replace(/[\]]/gm, ''); //to accept [index]
    var keys = path.split('.'),
      last = keys.pop();
    keys.reduce(function (o, k) { return o[k] = o[k] || {}; }, object)[last] = value;
    return object
  }

  fileUpload(data: any[]) {
    const formData = new FormData();
    if (data && data.length) {
      for (let i = 0; i < data.length; i++) {
        formData.append("files", data[i], data[i]['name']);        
      }   
    } 
    return formData;  
  }

  setDateRange(){
    this.defaultDate = new Date();
    var newMonth = this.defaultDate.getMonth() - 1;
    if (newMonth < 0) {
      newMonth += 12;
      this.defaultDate.setFullYear(this.defaultDate.getFullYear() - 1); // use getFullYear instead of getYear !
    }
    this.defaultDate.setMonth(newMonth)
    this.defaultDate = new Date(this.dp.transform(this.defaultDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'", 'es-ES'));
    let todayDate = new Date();
    let dateRange = [];
    dateRange[0]= this.defaultDate;
    dateRange[1]=todayDate;
    return dateRange;
  }
 
  getDropdownData(url: any): Observable<any> {
    return this.http.get<IResponse>(this.getRoute(url, this._envService.smartClearApiUrl));
  }

  async loadDropDownData(url: any): Promise<any> {
    return await this.http.get<IResponse>(this.getRoute(url, this._envService.smartClearApiUrl)).toPromise();
  }
  async loadData(url: any): Promise<any> {
    return await this.http.get<IResponse>(this.getRoute(url, this._envService.smartClearApiUrl)).toPromise();
  }

   setDateFormate(data, questions) {
    let dateKeys = [];
    dateKeys = questions.filter(x => x.type == 'datepicker').map(x => x.key);
    dateKeys.forEach((item) => {
      Object.keys(data).forEach((x) => {
        if (item == x && data[item] != null) {
          data[item] = new Date(data[item]);         
        }
      }) 
    });
    let objData = {
      'data': data
    } 
    return data;
  }

  private getRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }

  getActionbyStatus(status:string, actionResult:any[]):any[]{
    return actionResult.filter(x=>x.statusId == status)[0].allowedActions;
  }
}
