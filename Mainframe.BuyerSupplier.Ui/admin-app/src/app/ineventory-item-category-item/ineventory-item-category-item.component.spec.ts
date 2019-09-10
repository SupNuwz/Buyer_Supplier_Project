import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IneventoryItemCategoryItemComponent } from './ineventory-item-category-item.component';

describe('IneventoryItemCategoryItemComponent', () => {
  let component: IneventoryItemCategoryItemComponent;
  let fixture: ComponentFixture<IneventoryItemCategoryItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IneventoryItemCategoryItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IneventoryItemCategoryItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
