import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeWavesComponent } from './time-waves.component';

describe('TimeWavesComponent', () => {
  let component: TimeWavesComponent;
  let fixture: ComponentFixture<TimeWavesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeWavesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeWavesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
