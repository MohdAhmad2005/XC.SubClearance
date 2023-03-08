import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClearanceSanctionComponent } from './clearance-sanction.component';

describe('ClearanceSanctionComponent', () => {
  let component: ClearanceSanctionComponent;
  let fixture: ComponentFixture<ClearanceSanctionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClearanceSanctionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClearanceSanctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
