import {Component, OnInit} from '@angular/core';
import {MatSliderModule} from '@angular/material/slider';
import {NavBarSearch} from "../../entities/entities";
import {FormBuilder, FormControl, Validators} from "@angular/forms";


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
  priceMin: number = 300;
  priceMax: number = 5000;
/*
  priceMinControl: FormControl = new FormControl();
  priceMaxControl = new FormControl('',[
    Validators.min(this.priceMin)
  ]);

  priceForm = this.formBuilder.group({
    priceMin: this.priceMinControl,
    priceMax: this.priceMaxControl
  });

 */


  search() {
    this.consolelog();
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
    this.categorySearch.ticked = ticked;
  }

  updateAllComplete() {
    this.searchAllCategories = this.categorySearch.categories != null && this.categorySearch.categories.every(t => t.ticked);
  }

  consolelog() {
    console.log(this.categorySearch.ticked)
  }

  /*
  getPriceMaxError() {
    if (this.priceMaxControl.hasError('min')) {
      return 'Max. price has to be greater than min.'
    }
    return 'Please introduce a valid number';
  }

   */

  //TODO: Validators for the input range sliders -- fail
}
