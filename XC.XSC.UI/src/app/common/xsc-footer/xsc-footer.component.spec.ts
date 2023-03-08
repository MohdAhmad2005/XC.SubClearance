import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XscFooterComponent } from './xsc-footer.component';

describe('XscFooterComponent', () => {
  let component: XscFooterComponent;
  let fixture: ComponentFixture<XscFooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XscFooterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(XscFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
