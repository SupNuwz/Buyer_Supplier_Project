import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StandardInventoryComponent } from './standard-inventory.component';

describe('StandardInventoryComponent', () => {
  let component: StandardInventoryComponent;
  let fixture: ComponentFixture<StandardInventoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StandardInventoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StandardInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
