import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskAuditHistoryComponent } from './task-audit-history.component';

describe('TaskAuditHistoryComponent', () => {
  let component: TaskAuditHistoryComponent;
  let fixture: ComponentFixture<TaskAuditHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskAuditHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskAuditHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
