import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, Renderer2, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { QuestionBase } from './question-base';
import { DropdownBase } from './dropdown-base';
import { SelectItem } from 'primeng/api';
import { from, Subject } from 'rxjs';
import { SharedService } from 'src/app/Services/shared.service';
import { EnvironmentService } from 'src/services/environment-service.service';



@Component({
  selector: "app-question",
  templateUrl: "./dynamic-form-question.component.html",
  styleUrls: ['./dynamic-form-question.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class DynamicFormQuestionComponent implements OnInit {
  multiSelectdata: SelectItem[];
  cols: any[] = [];
  rows: any[] = [];
  formSubjectData: any;
  taskId: any;
  @Input() question: QuestionBase<string>;
  @Input() form: FormGroup;
  @Output() autocomplete = new EventEmitter();
  @Output() onFileUpload = new EventEmitter();
  @Output() checkBoxChangeEvent = new EventEmitter();
  autoCompleteList:any[];
  private unsubscribe$: Subject<any> = new Subject<any>();
  public dropdown: DropdownBase[];
  public radio: DropdownBase[];
  visible: boolean;
  minimumDate = new Date();
  dateTime = new Date();
  maxDateTime = new Date();
  maxValue: number;
  isrange: boolean;
  @Input() source: string;
  submissionNumber: any;
  templateName: any;
  textBoxData: any;
  fileList: FileList;
  myFiles: any[] = [];
  ckeditorContent: any;
  currentUserTeam = JSON.parse(sessionStorage.getItem("currentUserTeam"));
  @Input() placeholder: string;
  @Input() isChecked:boolean = false;
  selectedCountries1: string[]= [];
  items: SelectItem[]=[];
  tagTooltip: any[]=[];
  selectedCities: string[] = [];
  selectedCountries2: string[] = [];
  item: string;
  countries: any[];
  public bsDateConfig: any = { dateInputFormat: this._envService.dateFormat, showWeekNumbers: false };
  public bsDateRangeConfig: any = { dateInputFormat: this._envService.dateFormat, rangeInputFormat: this._envService.dateFormat,showWeekNumbers: false };

  @Input() value ;
  constructor( private renderer: Renderer2, private element: ElementRef, private sharedService:SharedService,private _envService: EnvironmentService) {
  }

  async ngOnInit() {
    if (this.question.controlType === 'dropdown'){
      if (this.question['targetFieldName'] == null) {
        await this.loadDropdown(this.question.dropDownUri, this.question);
      } else if (this.question['targetFieldName']){
         this.bindCascadingDropdown();
      }
    } else if (this.question.controlType === 'multiselect'){
      if ((this.question['targetFieldName'] == null)) {
        this.multiselectDropdown(this.question.dropDownUri, this.question)
      } else if (this.question['targetFieldName']){
        this.getMultiselectTargetData();
      }
    } else if (this.question.controlType === 'radio'){
      this.loadRadio(this.question.dropDownUri, this.question);
    } else if (this.question.controlType === 'autocomplete'){
       if(this.question['dropDownUri']){
        this.loadAutocomplete(this.question['dropDownUri'], this.question)
      }
    } else if (this.question.controlType === 'cron'){
    //  this.setCronValue();
    }
  }

  get isValid():boolean {
    if (this.question.key) { 
      return this.form.controls[this.question.key]?.valid;
  }
    else {
      return false;
    }
}

  setValueIntextBox(url, key){
    // if(url){
    //    this.service.loadDropDownData(url).then(res => {
    //     if (res) {
    //       this.form.controls[key].setValue(res['responseBody']);
    //     }
    //   }, error => {
    //   })
    // } 
  }

  getMultiselectTargetData() {
    // this.sharedService.setMultiselectDropdownValue.subscribe(res => {
    //   if (res) {
    //     this.loadTargetMultiSelectData(res['key'], res['teamId']);
    //   }
    // })
  }

  loadTargetMultiSelectData(form: QuestionBase<string>, id: any) {
    let templateString = form['targetFieldUri'];
    let url = templateString.replace('{id}', id);
    this.multiselectDropdown(url, form)
  }
  
  bindCascadingDropdown() {
    // this.sharedService.setDropdownValue.subscribe(res => {
    //   if (res) {
    //     this.formSubjectData = res.data ? res.data : this.form.value;
    //     let dropDownId = res['dropDownId'] ? res['dropDownId'] : res;
    //     let form = res['key']
    //     this.bindDropdown(form, this.formSubjectData, dropDownId);
    //   } else {
    //     this.bindDropdown(this.question, null, null);
    //   }
    // })
  }

  /**
   *@Description  Loads multiselectDropdown used to bind multiselectDropdown data
   * @param url url contains the URl that needs to call the dropdown API.
   * @param key. used to bind associate dropdown data 
   */
  multiselectDropdown(url: string, key?: any) {
    if(url){  
      this.sharedService.getDropdownData(url).subscribe(res => {
        this.multiSelectdata = res.result;
        if (this.multiSelectdata && this.multiSelectdata.length) {
          key['cols'] = [];
          this.multiSelectdata.forEach(x => {
            key['cols'].push({ "label": x["name"], "value": x["id"] })
          })
          this.items = this.multiSelectdata;
        }
      })   
    }else{
        key['cols'] = key['options'];
      }  
  }

  /**
   * @description Loads radio
   * @param url 
   * @param key 
   */
  loadRadio(url: any, key) {
    if(url)
    {
      this.sharedService.getDropdownData(url).subscribe(res => {
        key['radio'] = res.responseBody;
      })
    }
    else
    {
      key['radio'] =  key['option'];
    }
    
  }

/**
   *@Description  Loads dropdown used to bind dropdown data
   * @param url url contains the URl that needs to call the dropdown API.
   * @param key. used to bind associate dropdown data 
   */
   async loadAutocomplete(url: any, key) {
    if(url){
      if(key['key'] =="processorId"){
        url =`${url}?orgId=${sessionStorage.getItem('loginTeamId')}&roleName=Processor`;
      }
      await this.sharedService.loadDropDownData(url).then((res:any) => {
          key['result'] = res;
          this.autoCompleteList = res
        
      }, error => {
        key['result'] = [];
      })
    } else {
      key['result'] = [];
    }
    
  };

  /**
   *@Description  Loads dropdown used to bind dropdown data
   * @param url url contains the URl that needs to call the dropdown API.
   * @param key. used to bind associate dropdown data 
   */
  async loadDropdown(url: any, key) {
    if (url) {
      await this.sharedService.loadDropDownData(url).then(res => {
        if (res) {
          key['options'] = res.result;
        }
      }, error => {
        key['options'] = [];
      })
    } else {  
      key['options'] = key['options'];
    }   
  };

    /**
   *@Description  Loads autocomplete used to bind  data
   * @param url url contains the URl that needs to call the dropdown API.
   */
  async search(event, form) {
    form['result']=this.autoCompleteList.filter(x=>(x.name).toLowerCase().includes(event.query))
    // let url: any;  
    //   url = `${form.dropDownUri}${event.query}`
    // await this.service.loadDropDownData(url).then(res => {
    //   if (res) {
    //     form['result'] = res.responseBody;
    //   } else {
    //     form['result'] = [];
    //   }
    // }, error => {
    //   form['result'] = [];
    // })
  }

  selectAutoCompleteValue(event, key){
    this.tagTooltip.push(event['name']);
    this.tagTooltip = [...new Set(this.tagTooltip)];
    
    this.sharedService.autoCompleteValueChange.next({'data':event.id, 'displayPopUp':true, 'key':key, 'name':event.name})
  }


  /**
   * @description Binds dropdown
   * @param form 
   * @param data 
   * @param selectDropDownValue 
   */
  async bindDropdown(form: QuestionBase<string>, data, selectDropDownValue: any) {
    let url;
    if (form) {
      let templateString = form['targetFieldUri'];
      let id = form['targetFieldName'];
      if (data) {
        let dropDownId = selectDropDownValue ? selectDropDownValue : data[id];
        url = templateString.replace('{id}', dropDownId);
      } else {
        url = form.dropDownUri;
      }
      if (url) {
        await this.loadDropdown(url, form)
      }
    }
  }


  dateChangeEvent(event){
    this.sharedService.onSelectedDate.emit(event)
  }

  // ngOnDestroy() {
  //   // this.sharedService.setDropdownValue.unsubscribe();
  // }

  /**
   * @description Maximum date
   * @param key 
   * @returns  
   */
  maxDate(key) {
   
  }

  minDate(key:string) {
   
  }

  // /**
  //  * Determines whether submission number on
  //  * @param value 
  //  */
  // onSubmissionNumber(value) {
  //   this.submissionNumber = value;
  //   this.sharedService.autoComplete.emit({ 'event': document.querySelector('.submissionNumber'), "data": this.submissionNumber });
  // }


  /**
   * @description Maximum data for slider
   * @param key 
   * @returns  
   */
  maxData(key) {
   
  }
  /**
     * @description set  rang for slider
     * @param key 
     * @returns  
     */
  rangData(key) {
    
  }

  /**
   * Sets text box
   */
  async setTextBox() {
    try {
      this.sharedService.setTextBoxValue.subscribe(res => {
        if (res) {
          let url = res['key']['targetFieldUri'];
          let key = res['keyName'];
          let form: FormGroup = res['form'];
          let dropDownId = res['dropDownId'];
          let templateString = url.replace('{id}', dropDownId);
          if (templateString) {
            this.getTextBoxUrlDetails(templateString, key, form);
          }
        }
      }, (error) => {
        console.info(error);
      });
    } catch (e) {
      console.info(e);
    }
  }

  /**
   * Gets text box url details
   * @param url 
   */
  getTextBoxUrlDetails(url, key, form: FormGroup) {
    try {
      this.sharedService.loadData(url).then(res => {
        if (res) {
          this.textBoxData = '';
          this.textBoxData = res['responseBody']['name'];
          if (this.textBoxData) {
            form.controls[key].setValue(this.textBoxData);
            form.controls[key].updateValueAndValidity();
          }
        }
      }, (error) => {
      })
    }
    catch (e) {
    }
  }
  
     /**
     * @description This method use to load file    
     */
    fileChange(event): void {
      this.myFiles=[];
      this.fileList = event.target.files;
      this.myFiles.push(this.fileList);
      this.onFileUpload.emit(this.myFiles)
    }

  // /**
  // * Files change
  // * @param event 
  // */
  // fileChange(event): void {
  //   document.getElementById('exampleFormControlFile1')['value'] = "";
  //   this.sharedService.fileChange.emit({ "fileList": event?.target?.files });
  // }

  handleChange(e) {
   this.checkBoxChangeEvent.emit(e.target.checked)
}

  /**
   * Events binder based on key
   * @param key 
   * @param [readOnly] 
   * @returns  
   */
  eventBinderBasedOnKey(key: any, readOnly?: boolean) {
    let check: boolean = false;
    return check;
  }

  public onValueChange(event: any) {
     var formattedDate;
     let tzString = Intl.DateTimeFormat().resolvedOptions().timeZone;
     formattedDate = new Date((typeof event === "string" ? new Date(event) : event).toLocaleString("en-US", { timeZone: tzString }));
     return formattedDate;
     }
     public onDateRangeValueChange(event: any) {
     var fromDate;
     var toDate;
     if (event && event.length) {
     let tzString = Intl.DateTimeFormat().resolvedOptions().timeZone;
     fromDate = new Date((typeof event[0] === "string" ? new Date(event[0]) : event).toLocaleString("en-US", { timeZone: tzString }));
     toDate = new Date((typeof event[1] === "string" ? new Date(event[1]) : event).toLocaleString("en-US", { timeZone: tzString }));
     }
    }

}
