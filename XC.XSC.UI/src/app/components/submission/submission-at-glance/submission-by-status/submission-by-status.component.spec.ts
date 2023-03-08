import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionByStatusComponent } from './submission-by-status.component';

describe('SubmissionByStatusComponent', () => {
  let component: SubmissionByStatusComponent;
  let fixture: ComponentFixture<SubmissionByStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionByStatusComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionByStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
