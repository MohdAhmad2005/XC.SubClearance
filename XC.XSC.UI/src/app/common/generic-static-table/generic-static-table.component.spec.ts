import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericStaticTableComponent } from './generic-static-table.component';

describe('GenericStaticTableComponent', () => {
  let component: GenericStaticTableComponent;
  let fixture: ComponentFixture<GenericStaticTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenericStaticTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericStaticTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
