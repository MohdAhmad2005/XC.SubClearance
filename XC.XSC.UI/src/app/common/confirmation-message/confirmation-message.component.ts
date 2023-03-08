import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'confirmation-message',
  templateUrl: './confirmation-message.component.html',
  styleUrls: ['./confirmation-message.component.css']
})
export class ConfirmationMessageComponent implements OnInit {
  @Input() Message: string;
  @Output() Accept = new EventEmitter();
  @Input() ActionReferenceType: any;

  constructor(private confirmationService: ConfirmationService) { }

 public confirm():void {
    this.confirmationService.confirm({
      message: this.Message,
      accept: () => {
        this.Accept.emit({ isConfirmed:true, referenceType:this.ActionReferenceType });
      },
      reject: () => {
        this.Accept.emit({ isConfirmed: false, referenceType: this.ActionReferenceType });
      }
    });
  }
  ngOnInit(): void {
    this.confirm();
  }
}
