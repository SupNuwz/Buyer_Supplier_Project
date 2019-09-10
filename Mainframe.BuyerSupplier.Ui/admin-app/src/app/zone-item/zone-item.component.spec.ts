import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ZoneItemComponent } from './zone-item.component';

describe('ZoneItemComponent', () => {
  let component: ZoneItemComponent;
  let fixture: ComponentFixture<ZoneItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ZoneItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ZoneItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
