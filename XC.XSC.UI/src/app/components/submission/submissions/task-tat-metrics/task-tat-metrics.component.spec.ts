import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TaskTatMetricsComponent } from './task-tat-metrics.component';

describe('TaskTatMetricsComponent', () => {
  let component: TaskTatMetricsComponent;
  let fixture: ComponentFixture<TaskTatMetricsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskTatMetricsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskTatMetricsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
