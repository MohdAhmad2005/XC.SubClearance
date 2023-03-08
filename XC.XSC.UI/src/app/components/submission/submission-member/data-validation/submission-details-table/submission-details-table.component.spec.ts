import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionDetailsTableComponent } from './submission-details-table.component';

describe('SubmissionDetailsTableComponent', () => {
  let component: SubmissionDetailsTableComponent;
  let fixture: ComponentFixture<SubmissionDetailsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionDetailsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionDetailsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
