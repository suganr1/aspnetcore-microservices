import { Component, Input, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { formatDate, DecimalPipe } from "@angular/common";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

import { StockService } from "./../_services/stock.service";
import { CommonService } from "./../_services/common.service";

import { Stock } from "./../model/stock.model";
import { StockSearch } from "./../model/stock-search.model";

import { ConfirmDialogComponent } from "./../shared/confirm-dialog/confirm-dialog.component";
import { AddStockComponent } from "./../stock/add-stock/add-stock.component";

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.scss']
})
export class StockComponent implements OnInit {

  @Input() companyCode: string = '';
  @Input() stockList: Stock[] = new Array<Stock>();

  range: FormGroup = new FormGroup({
    start: new FormControl(),
    end: new FormControl()
  });

  displayedColumns = ['price', 'createdDate', 'createdTime', 'actions'];
  dataSource!: MatTableDataSource<Stock>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  maxStock: number = 0;
  minStock: number = 0;
  avgStock: number = 0;

  constructor(private cdr: ChangeDetectorRef, private fb: FormBuilder, private _decimalPipe: DecimalPipe,
    private stockService: StockService, private commonService: CommonService
  ) { }

  ngOnInit(): void {
    this.reactiveForm();
    this.minMaxAvgStock();
  }

  ngAfterViewInit() {
    this.reloadPaginator();
  }

  ngOnChanges() {
    this.dataSource = new MatTableDataSource(this.stockList);
    this.cdr.detectChanges();
    this.reloadPaginator();
  }

  clearValues() {
    this.maxStock = 0;
    this.minStock = 0;
    this.avgStock = 0;
  }

  minMaxAvgStock() {
    this.clearValues();
    if (this.stockList && this.stockList.length > 0) {
      this.maxStock = Math.max.apply(Math, this.stockList.map(function (o) { return o.price; }));
      this.minStock = Math.min.apply(Math, this.stockList.map(function (o) { return o.price; }));
      this.avgStock = this.stockList.reduce((total, next) => total + next.price, 0) / this.stockList.length;
    }
  }

  reloadPaginator() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  reactiveForm() {
    let startDate = new Date();
    startDate.setDate(startDate.getDate() - 30);
    let endDate = new Date();

    this.range = this.fb.group({
      start: [startDate, [Validators.required]],
      end: [endDate, [Validators.required]]
    });
  }

  loadStocksTable(stocks: Array<Stock>) {
    this.stockList = stocks;
    this.minMaxAvgStock();
    this.ngOnChanges();
  }

  getStocksByDate() {
    this.stockService.getStocksByDate(this.companyCode, new Date(this.range.value.start), new Date(this.range.value.end))
      .subscribe(stocks => {
        this.loadStocksTable(stocks);
      });
  }

  dateRangeChange(type: string, event: MatDatepickerInputEvent<Date>) {
    if (event.value) {
    } else {
      this.stockList = new Array<Stock>();
      this.ngOnChanges();
    }
  }

  addStock() {
    const data = { disableClose: true, data: { companyCode: this.companyCode } };
    const dialogRef = this.commonService.dialogOpen(AddStockComponent, data);

    dialogRef.afterClosed().subscribe(stocks => {
      if (stocks) {
        this.loadStocksTable(stocks);
      }
    });
  }

  deleteStock(stock: Stock) {
    if (stock) {
      let searchModel = new StockSearch();
      searchModel.companyCode = stock.companyCode;
      searchModel.id = stock.id;
      searchModel.fromDate = formatDate(new Date(this.range.value.start), 'yyyy-MM-dd', 'en');
      searchModel.toDate = formatDate(new Date(this.range.value.end), 'yyyy-MM-dd', 'en');

      const data = {
        data: {
          title: 'Confirm Remove Stock',
          messageLine1: 'Are you sure, you want to remove a Stock with Price: ' + this._decimalPipe.transform(stock.price, "1.2-2") + '.'
        }
      };

      const confirmDialog = this.commonService.dialogOpen(ConfirmDialogComponent, data);

      confirmDialog.afterClosed().subscribe(result => {
        if (result === true) {
          this.stockService.deleteStock(searchModel).subscribe(stocks => {
            this.loadStocksTable(stocks);
          }, err => {
            console.log(err);
          });
        }
      });
    }
  }
}
