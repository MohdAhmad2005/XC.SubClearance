import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionPerformanceComponent } from './submission-performance.component';

describe('SubmissionPerformanceComponent', () => {
  let component: SubmissionPerformanceComponent;
  let fixture: ComponentFixture<SubmissionPerformanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionPerformanceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionPerformanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
