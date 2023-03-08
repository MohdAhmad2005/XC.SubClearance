import { HttpClient } from '@angular/common/http';
import { Injectable }   from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { QuestionBase } from './question-base';

@Injectable()
export class QuestionControlService {
  constructor(private fb:FormBuilder, private _httpclient: HttpClient) { }
customeValidations:[]
  toFormGroup(questions: QuestionBase<string>[] ) {
    const group = this.fb.group({});
    questions.forEach(question => {
      const control = this.fb.control(
        question.value, this.bindValidations(question.validations || [])
      );
      group.addControl(question.key, control)
    });
    return group;
  }

  bindValidations(validations: any) {
    if (validations.length > 0) {
      const validList = [];
      validations.forEach(valid => {
        if(valid.name =='required'){
          valid.validator = Validators.required;
        } if (valid.name =='pattern'){
          valid.validator = Validators.pattern(valid.validator);
        }
        validList.push(valid.validator);
      });
      return Validators.compose(validList);
    }
    return null;
  }

  getFormData(param:any):Promise<any>{
   return this._httpclient.get(param).toPromise();
  }

  public setDateFormate(data, questions) {
    let dateKeys = [];
    dateKeys = questions.filter(x => x.controlType == 'datepicker').map(x => x.key);
    dateKeys.forEach((item) => {
      Object.keys(data).forEach((x) => {
        if (item == x && data[item] != null) {
          let tzString = Intl.DateTimeFormat().resolvedOptions().timeZone;
          data[item] = new Date((typeof data[item] === "string" ? new Date(data[item]) : data[item]).toLocaleString("en-US", { timeZone: tzString }));
        }
      })
    });
    return data;
  }
  
}
