import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WaveManagementComponent } from './wave-management.component';

describe('WaveManagementComponent', () => {
  let component: WaveManagementComponent;
  let fixture: ComponentFixture<WaveManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WaveManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WaveManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
