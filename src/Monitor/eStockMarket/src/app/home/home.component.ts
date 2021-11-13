import { Component, OnInit } from '@angular/core';

import { Stock } from "./../model/stock.model";
import { Aggregator } from "./../model/aggregator.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  stockList: Stock[] = new Array<Stock>();
  searchedCompany: string = '';
  showStockDetails: boolean = false;

  constructor() { }

  ngOnInit(): void {

  }

  searchedValue(aggregator: Aggregator) {
    if (aggregator && aggregator.stockDetails) {
      this.stockList = aggregator.stockDetails;
      this.showStockDetails = true;
      this.searchedCompany = aggregator.companyCode || '';
    } else {
      this.showStockDetails = false;
      this.searchedCompany = '';
    }
  }
}
