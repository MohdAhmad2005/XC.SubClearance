import { Component, Input, OnInit } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import { SubmissionData } from 'src/app/models/submission/data-validations';
import { DateparserService } from '../../../../../utility/dateparser.service';

@Component({
  selector: 'generic-submission-control',
  templateUrl: './generic-submission-control.component.html',
  styleUrls: ['./generic-submission-control.component.css'],
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
})
export class GenericSubmissionControlComponent implements OnInit {

  @Input() controls: SubmissionData = new SubmissionData()
  @Input() controlType;
  @Input() index;
  @Input() form: NgForm;
  constructor() { }


  ngOnInit(): void {
  }

  public onChangeSuggetions(data: SubmissionData): void {
    let datepickerControls = this.controls['controls'].filter(x => x.control == 'datepicker');
    let suggestionList = data.suggestions.suggestionOptions.filter(x => x.id == data.suggestions.id);
    if (suggestionList && suggestionList.length) {
      if (datepickerControls && datepickerControls.length) {
        data[datepickerControls[0].key] = (suggestionList[0]['finalEntry']);
      } else {
        data['finalEntry'] = suggestionList[0]['finalEntry'];
      }
      data['confidance'] = suggestionList[0]['confidance'];
    }

  }

  public onchange(controls, formControl): void {
    if (formControl.key === 'finalEntry' && formControl.control != 'datepicker') {
      let selectedNoneValue = controls.suggestions.suggestionOptions.filter(x => x.name === 'None');
      if (selectedNoneValue && selectedNoneValue.length) {
        controls.suggestions.id = selectedNoneValue[0].id;
      }
    }
  }

  public onDateChange(controls, formControl): void {
    if (formControl.key === 'finalEntry' && formControl.control == 'datepicker') {
      let selectedNoneValue = controls.suggestions.suggestionOptions.filter(x => x.name === 'None');
      if (selectedNoneValue && selectedNoneValue.length) {
        controls.suggestions.id = selectedNoneValue[0].id;
      }
    }
  }
}
