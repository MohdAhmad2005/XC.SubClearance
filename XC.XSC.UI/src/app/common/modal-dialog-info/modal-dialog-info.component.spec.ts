import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDialogInfoComponent } from './modal-dialog-info.component';

describe('ModalDialogInfoComponent', () => {
  let component: ModalDialogInfoComponent;
  let fixture: ComponentFixture<ModalDialogInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDialogInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalDialogInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
