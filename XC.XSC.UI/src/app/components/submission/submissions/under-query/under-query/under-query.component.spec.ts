import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnderQueryComponent } from './under-query.component';

describe('UnderQueryComponent', () => {
  let component: UnderQueryComponent;
  let fixture: ComponentFixture<UnderQueryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnderQueryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnderQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
