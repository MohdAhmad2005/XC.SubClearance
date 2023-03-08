import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionAtGlanceComponent } from './submission-at-glance.component';

describe('SubmissionAtGlanceComponent', () => {
  let component: SubmissionAtGlanceComponent;
  let fixture: ComponentFixture<SubmissionAtGlanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionAtGlanceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionAtGlanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
