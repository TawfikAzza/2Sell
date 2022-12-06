import {Component, OnInit} from '@angular/core';
import {NavBarSearch} from "../../entities/entities";

@Component({
  selector: 'app-categoriesbar',
  templateUrl: './categoriesbar.component.html',
  styleUrls: ['./categoriesbar.component.css']
})
export class CategoriesbarComponent implements OnInit {

  constructor() {
  }

  ngOnInit(): void {

  }

  categorySearch: NavBarSearch = {
    operationType: 1,
    name: 'Categories:',
    ticked: false,
    categories: [
      {id: 1, name: 'Mountain Bike', ticked: false},
      {id: 2, name: 'Woman bike', ticked: false},
      {id: 3, name: 'City bike', ticked: false},
    ],
  };

  searchAllCategories: boolean = false;
  searchByPrice: boolean = false;

  priceLabel(value: number): string {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }
    return `${value}`;
  };

  search() {
    console.log(this.searchByPrice)
  }

  someTicked() {
    if (this.categorySearch.categories == null) {
      return false;
    }
    return this.categorySearch.categories.filter(t => t.ticked).length > 0 && !this.searchAllCategories;
  }

  setAll(ticked: boolean) {
    this.searchAllCategories = ticked;
    if (this.categorySearch.categories == null) {
      return;
    }
    this.categorySearch.categories.forEach(t => (t.ticked = ticked));
  }

  updateAllComplete() {
    this.searchAllCategories = this.categorySearch.categories != null && this.categorySearch.categories.every(t => t.ticked);
  }
}
