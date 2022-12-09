import {Component, OnInit} from '@angular/core';
import {MatSliderModule} from '@angular/material/slider';

import {
  Category,
  categoryDTO,
  catPriceDTO,
  filterSearchDTO,
  NavBarSearch,
  postDTO,
  priceDTO
} from "../../entities/entities";
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {HttpService} from "../../services/http.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {PostfeedComponent} from "../postfeed/postfeed.component";


@Component({
  selector: 'app-categoriesbar',
  templateUrl: './categoriesbar.component.html',
  styleUrls: ['./categoriesbar.component.css']
})
export class CategoriesbarComponent implements OnInit {

  constructor(public http:HttpService,
              private snackBar: MatSnackBar,
              private router:Router,
              private postFeed:PostfeedComponent) {
  }

  ngOnInit(): void {

  }
  result:postDTO[]=[];//Variable which will store the result of the search made through the searchCategories
  categorySearch: NavBarSearch = {
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

  getCategoriesIds(categories : any){
    let ticked: any;
    let filtered: number[] = [];
    ticked = categories.filter((t: { ticked: any; }) => t.ticked);
    for (let i = 0; i < ticked.length; i++) {
      filtered.push(ticked[i].id)
    }
    return filtered;
  }

  async search() {
    if(!this.categorySearch.ticked && !this.searchByPrice){
      console.log('nothing to search!')
      return;
    }
    else if(this.categorySearch.ticked && this.searchByPrice) {
      let catPriceSearch : catPriceDTO = {
        ids: this.getCategoriesIds(this.categorySearch.categories),
        min: this.priceMin,
        max: this.priceMax
      }
      let dto: filterSearchDTO = {
        operationType: 3,
        dto: catPriceSearch
      }
      this.result = await this.http.filterSearch(dto);
      console.log('search by both cat and price!');
      return;
    }
    else if(this.categorySearch.ticked && !this.searchByPrice){
      let catSearch: categoryDTO = {
        ids: this.getCategoriesIds(this.categorySearch.categories)
      }

      let dto: filterSearchDTO = {
        operationType: 1,
        dto: catSearch
      }
      this.result = await this.http.filterSearch(dto);
      console.log('only searching by cat!');
      return;
    }
    else{
      let priceSearch : priceDTO = {
        min: this.priceMin,
        max: this.priceMax
      }

      let dto: filterSearchDTO = {
        operationType: 2,
        dto: priceSearch
      }
      this.result = await this.http.filterSearch(dto);
      this.http.result = this.result;
      console.log('only searching by price!');
      this.postFeed.ngOnInit();
      this.postFeed.log()
      return;
    }
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
    this.updateSearchCatIsTicked();
  }

  updateSearchCatIsTicked(){
    if (this.categorySearch.categories == null) {
      return;
    } else{
      this.categorySearch.ticked = this.categorySearch.categories.some(t => t.ticked);
    }
  };

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
