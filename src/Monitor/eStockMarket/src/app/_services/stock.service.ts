import { EnvService } from './env.service';

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { CommonService } from "./../_services/common.service";
import { formatDate } from '@angular/common';

import { Stock } from "./../model/stock.model";
import { StockSearch } from '../model/stock-search.model';

@Injectable({
  providedIn: 'root'
})

export class StockService {
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  apiUrl: string = "";

  constructor(
    public env: EnvService,
    private http: HttpClient,
    private commonService: CommonService) {    
    this.apiUrl = this.env.apiUrl;
   }

  addStock(stock: Stock): Observable<Stock> {
    //const url = `${this.apiUrl}Company/${code}`;
    const url = `${this.apiUrl}/v1.0/market/stock/add/${stock.companyCode}`;
    return this.http.post<Stock>(url, stock, this.httpOptions)
      .pipe(
        tap(_ => { }),
        catchError(this.commonService.handleError<Stock>(`Issue in addStock() for Company Code - ${stock.companyCode}`))
      );
  }

  getStocksByDate(code: string, startDate: Date, endDate: Date): Observable<Stock[]> {
    //const url = `${this.apiUrl}Company/${code}`;
    const sDate = formatDate(startDate, 'yyyy-MM-dd', 'en');
    const eDate = formatDate(endDate, 'yyyy-MM-dd', 'en');
    const url = `${this.apiUrl}/v1.0/market/stock/get/${code}/${sDate}/${eDate}/`;

    return this.http.get<Stock[]>(url)
      .pipe(
        tap(_ => { }),
        catchError(this.commonService.handleError<Stock[]>(`Issue in getStocksByDate() for Company Code = ${code}`))
      );
  }

  deleteStock(stockSearch: StockSearch): Observable<Stock[]> {
    const url = `${this.apiUrl}/v1.0/market/stock/delete`;
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      body: stockSearch
    };

    return this.http
      .delete<Stock[]>(url, options)
      .pipe(
        tap(_ => { }),
        catchError(this.commonService.handleError<Stock[]>(`Issue in deleteStock() for Company Code - ${stockSearch.companyCode}`))
      );
  }
}
