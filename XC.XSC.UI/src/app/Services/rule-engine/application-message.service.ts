import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApplicationMessageService {
  messageList: any;
  constructor(private http: HttpClient) { }

  init() {
    this.readMessage();
  }

  readMessage() {
    this.http.get('./assets/config/rule-engine-messages.json').subscribe(res => {
      var result = res as any;
      this.messageList = result.ServiceMessages;
    })
  }

  getMessageByKey(key) {
    if (!this.messageList)
      this.readMessage();
    if (this.messageList)
      return this.messageList.filter(x => x.Key == key)[0].Message;

  }

}
