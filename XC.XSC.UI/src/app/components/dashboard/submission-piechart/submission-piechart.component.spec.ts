import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionPiechartComponent } from './submission-piechart.component';

describe('SubmissionPiechartComponent', () => {
  let component: SubmissionPiechartComponent;
  let fixture: ComponentFixture<SubmissionPiechartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionPiechartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionPiechartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
