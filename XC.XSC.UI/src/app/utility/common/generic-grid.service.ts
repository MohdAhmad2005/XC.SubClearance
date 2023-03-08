import { Injectable } from '@angular/core';
import { SortOrder } from 'src/app/models/config/genricGridSetting';

@Injectable({
  providedIn: 'root'
})
export class GenericGridService {

  constructor() { }

  public getSortOrder(gridSortOrder: number): SortOrder {
  
    if(gridSortOrder == -1) {
      return SortOrder.Ascending;
    } else {
      return SortOrder.Descending;
    }

  }

}
