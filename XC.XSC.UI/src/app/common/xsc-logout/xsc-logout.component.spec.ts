import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XscLogoutComponent } from './xsc-logout.component';

describe('XscLogoutComponent', () => {
  let component: XscLogoutComponent;
  let fixture: ComponentFixture<XscLogoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XscLogoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(XscLogoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
