import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionListOutscopeComponent } from './submission-list-outscope.component';

describe('SubmissionListOutscopeComponent', () => {
  let component: SubmissionListOutscopeComponent;
  let fixture: ComponentFixture<SubmissionListOutscopeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionListOutscopeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionListOutscopeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
