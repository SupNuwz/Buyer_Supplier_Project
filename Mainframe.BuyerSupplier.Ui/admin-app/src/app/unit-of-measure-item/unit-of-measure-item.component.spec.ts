import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitOfMeasureItemComponent } from './unit-of-measure-item.component';

describe('UnitOfMeasureItemComponent', () => {
  let component: UnitOfMeasureItemComponent;
  let fixture: ComponentFixture<UnitOfMeasureItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnitOfMeasureItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitOfMeasureItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
