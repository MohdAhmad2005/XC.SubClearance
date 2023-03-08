import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ISubmissionStatusResponse } from 'src/app/models/submission/submissionlist';
import { SubmissionService } from 'src/app/Services/submission/submission.service';
import { CommonStorageService } from '../../../common/common-storage/common-storage.service';
import { UserCommonStorage } from '../../../common/common-storage/user-common-storage';
import { ToasterService } from '../../../common/toaster/toaster.service';
import { Region } from '../../../models/uam/region/region';
import { Lob } from '../../../models/user/lob';

@Component({
  selector: 'app-submission-at-glance',
  templateUrl: './submission-at-glance.component.html',
  styleUrls: ['./submission-at-glance.component.css']
})
export class SubmissionAtGlanceComponent implements OnInit {

  public regionList: Region[];
  public lobList: Lob[];
  public selectedRegion: number;
  public selectedLob: number;
  

  submissionGlanceList: ISubmissionStatusResponse[];
  @Output() ChangeRegionOrLobEvent: EventEmitter<boolean> = new EventEmitter();

  public userCommonStorage: UserCommonStorage = new UserCommonStorage();


  constructor(private _submissionService: SubmissionService,
    private _toasterService: ToasterService,
    private _commonStorageService: CommonStorageService) { }

  ngOnInit(): void {

    this.getRegionList();
  }
  private getRegionList(): void {
    this._submissionService.getUserRegions().subscribe(response => {
      if (response.isSuccess) {
        this.regionList = response.result;
        this.getLobList();
      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.warningMessage(error.message);
      });
  }

  private getLobList(): void {
    this._submissionService.getUserLobApi().subscribe(response => {debugger;
      if (response.isSuccess) {
        this.lobList = response.result;

        this.userCommonStorage = this._commonStorageService.getCommonStorage();

        if (!!this.userCommonStorage) {
          if (!!this.userCommonStorage.region && !!this.userCommonStorage.lob) {
            this.selectedRegion = this.userCommonStorage.region;
            this.selectedLob = this.userCommonStorage.lob;
          }
          else {
            this.userCommonStorage.lob = this.selectedLob = this.lobList[0].id;
            this.userCommonStorage.region = this.selectedRegion = this.regionList[0].id;
            this._commonStorageService.setCommonStorage(this.userCommonStorage)
          }
        }
        this.GetSubmissionGlance();
        this.ChangeRegionOrLobEvent.emit(true);
      }
      else {
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.warningMessage(error.message);
      });
  }

  GetSubmissionGlance() {

    this._submissionService.getSubmissiosnGlance().subscribe(response => {
      if (response.isSuccess) {
        this.submissionGlanceList = response.result as ISubmissionStatusResponse[];
      }
      else {
        this.submissionGlanceList = {} as ISubmissionStatusResponse[];
        this._toasterService.errorMessage(response.message);
      }
    },
      error => {
        this._toasterService.warningMessage(error.message);
        this.submissionGlanceList = {} as ISubmissionStatusResponse[];
      });
  }

  public changeRegion(event: any): void {
    this.userCommonStorage = this._commonStorageService.getCommonStorage();
    if (!!this.userCommonStorage) {
      this.userCommonStorage.region = event.target.value;
      this._commonStorageService.setCommonStorage(this.userCommonStorage)
    }
    this.GetSubmissionGlance();
    this.ChangeRegionOrLobEvent.emit(false);
  }
  public changeLob(event: any): void {
    this.userCommonStorage = this._commonStorageService.getCommonStorage();
    if (!!this.userCommonStorage) {
      this.userCommonStorage.lob = event.target.value;
      this._commonStorageService.setCommonStorage(this.userCommonStorage)
    }
    this.GetSubmissionGlance();
    this.ChangeRegionOrLobEvent.emit(false);
  }
}
