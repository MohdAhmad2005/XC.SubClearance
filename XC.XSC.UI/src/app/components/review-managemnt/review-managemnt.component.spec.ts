import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewManagemntComponent } from './review-managemnt.component';

describe('ReviewManagemntComponent', () => {
  let component: ReviewManagemntComponent;
  let fixture: ComponentFixture<ReviewManagemntComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewManagemntComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewManagemntComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
