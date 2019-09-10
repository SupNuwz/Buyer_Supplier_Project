import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeWavesItemComponent } from './time-waves-item.component';

describe('TimeWavesItemComponent', () => {
  let component: TimeWavesItemComponent;
  let fixture: ComponentFixture<TimeWavesItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeWavesItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeWavesItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
