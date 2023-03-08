import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericSubmissionControlComponent } from './generic-submission-control.component';

describe('GenericSubmissionControlComponent', () => {
  let component: GenericSubmissionControlComponent;
  let fixture: ComponentFixture<GenericSubmissionControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenericSubmissionControlComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericSubmissionControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
