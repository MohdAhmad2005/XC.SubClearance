import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { DateparserService } from 'src/app/utility/dateparser.service';
import { ModalDialogComponent } from '../modal-dialog/modal-dialog.component';

@Component({
  selector: 'generic-static-table',
  templateUrl: './generic-static-table.component.html',
  styleUrls: ['./generic-static-table.component.css']
})
export class GenericStaticTableComponent implements OnInit {

  @Input() RowData: any[];
  @Input() ColumnDefination: any[];
  @Input() FilterField: any[];
  @Input() Actions: any[];
  @Input() IsActionVisible:boolean=true;
  @Input() IsGlobalSearchFilterVisible:boolean=true;
  @Input() IsNoRecordVisible:boolean=true;
  @Output() ActionEventEmitter: EventEmitter<any> = new EventEmitter();
  @Output() CheckBoxEventEmitter: EventEmitter<any> = new EventEmitter();


  constructor(private _dateformatter: DateparserService,
    public _modalService: NgbModal) { }

  ngOnInit(): void {
  }

  public onActionClick(actionItem: any, rowData: any): void {
    this.ActionEventEmitter.emit({ event: actionItem, data: rowData });
  }

  public onCheckBoxChange(event: any, rowData: any) {
    this.CheckBoxEventEmitter.emit({ event, data: rowData });
  }

  public getDate(data: any): String {
     return (this._dateformatter.FormatDate(data));
  }

  public getText(rowData: any): String {
    if(rowData.length>99){      
      return (rowData.substring(0, 100));
    }
    else{
      return rowData;
    }
  }

  public onClickLink(rowData: any, column: any): void {        
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
    };
    const modalRef = this._modalService.open(ModalDialogComponent, ngbModalOptions);        
    modalRef.componentInstance.ModalHeader = column.header;
    modalRef.componentInstance.ModalBody = rowData.description;
  }

}
