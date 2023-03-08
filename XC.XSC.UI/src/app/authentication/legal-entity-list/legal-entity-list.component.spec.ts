import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LegalEntityListComponent } from './legal-entity-list.component';

describe('LegalEntityListComponent', () => {
  let component: LegalEntityListComponent;
  let fixture: ComponentFixture<LegalEntityListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LegalEntityListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LegalEntityListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
