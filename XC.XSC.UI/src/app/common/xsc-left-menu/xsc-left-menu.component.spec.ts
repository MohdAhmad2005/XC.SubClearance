import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XscLeftMenuComponent } from './xsc-left-menu.component';

describe('XscLeftMenuComponent', () => {
  let component: XscLeftMenuComponent;
  let fixture: ComponentFixture<XscLeftMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XscLeftMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(XscLeftMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
