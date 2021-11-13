import { Component, OnInit, ViewChild, ChangeDetectorRef, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormControl } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { Company } from "./../model/company.model";
import { Aggregator } from '../model/aggregator.model';
import { ExchangeType } from "./../model/exchange-type.model";

import { CommonService } from "./../_services/common.service";
import { CompanyService } from "./../_services/company.service";
import { AggregatorService } from "./../_services/aggregator.service";

import { ConfirmDialogComponent } from "./../shared/confirm-dialog/confirm-dialog.component";
import { CreateCompanyComponent } from "./../company/create-company/create-company.component";

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})

export class CompanyComponent implements OnInit, AfterViewInit {

  myControl = new FormControl();
  dropDownList: string[] = [];
  filteredOptions!: Observable<string[]>;

  aggregator: Aggregator = new Aggregator();
  companyList: Company[] = new Array<Company>();
  searchValue: string = '';

  showFooter: boolean = false;
  defaultColumns = ['code', 'name', 'exchangeTypeId', 'actions'];
  defaultColumnsWithPrice = ['code', 'name', 'price', 'exchangeTypeId', 'actions'];
  displayedColumns: string[] = [];

  dataSource!: MatTableDataSource<Company>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  expandedElement: any;

  @Output() returnValueToHome: EventEmitter<any> = new EventEmitter()

  constructor(private cdr: ChangeDetectorRef,
    public companyService: CompanyService,
    public commonService: CommonService,
    public aggregatorService: AggregatorService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this.getAllCompanies(true);

    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  ngAfterViewInit(): void {
    this.reloadPaginator();
  }

  ngOnChanges() {
    this.dataSource = new MatTableDataSource(this.companyList);
    this.cdr.detectChanges();
    this.reloadPaginator();
  }

  onSelectedCompany(option: string) {
    this.searchValue = this.getCompanyCodeFromString(option);
  }

  reloadPaginator() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  clearValues(isEmitEvent?: boolean) {
    this.companyList = new Array<Company>();
    this.dataSource = new MatTableDataSource(this.companyList);
    if (isEmitEvent) {
      this.returnValueToHome.emit(null);
      this.searchValue = '';
    }
  }

  getAllCompanies(isSelect: boolean = false) {
    this.clearValues(true);
    this.aggregatorService.getAllCompanies()
      .subscribe(companies => {
        this.dropDownList = companies.map(a => a.code + ' (' + a.name + ')');
        if (isSelect) {
          return;
        } else {
          this.displayedColumns = this.defaultColumnsWithPrice;
          this.companyList = companies;
          this.ngOnChanges();
        };
      });
  }

  searchByCode() {
    const searchText = this.getCompanyCodeFromString(this.myControl.value);
    this.aggregatorService.getCompanyByCode(searchText).subscribe(aggregator => {
      this.clearValues(false);
      this.returnValueToHome.emit(aggregator);
      if (aggregator) {
        this.displayedColumns = this.defaultColumns;
        this.companyList.push(aggregator.companyDetails);
        this.ngOnChanges();
        this.expandRow(aggregator.companyDetails);
        return;
      }

      this.commonService.openSnackBar(`There is no Company with the Code - ${searchText}`, "Dismiss");
    });
  }

  addCompanyDialog() {
    const data = { disableClose: true, data: { issue: {} } };
    const dialogRef = this.commonService.dialogOpen(CreateCompanyComponent, data);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getAllCompanies();
      }
    });
  }

  getExchangeValue(exchangeTypeId: number) {
    return exchangeTypeId ? ExchangeType[exchangeTypeId] : ExchangeType[0];
  }

  getTurnOverValue(turnOver: number) {
    return turnOver ? turnOver / 10000000 : 0;
  }

  deleteCompany(companyCode: string) {
    const data = {
      data: {
        title: 'Confirm Remove Company',
        messageLine1: 'Are you sure, you want to remove an Company: ' + companyCode + '.',
        messageLine2: 'NOTE: This will also delete the stocks of the company.'
      }
    };

    const confirmDialog = this.commonService.dialogOpen(ConfirmDialogComponent, data);

    confirmDialog.afterClosed().subscribe(result => {
      if (result === true) {
        this.companyService.deleteCompany(companyCode).subscribe(data => {
          this.getAllCompanies(true);
        }, err => {
          console.log(err);
        });
      }
    });
  }

  searchText(newValue: string) {
    if (newValue.length === 0) {
      this.clearValues(true);
    }
  }

  getCompanyCodeFromString(value: string) {
    return value ? value.split(' (')[0] : value;
  }

  expandRow(row: Company) {
    if (this.expandedElement) {
      if (row.code === this.expandedElement.code) {
        this.expandedElement = undefined;
      } else {
        this.expandedElement = row;
      }
    } else {
      this.expandedElement = row;
    }
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.dropDownList.filter(option => option.toLowerCase().includes(filterValue));
  }
}
