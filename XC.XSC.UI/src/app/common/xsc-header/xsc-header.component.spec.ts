import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XscHeaderComponent } from './xsc-header.component';

describe('XscHeaderComponent', () => {
  let component: XscHeaderComponent;
  let fixture: ComponentFixture<XscHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XscHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(XscHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
