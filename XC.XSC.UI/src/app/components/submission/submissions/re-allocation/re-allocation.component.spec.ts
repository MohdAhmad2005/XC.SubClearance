import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReAllocationComponent } from './re-allocation.component';

describe('ReAllocationComponent', () => {
  let component: ReAllocationComponent;
  let fixture: ComponentFixture<ReAllocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReAllocationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReAllocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
