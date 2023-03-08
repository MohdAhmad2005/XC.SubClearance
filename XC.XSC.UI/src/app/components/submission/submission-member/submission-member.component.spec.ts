import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionMemberComponent } from './submission-member.component';

describe('SubmissionMemberComponent', () => {
  let component: SubmissionMemberComponent;
  let fixture: ComponentFixture<SubmissionMemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionMemberComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
