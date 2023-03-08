import { Injectable } from '@angular/core';
import { GenericGridSettings } from 'src/app/models/config/genricGridSetting';
import { GenericGridService } from './generic-grid.service';
@Injectable({
  providedIn: 'root'
})
export class BindGridSettingService {

  constructor(private _genericGridService: GenericGridService) { }

  private genericGridSettings: GenericGridSettings = new GenericGridSettings();

  BindGridSettings(gridEvent: any): GenericGridSettings {
    this.genericGridSettings.page = (!!((gridEvent.first / gridEvent.rows) + 1)) ? (gridEvent.first / gridEvent.rows) + 1 : 1;
    this.genericGridSettings.limit = (!!gridEvent.rows) ? gridEvent.rows : 5;
    this.genericGridSettings.sortField = (!!gridEvent.sortField) ? gridEvent.sortField : '';
    //this.genericGridSettings.sortOrder = (!!gridEvent.sortOrder) ? (gridEvent.sortOrder=='-1'? 0 :gridEvent.sortOrder) : '1';
    this.genericGridSettings.sortOrder = this._genericGridService.getSortOrder(gridEvent.sortOrder);
    this.genericGridSettings.globalFilter = (!!gridEvent.globalFilter) ? gridEvent.globalFilter : '';
    this.genericGridSettings.mulitSortMeta = (!!gridEvent.mulitSortMeta) ? gridEvent.mulitSortMeta : '';

    return this.genericGridSettings;
  }
}
