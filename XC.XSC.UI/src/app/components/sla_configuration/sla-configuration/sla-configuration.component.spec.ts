import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlaConfigurationComponent } from './sla-configuration.component';

describe('SlaConfigurationComponent', () => {
  let component: SlaConfigurationComponent;
  let fixture: ComponentFixture<SlaConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlaConfigurationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SlaConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
