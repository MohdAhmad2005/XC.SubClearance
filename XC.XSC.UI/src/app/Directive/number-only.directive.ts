import { Directive, ElementRef, HostListener } from '@angular/core';
import { RegularExpressionServiceService } from '../../services/regular-expression-service.service';

@Directive({
  selector: '[NumberOnly]'
})
export class NumberOnlyDirective {

  constructor(private el: ElementRef, private _regularExpressionService: RegularExpressionServiceService) { }

  regExp = this._regularExpressionService.Phone;
  element = this.el;
  
  @HostListener('keypress', ['$event']) onKeyPress(event) {
    if (event.key == '+') {
      return true;
    }
    else
      return new RegExp(this.regExp).test(event.key);
  }

  // block from even copy paste special characters
  @HostListener('paste', ['$event']) blockPaste(event: ClipboardEvent) {
    this.validateFields(event);
  }
  validateFields(event: ClipboardEvent) {
    event.preventDefault();
    const pasteData = event.clipboardData.getData('text/plain').replace(/^[+]?\d+$/g, '');
    document.execCommand('insertHTML', false, pasteData);
  }
}



