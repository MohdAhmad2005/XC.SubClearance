import { Component, EventEmitter, Injectable, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NgbDate, NgbDateParserFormatter, NgbDateStruct, NgbDateAdapter, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';


//#region Formatter service
/**
* This Service handles how the date is rendered and parsed from keyboard i.e. in the bound input field.
*/
@Injectable()
export class CustomDateParserFormatter extends NgbDateParserFormatter {
  readonly DELIMITER = '/';

  parse(value: string): NgbDateStruct | null {
    if (value) {
      const date = value.split(this.DELIMITER);
      return {
        day: parseInt(date[0], 10),
        month: parseInt(date[1], 10),
        year: parseInt(date[2], 10)
      };
    }
    return null;
  }

  format(date: NgbDateStruct | null): string {
    return date ? date.day + this.DELIMITER + date.month + this.DELIMITER + date.year : '';
  }
}
//#endregion


@Component({
  selector: 'app-calender',
  templateUrl: './calender.component.html',
  styleUrls: ['./calender.component.css'],
  providers: [
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }
  ]
})


export class CalenderComponent implements OnInit {

  //#region Variable declarations
  selectedDate: string
  model2: NgbDate;
  //#endregion

  //#region Input/Output Properties
  @Input() ShowCalenderIcon: boolean = false; // If true, then it will show calender icon alongwith the calender text box otherwise it will not show
  @Input() Disabled: boolean = false; // set diisabled true for disabling the text box.
  @Input() DateFormat: any;// possible date formats 'DD/MM/YYYY', 'DD-MM-YYYY'
  @Input() BindDate: any; // saved date for binding on calender
  @Input() Control: FormControl;

  @Output() SelectedDate = new EventEmitter();
  //#endregion

  constructor(private ngbCalendar: NgbCalendar, private dateAdapter: NgbDateAdapter<string>) { }


  //#region Life Cycle methods
  ngOnInit(): void {
   // this.BindDate = this.ngbCalendar.getToday();
  }
  //#endregion

  //#region Other Methods
  ChangeDateEvent(date: NgbDate) {
    this.selectedDate = date.day + '/' + date.month + '/' + date.year;
    this.SelectedDate.emit(this.selectedDate);
  }
  //#endregion
}
