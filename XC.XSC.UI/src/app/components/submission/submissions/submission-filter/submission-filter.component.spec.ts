import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionFilterComponent } from './submission-filter.component';

describe('SubmissionFilterComponent', () => {
  let component: SubmissionFilterComponent;
  let fixture: ComponentFixture<SubmissionFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
